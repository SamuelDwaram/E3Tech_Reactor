using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.TrendsManager.Model.Interfaces;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Geared;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Unity;
using Brushes = System.Windows.Media.Brushes;

namespace E3.ReactorManager.TrendsManager.Model.Implementations
{
    public class TrendsManager : ITrendsManager
    {
        private readonly IDatabaseReader databaseReader;
        private List<string> trendsParameters;
        private DateTime startTimeStamp;
        private DateTime endTimeStamp;

        public TrendsManager(IUnityContainer containerProvider)
        {
            databaseReader = containerProvider.Resolve<IDatabaseReader>();
            trendsParameters = new List<string>();
        }

        public Dictionary<string, string> GetAvailableFieldDevices()
        {
            return databaseReader.GetAvailableFieldDevices();
        }

        public Dictionary<string, dynamic> GetTrendsData(object deviceId)
        {
            var trendsDataDictionary = new Dictionary<string, dynamic>();

            /* Get data from DB using DatabaseReader */
            var trendsData = databaseReader.ReadFieldDeviceData((string)deviceId, trendsParameters.ToList(), startTimeStamp, endTimeStamp);

            foreach (DataColumn column in trendsData.Columns)
            {
                if (column.ColumnName.ToLower().Contains("time"))
                {
                    trendsDataDictionary.Add("TimeStamp", new GearedValues<string>(trendsData.AsEnumerable().Select(x => DateTime.Parse(x[column.ColumnName].ToString()).ToString("yyyy-MM-dd HH:mm"))));
                }
                else
                {
                    trendsDataDictionary.Add(column.ColumnName, new GearedValues<double>(trendsData.AsEnumerable().Select(x => double.Parse(x[column.ColumnName].ToString()))));
                }
            }

            return trendsDataDictionary;
        }

        public void SetTrendsParameters(IList<string> parameters, DateTime startDate, DateTime endDate)
        {
            trendsParameters = parameters.ToList();
            startTimeStamp = startDate;
            endTimeStamp = endDate;
        }
        
        public string PrepareTrendsImageForGivenData(DataTable dataTable, Dictionary<string, string> parametersInfo)
        {
            SeriesCollection totalSeriesCollection = new SeriesCollection();
            AxesCollection axisY = new AxesCollection();
            foreach (KeyValuePair<string, string> parameter in parametersInfo)
            {
                if (dataTable.Columns.Contains(parameter.Key))
                {
                    Axis axis = new Axis
                    {
                        Title = parameter.Key,
                        Foreground = Brushes.Black,
                        MinValue = GetMinValue(parameter.Value),
                        MaxValue = GetMaxValue(parameter.Value),
                        Separator = new LiveCharts.Wpf.Separator
                        {
                            StrokeThickness = 0
                        },
                        LabelFormatter = val => Math.Round(val, 1).ToString()
                    };
                    axisY.Add(axis);
                    int index = totalSeriesCollection.Count;
                    totalSeriesCollection.Add(new GLineSeries
                    {
                        Title = parameter.Key,
                        Values = new GearedValues<DateTimePoint>(),
                        ScalesYAt = index,
                        PointGeometrySize = 0
                    });
                    totalSeriesCollection[index].Values.AddRange(new GearedValues<DateTimePoint>(dataTable.AsEnumerable().Select(row => new DateTimePoint
                    {
                        Value = row.Field<double>(parameter.Key),
                        DateTime = row.Field<DateTime>("TimeStamp")
                    })));
                }
            }
            string tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());

            CartesianChart cartesianChart = new CartesianChart
            {
                DisableAnimations = true,
                LegendLocation = LegendLocation.Top,
                Height = 500,
                Width = 650,
                AxisY = axisY,
                AxisX = new AxesCollection
                {
                    new Axis
                    {
                        Title = "t/min",
                        FontSize = 12,
                        Foreground = Brushes.Black,
                        LabelFormatter = val => new DateTime((long)val).ToString("dd/MMM HH:mm:ss"),
                        Separator = new LiveCharts.Wpf.Separator
                        {
                            StrokeThickness = 0
                        }
                    }
                },
                Series = totalSeriesCollection
            };

            Viewbox viewBox = new Viewbox
            {
                Child = cartesianChart
            };
            viewBox.Measure(cartesianChart.RenderSize);
            viewBox.Arrange(new Rect(new Point(0, 0), cartesianChart.RenderSize));
            cartesianChart.Update(true, true);
            viewBox.UpdateLayout();

            SaveToPng(cartesianChart, tempFilePath);
            return tempFilePath;
        }

        private double GetMaxValue(string value)
        {
            return Convert.ToDouble(value.Contains('|') ? value.Split('|')[1] : value);
        }

        private double GetMinValue(string value)
        {
            return Convert.ToDouble(value.Contains('|') ? value.Split('|')[0] : "0");
        }

        private void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            EncodeVisual(visual, fileName, encoder);
        }

        private static void EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
            using var stream = File.Create(fileName);
            encoder.Save(stream);
        }
    }
}
