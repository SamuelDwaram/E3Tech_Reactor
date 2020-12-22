using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using System.Collections.Generic;
using Unity;

namespace E3.ReactorManager.ParametersProvider.Model
{
    public class DefaultParametersProvider : IParametersProvider
    {
        IFieldDevicesCommunicator fieldDevicesCommunicator;

        public DefaultParametersProvider(IUnityContainer containerProvider)
        {
            fieldDevicesCommunicator = containerProvider.Resolve<IFieldDevicesCommunicator>();
        }

        public Dictionary<string, string> GetFieldDeviceParametersWithTheirValues(string fieldDeviceIdentifier)
        {
            Dictionary<string, string> parametersData = new Dictionary<string, string>();
            var fieldDeviceData = fieldDevicesCommunicator.GetFieldDeviceData(fieldDeviceIdentifier);

            foreach (var sensorDataSet in fieldDeviceData.SensorsData)
            {
                foreach (var fieldPoint in sensorDataSet.SensorsFieldPoints)
                {
                    if (fieldPoint.ToBeLogged)
                    {
                        //Add the field points which are logged in database to parameters dictionary
                        parametersData.Add(fieldPoint.Label, fieldPoint.Value);
                    }
                }
            }

            return parametersData;
        }

        public IList<string> GetFieldDeviceRecordedParametersList(string fieldDeviceIdentifier)
        {
            IList<string> parametersList = new List<string>();
            FieldDevice fieldDeviceData = fieldDevicesCommunicator.GetFieldDeviceData(fieldDeviceIdentifier);
            
            if (fieldDeviceData != null)
            {
                foreach (var sensorDataSet in fieldDeviceData.SensorsData)
                {
                    foreach (var fieldPoint in sensorDataSet.SensorsFieldPoints)
                    {
                        if (fieldPoint.ToBeLogged)
                        {
                            //Add the field points which are logged in database to parameters List
                            parametersList.Add(fieldPoint.Label);
                        }
                    }
                }

            }
            return parametersList;
        }
    }
}
