using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.DataAbstractionLayer.Data;
using E3.ReactorManager.ParametersProvider.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Unity;
using WebEye.Controls.Wpf;

namespace E3.ReactorManager.VideoCaptureDevicesHandler.Model
{
    public class VideoCaptureDevicesHandler : IVideoCaptureDevicesHandler
    {
        private readonly IParametersProvider parametersProvider;
        private readonly WebCameraControl webCameraControl = new WebCameraControl();
        private Bitmap currentFrame;
        private readonly DispatcherTimer uiThreadTimer;
        private readonly IDatabaseWriter databaseWriter;
        private readonly IDatabaseReader databaseReader;

        public VideoCaptureDevicesHandler(IUnityContainer containerProvider, IDatabaseReader databaseReader, IDatabaseWriter databaseWriter)
        {
            uiThreadTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100),
            };
            uiThreadTimer.Tick += UiThreadTimer_Tick;
            this.databaseReader = databaseReader;
            this.databaseWriter = databaseWriter;

            if (containerProvider.IsRegistered<IParametersProvider>())
            {
                parametersProvider = containerProvider.Resolve<IParametersProvider>();
            }
        }

        private void UiThreadTimer_Tick(object sender, EventArgs e)
        {
            if (webCameraControl.IsCapturing)
            {
                LiveImageEventArgs liveImageEventArgs = new LiveImageEventArgs();
                try
                {
                    currentFrame = webCameraControl.GetCurrentImage();
                }
                catch (DirectShowException exception)
                {
                    Debug.WriteLine(exception.Message);
                    currentFrame = default;
                    liveImageEventArgs.ImageCaptureStatus = false;
                    webCameraControl.StopCapture();
                }
                if (liveImageEventArgs.ImageCaptureStatus)
                {
                    liveImageEventArgs.ImageSource = ConvertBitmap(currentFrame);
                }
                else
                {
                    liveImageEventArgs.ImageSource = default;
                }
                NewImageReady?.Invoke(this, liveImageEventArgs);
            }
            else
            {
                currentFrame = default;
            }
        }

        private BitmapImage ConvertBitmap(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            src.Save(ms, ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        public void CaptureImage(string fieldDeviceIdentifier)
        {
            Task.Factory.StartNew(() => {
                byte[] imgByteArray = GetImageInBytes(currentFrame);
                if (imgByteArray == null)
                {
                    //Skip.
                    MessageBox.Show("Unable to Capture Image. Please restart the application.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    IList<DbParameterInfo> dbParameters = new List<DbParameterInfo>
                    {
                        new DbParameterInfo("@FieldDeviceIdentifier", fieldDeviceIdentifier, DbType.String),
                        new DbParameterInfo("@ImageData", imgByteArray, DbType.Binary),
                    };

                    if (parametersProvider == null)
                    {
                        //Skip without adding the parameters data
                    }
                    else
                    {
                        Dictionary<string, string> parametersData = parametersProvider.GetFieldDeviceParametersWithTheirValues(fieldDeviceIdentifier);
                        parametersData.Add("TimeStamp", DateTime.Now.ToString("HH:mm:ss dd-MM-yyyy"));
                        BinaryFormatter binFormatter = new BinaryFormatter();
                        using (MemoryStream memStream = new MemoryStream())
                        {
                            binFormatter.Serialize(memStream, parametersData);
                            dbParameters.Add(new DbParameterInfo("@ParametersData", memStream.ToArray(), DbType.Binary));
                        }
                    }
                    databaseWriter.ExecuteWriteCommand("LogFieldDeviceImage", CommandType.StoredProcedure, dbParameters);
                }
            });
        }

        private byte[] GetImageInBytes(Bitmap src)
        {
            byte[] imgBytes = null;

            if (src != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    src.Save(memoryStream, ImageFormat.Png);
                    imgBytes = new byte[memoryStream.Length];
                    imgBytes = memoryStream.ToArray();
                }
            }

            return imgBytes;
        }

        public IEnumerable<WebCameraId> GetCaptureDevicesList()
        {
            return webCameraControl.GetVideoCaptureDevices();
        }

        public void StartCapturing(WebCameraId cameraId)
        {
            if (!webCameraControl.IsCapturing)
            {
                webCameraControl.StartCapture(cameraId);
                uiThreadTimer.Start();
            }
        }

        public void StopCapturing()
        {
            if (webCameraControl.IsCapturing)
            {
                webCameraControl.StopCapture();
                uiThreadTimer.Stop();
            }
        }

        public event LiveImageEventHandler NewImageReady;

    }
}
