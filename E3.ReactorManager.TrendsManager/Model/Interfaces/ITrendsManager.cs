using System;
using System.Collections.Generic;
using System.Data;

namespace E3.ReactorManager.TrendsManager.Model.Interfaces
{
    public interface ITrendsManager
    {
        void SetTrendsParameters(IList<string> parameters, DateTime startDate, DateTime endDate);

        Dictionary<string, string> GetAvailableFieldDevices();

        Dictionary<string, dynamic> GetTrendsData(object deviceId);

        string PrepareTrendsImageForGivenData(DataTable dataTable, Dictionary<string, string> parametersInfo);
    }
}
