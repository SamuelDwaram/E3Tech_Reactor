using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using E3.EquipmentUsageTracker.Model.Data;
using E3.EquipmentUsageTracker.Model.Enums;
using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.DataAbstractionLayer.Data;
using E3.ReactorManager.Interfaces.Framework.Logging;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using LiveCharts;
using Unity;

namespace E3.EquipmentUsageTracker.Model
{
    public class EquipmentUsageTracker : IEquipmentUsageTracker
    {
        IDatabaseReader databaseReader;
        IDatabaseWriter databaseWriter;
        IFieldDevicesCommunicator fieldDevicesCommunicator;
        ILogger logger;

        public EquipmentUsageTracker(IUnityContainer containerProvider)
        {
            databaseWriter = containerProvider.Resolve<IDatabaseWriter>();
            databaseReader = containerProvider.Resolve<IDatabaseReader>();
            logger = containerProvider.Resolve<ILogger>();
            fieldDevicesCommunicator = containerProvider.Resolve<IFieldDevicesCommunicator>();
            fieldDevicesCommunicator.FieldPointDataReceived += OnFieldPointDataReceived;
        }

        #region Live Data handlers
        private void OnFieldPointDataReceived(object sender, FieldPointDataReceivedArgs fieldPointDataChangedArgs)
        {
            Task.Factory.StartNew(new Func<object, FieldPointDataReceivedArgs>(ValidateLiveDataReceived), fieldPointDataChangedArgs)
                .ContinueWith(new Action<Task<FieldPointDataReceivedArgs>>((task) => {
                    if (task.Result == null)
                    {
                        // Skip. Not EquipmentUsageTracker field point
                    }
                    else
                    {
                        LogEquipmentUsage(task.Result);
                    }
                }));
        }

        private FieldPointDataReceivedArgs ValidateLiveDataReceived(object arg)
        {
            var liveData = arg as FieldPointDataReceivedArgs;
            return liveData.FieldPointType.Equals("EquipmentUsageTracker") ? liveData : null;
        }
        #endregion

        public IList<EquipmentAndConnectedFieldDeviceArgs> GetAvailableEquipments()
        {
            return (from DataRow row in databaseReader.ExecuteReadCommand("GetAvailableEquipments", CommandType.StoredProcedure).AsEnumerable()
                    select new EquipmentAndConnectedFieldDeviceArgs()
                    {
                        EquipmentIdentifier = row["EquipmentIdentifier"].ToString(),
                        FieldDeviceConnectedTo = row["FieldDeviceIdentifier"].ToString(),
                        FieldDeviceLabel = fieldDevicesCommunicator.GetFieldDeviceLabel(row["FieldDeviceIdentifier"].ToString())
                    }).ToList();
        }

        public EquipmentOccupancyArgs GetEquipmentOccupancy(string fieldDeviceIdentifier, string equipmentIdentifier, string month, OccupancyReportTypeEnum occupancyReportTypeEnum)
        {
            double usedValue = 0;
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, int.Parse(month), 1);
            DateTime lastDayOfMonth = new DateTime(DateTime.Now.Year, int.Parse(month), DateTime.DaysInMonth(DateTime.Now.Year, int.Parse(month)));

            IList<DbParameterInfo> parameters = new List<DbParameterInfo>
            {
                new DbParameterInfo("@FieldDeviceIdentifier", fieldDeviceIdentifier, DbType.String),
                new DbParameterInfo("@EquipmentIdentifier", equipmentIdentifier, DbType.String),
                new DbParameterInfo("@StartDate", firstDayOfMonth.ToString("yyyy-MM-dd"), DbType.String),
                new DbParameterInfo("@EndDate", lastDayOfMonth.ToString("yyyy-MM-dd"), DbType.String),
            };

            EquipmentOccupancyArgs equipmentOccupancyArgs = default;
            DataTable dataFromDB = databaseReader.ExecuteReadCommand("GetEquipmentOccupancy", CommandType.StoredProcedure, parameters);

