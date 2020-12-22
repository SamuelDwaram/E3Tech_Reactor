using System.Collections.Generic;

namespace E3.ReactorManager.ParametersProvider.Model
{
    public interface IParametersProvider
    {
        Dictionary<string, string> GetFieldDeviceParametersWithTheirValues(string fieldDeviceIdentifier);

        IList<string> GetFieldDeviceRecordedParametersList(string fieldDeviceIdentifier);
    }
}
