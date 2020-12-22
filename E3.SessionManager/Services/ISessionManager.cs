using E3.SessionManager.Models;
using System.Collections.Generic;

namespace E3.SessionManager.Services
{
    public interface ISessionManager
    {
        int StartSession(string trainerName, string traineeName, string traineeRegion, string deviceId, string remarks);

        void EndSession(int id);

        IEnumerable<Session> GetAll();

        Session GetSession(int id);

        string DeviceId { get; }

        Session GetCurrentRunningSessionIfAny();

        IEnumerable<Session> GetSessionsInDevice();
    }
}