            /* Calculate the Used Value */
            if (dataFromDB.Rows.Count > 0)
            {
                switch (occupancyReportTypeEnum)
                {
                    case OccupancyReportTypeEnum.Days:
                        /* Count only the Days where number of hours used > 0 */
                        usedValue = dataFromDB.AsEnumerable().Where(x => double.Parse(x["NumberOfHoursUsed"].ToString()) > 0).Count();
                        break;
                    case OccupancyReportTypeEnum.Hours:
                        usedValue = dataFromDB.AsEnumerable().Sum(x => double.Parse(x["NumberOfHoursUsed"].ToString()));
                        break;
                    default:
                        break;
                }
            }

            /* prepare the EquipmentOccupancyArgs with the calculated used value */
            equipmentOccupancyArgs = new EquipmentOccupancyArgs
            {
                EquipmentIdentifier = equipmentIdentifier,
                ConnectedDeviceId = fieldDeviceIdentifier,
                UsedValue = new ChartValues<double> { usedValue },
                TotalUsableValue = occupancyReportTypeEnum == OccupancyReportTypeEnum.Days ? new ChartValues<double> { lastDayOfMonth.Day } : new ChartValues<double> { 720 - usedValue },
            };

            return equipmentOccupancyArgs;
        }

        public IList<EquipmentUsageLogArgs> GetEquipmentUsageLog(string fieldDeviceIdentifier)
        {
            IList<DbParameterInfo> parameters = new List<DbParameterInfo>
            {
                new DbParameterInfo("@FieldDeviceIdentifier", fieldDeviceIdentifier, DbType.String)
            };
            int serialNumber = 0;
            return (from DataRow row in databaseReader.ExecuteReadCommand("GetEquipmentUsageLog", CommandType.StoredProcedure, parameters).AsEnumerable()
                    select new EquipmentUsageLogArgs()
                    {
                        SerialNumber = ++serialNumber,
                        DeviceId = row["FieldDeviceIdentifier"].ToString(),
                        BatchNumber = row["Number"].ToString(),
                        ProjectName = row["Name"].ToString(),
                        FromDate = DateTime.Parse(row["TimeStarted"].ToString()).ToString("yyyy-MM-dd"),
                        ToDate = !string.IsNullOrWhiteSpace(row["TimeCompleted"].ToString()) ? DateTime.Parse(row["TimeCompleted"].ToString()).ToString("yyyy-MM-dd") : string.Empty,
                    }).ToList();
        }

        public void LogEquipmentUsage(FieldPointDataReceivedArgs fieldPointDataChangedArgs)
        {
            string equipmentDetails = fieldPointDataChangedArgs.FieldPointDescription;
            string equipmentName = equipmentDetails.Substring(0, equipmentDetails.IndexOf("_"));
            string parameterName = equipmentDetails.Substring(equipmentDetails.IndexOf("_") + 1);

            IList<DbParameterInfo> parameters = new List<DbParameterInfo>
            {
                new DbParameterInfo("@FieldDeviceIdentifier", fieldPointDataChangedArgs.FieldDeviceIdentifier, DbType.String),
                new DbParameterInfo("@EquipmentIdentifier", equipmentName, DbType.String),
                new DbParameterInfo("@ParameterName", parameterName, DbType.String),
                new DbParameterInfo("@ParameterValue", fieldPointDataChangedArgs.NewFieldPointData, DbType.String),
            };
            
            databaseWriter.ExecuteWriteCommand("LogEquipmentOccupancy", CommandType.StoredProcedure, parameters);
            logger.Log(LogType.Information, "Logging "+ equipmentName + " " + parameterName + " = " + fieldPointDataChangedArgs.NewFieldPointData);
        }

        public void PublishUpdateEquipmentOccupancyEvent(string month, OccupancyReportTypeEnum occupancyReportTypeEnum)
        {
            UpdateEquipmentOccupancyEvent?.Invoke(month, occupancyReportTypeEnum);
        }

        public event UpdateEquipmentOccupancyEventHandler UpdateEquipmentOccupancyEvent;

    }
}
