using E3.ReactorManager.DataAbstractionLayer;
using E3.ReactorManager.EquipmentUsageTracker.Model.Data;
using E3.ReactorManager.EquipmentUsageTracker.Model.Interfaces;
using E3.ReactorManager.Framework;
using E3.ReactorManager.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.Framework.Logging;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace E3.ReactorManager.EquipmentUsageTracker
{
    public class EquipmentUsageTracker : IEquipmentUsageTracker
    {
        #region Fields
        ILogger logger;
        IDatabaseWriter databaseWriter;
        IDatabaseReader databaseReader;
        IFieldDevicesCommunicator fieldDevicesCommunicator;
        #endregion

        #region Constructors
        public EquipmentUsageTracker(IUnityContainer containerProvider)
        {
            databaseWriter = containerProvider.Resolve<IDatabaseWriter>();
            databaseReader = containerProvider.Resolve<IDatabaseReader>();
            fieldDevicesCommunicator = containerProvider.Resolve<IFieldDevicesCommunicator>();

            logger = containerProvider.Resolve<ILogger>();

            fieldDevicesCommunicator.FieldPointDataReceived += OnFieldPointDataReceived;
        }
        #endregion

        #region Live Data handlers
        private void OnFieldPointDataReceived(object sender, FieldPointDataReceivedArgs fieldPointDataChangedArgs)
        {
            Task.Factory.StartNew(new Func<object, FieldPointDataReceivedArgs>(ValidateLiveDataReceived), fieldPointDataChangedArgs)
                .ContinueWith(new Action<Task<FieldPointDataReceivedArgs>>(LogEquipmentUsageIfRequired));
        }

        private void LogEquipmentUsageIfRequired(Task<FieldPointDataReceivedArgs> task)
        {
            if (task.IsCompleted)
            {
                if (task.Result != null)
                {
                    LogEquipmentUsage(task.Result);
                }
            }
            else
            {
                if (task.IsFaulted)
                {
                    // display error to user
                }
            }
        }

        private FieldPointDataReceivedArgs ValidateLiveDataReceived(object arg)
        {
            var liveData = arg as FieldPointDataReceivedArgs;
            if (databaseWriter != null && liveData.FieldPointType.Equals("EquipmentUsageTracker"))
            {
                return liveData;
            }

            return null;
        }

        public void LogEquipmentUsage(FieldPointDataReceivedArgs fieldPointDataChangedArgs)
        {
            string equipmentDetails = fieldPointDataChangedArgs.FieldPointDescription;
            string equipmentName = equipmentDetails.Substring(0, equipmentDetails.IndexOf("_"));
            string parameterName = equipmentDetails.Substring(equipmentDetails.IndexOf("_") + 1);
            databaseWriter
                .LogEquipmentUsage(fieldPointDataChangedArgs.FieldDeviceIdentifier,
                                    equipmentName,
                                    parameterName,
                                    fieldPointDataChangedArgs.NewFieldPointData);
        }


        #endregion

        #region Generate Occupancy Report Data
        public IList<OccupancyReportArgs> GetOccupancyData(string fieldDeviceIdentifier, IList<string> equipmentIdentifierList, string month, OccupancyReportTypeEnum occupancyReportTypeEnum)
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, int.Parse(month), 1);
            var lastDayOfMonth = new DateTime(DateTime.Now.Year, int.Parse(month), DateTime.DaysInMonth(DateTime.Now.Year, int.Parse(month)));
            IList<OccupancyReportArgs> reportData = new List<OccupancyReportArgs>();
            double usedValue = 0;

            foreach (var equipmentIdentifier in equipmentIdentifierList)
            {
                OccupancyReportArgs occupancyReportArgs = default;

                var dataInDB
                    = databaseReader.GetEquipmentOccupancyData("OtherEquipment", equipmentIdentifier, firstDayOfMonth.ToString("yyyy-MM-dd"), lastDayOfMonth.ToString("yyyy-MM-dd"));
                /* Calculate the Used Value */
                if (dataInDB.Rows.Count > 0)
                {
                    switch (occupancyReportTypeEnum)
                    {
                        case OccupancyReportTypeEnum.Days:
                            /* Count only the Days where number of hours used > 0 */
                            usedValue = dataInDB.AsEnumerable().Where(x => double.Parse(x["NumberOfHoursUsed"].ToString()) > 0).Count();
                            break;
                        case OccupancyReportTypeEnum.Hours:
                            usedValue = dataInDB.AsEnumerable().Sum(x => double.Parse(x["NumberOfHoursUsed"].ToString()));
                            break;
                        default:
                            break;
                    }
                    if (occupancyReportArgs != null)
                    {
                        reportData.Add(occupancyReportArgs);
                    }
                }

                /* prepare the Occupancy Report Args with the calculated used value */
                switch (occupancyReportTypeEnum)
                {
                    case OccupancyReportTypeEnum.Days:
                        occupancyReportArgs = new OccupancyReportArgs
                        {
                            EquipmentIdentifier = equipmentIdentifier,
                            UsedValue = new ChartValues<double> { usedValue },
                            TotalUsableValue = new ChartValues<double> { lastDayOfMonth.Day },
                        };
                        break;
                    case OccupancyReportTypeEnum.Hours:
                        occupancyReportArgs = new OccupancyReportArgs
                        {
                            EquipmentIdentifier = equipmentIdentifier,
                            UsedValue = new ChartValues<double> { usedValue },
                            TotalUsableValue = new ChartValues<double> { 720 - usedValue },
                        };
                        break;
                    default:
                        break;
                }
                if (occupancyReportArgs != null)
                {
                    reportData.Add(occupancyReportArgs);
                }
            }

            return reportData;
        }

        #endregion

        #region Generate Equipment usage log Data

        public IList<EquipmentUsageLogArgs> GetEquipmentUsageLogData(string fieldDeviceIdentifier)
        {
            var dataTable = databaseReader.GetEquipmentUsageLogArgsData(fieldDeviceIdentifier);
            IList<EquipmentUsageLogArgs> equipmentUsageLogArgsList = new List<EquipmentUsageLogArgs>();

            if (dataTable.Rows.Count > 0)
            {
                int serialNumber = 0;
                equipmentUsageLogArgsList = (from DataRow dr in dataTable.Rows
                               select new EquipmentUsageLogArgs()
                               {
                                   SerialNumber = ++serialNumber,
                                   BatchNumber = dr["Number"].ToString(),
                                   ProjectName = dr["Name"].ToString(),
                                   FromDate = DateTime.Parse(dr["TimeStarted"].ToString()).ToString("yyyy-MM-dd"),
                                   ToDate = !string.IsNullOrWhiteSpace(dr["TimeCompleted"].ToString()) ? DateTime.Parse(dr["TimeCompleted"].ToString()).ToString("yyyy-MM-dd") : string.Empty,
                               }).ToList();
            }

            return equipmentUsageLogArgsList;
        }
        #endregion
    }
}
