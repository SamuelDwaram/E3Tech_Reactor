using E3.SessionManager.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.DataAbstractionLayer.Data;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace E3.SessionManager.Services
{
    public class SessionManager : ISessionManager
    {
        private readonly IDatabaseReader databaseReader;
        private readonly IDatabaseWriter databaseWriter;
        private readonly string deviceId;
        private IList<Session> sessions = new List<Session>();

        public string DeviceId => deviceId;

        public SessionManager(IDatabaseReader databaseReader, IDatabaseWriter databaseWriter, string deviceId)
        {
            this.databaseReader = databaseReader;
            this.databaseWriter = databaseWriter;
            this.deviceId = deviceId;
            LoadSessions();
        }

        private void LoadSessions()
        {
            IList<DbParameterInfo> dbParameters = new List<DbParameterInfo>
            {
                new DbParameterInfo("DeviceId", deviceId, DbType.String)
            };

            sessions = (from DataRow row in databaseReader.ExecuteReadCommand("GetAllSessions", CommandType.StoredProcedure, dbParameters).AsEnumerable()
                        select new Session { 
                            Id = Convert.ToInt32(row["Id"]),
                            TrainerName = Convert.ToString(row["TrainerName"]),
                            TraineeName = Convert.ToString(row["TraineeName"]),
                            TraineeRegion = Convert.ToString(row["TraineeRegion"]),
                            DeviceId = Convert.ToString(row["DeviceId"]),
                            Remarks = Convert.ToString(row["Remarks"]),
                            StartTimeStamp = Convert.ToDateTime(row["StartTimeStamp"]),
                            EndTimeStamp = string.IsNullOrWhiteSpace(row["EndTimeStamp"].ToString()) ? default : Convert.ToDateTime(row["EndTimeStamp"])
                        }).ToList();
        }

        public int StartSession(string trainerName, string traineeName, string traineeRegion, string deviceId, string remarks)
        {
            IList<DbParameterInfo> dbParameters = new List<DbParameterInfo>
            {
                new DbParameterInfo("TrainerName", trainerName, DbType.String),
                new DbParameterInfo("TraineeName", traineeName, DbType.String),
                new DbParameterInfo("TraineeRegion", traineeRegion, DbType.String),
                new DbParameterInfo("DeviceId", deviceId, DbType.String),
                new DbParameterInfo("Remarks", remarks, DbType.String),
                new DbParameterInfo("StartTimeStamp", DateTime.Now, DbType.DateTime),
            };

            object id = databaseWriter.GetScalarValue("InsertSession", CommandType.StoredProcedure, dbParameters);
            sessions.Add(new Session { 
                Id = Convert.ToInt32(id),
                TrainerName = trainerName,
                TraineeName = traineeName,
                TraineeRegion = traineeRegion,
                DeviceId = DeviceId,
                Remarks = remarks,
                StartTimeStamp = Convert.ToDateTime(dbParameters.First(p => p.Name == "StartTimeStamp").Value)
            });
            return Convert.ToInt32(id);
        }

        public void EndSession(int id)
        {
            IList<DbParameterInfo> dbParameters = new List<DbParameterInfo>
            {
                new DbParameterInfo("Id", id, DbType.String),
                new DbParameterInfo("EndTimeStamp", DateTime.Now, DbType.DateTime),
            };
            databaseWriter.ExecuteWriteCommand("EndSession", CommandType.StoredProcedure, dbParameters);
            sessions.First(s => s.Id == id).EndTimeStamp = Convert.ToDateTime(dbParameters.First(p => p.Name == "EndTimeStamp").Value);
        }
        
        public IEnumerable<Session> GetAll()
        {
            return sessions;
        }

        public Session GetSession(int id)
        {
            IList<DbParameterInfo> dbParameters = new List<DbParameterInfo>
            {
                new DbParameterInfo("DeviceId", deviceId, DbType.String),
                new DbParameterInfo("SessionId", id, DbType.Int32)
            };
            return (from DataRow row in databaseReader.ExecuteReadCommand("GetSession", CommandType.StoredProcedure, dbParameters).AsEnumerable()
                    select new Session
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        DeviceId = Convert.ToString(row["DeviceId"]),
                        TrainerName = Convert.ToString(row["TrainerName"]),
                        TraineeName = Convert.ToString(row["TraineeName"]),
                        TraineeRegion = Convert.ToString(row["TraineeRegion"]),
                        Remarks = Convert.ToString(row["Remarks"]),
                        StartTimeStamp = Convert.ToDateTime(row["StartTimeStamp"]),
                        EndTimeStamp = Convert.ToDateTime(row["EndTimeStamp"]),
                    }).FirstOrDefault();
        }

        public Session GetCurrentRunningSessionIfAny()
        {
            return sessions.FirstOrDefault(s => s.EndTimeStamp == default) ?? null;
        }

        public IEnumerable<Session> GetSessionsInDevice()
        {
            IList<DbParameterInfo> dbParameters = new List<DbParameterInfo>
            {
                new DbParameterInfo("DeviceId", deviceId, DbType.String)
            };
            return (from DataRow row in databaseReader.ExecuteReadCommand("GetAllSessions", CommandType.StoredProcedure, dbParameters).AsEnumerable()
                    select new Session
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        DeviceId = Convert.ToString(row["DeviceId"]),
                        TrainerName = Convert.ToString(row["TrainerName"]),
                        TraineeName = Convert.ToString(row["TraineeName"]),
                        TraineeRegion = Convert.ToString(row["TraineeRegion"]),
                        Remarks = Convert.ToString(row["Remarks"]),
                        StartTimeStamp = Convert.ToDateTime(row["StartTimeStamp"]),
                        EndTimeStamp = Convert.ToDateTime(row["EndTimeStamp"]),
                    }).OrderByDescending(s => s.StartTimeStamp);
        }
    }
}
