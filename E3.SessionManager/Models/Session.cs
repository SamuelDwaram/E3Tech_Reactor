using System;

namespace E3.SessionManager.Models
{
    public class Session
    {
        public int Id { get; set; }
        public string TrainerName { get; set; }
        public string TraineeName { get; set; }
        public string TraineeRegion { get; set; }
        public string DeviceId { get; set; }
        public string Remarks { get; set; }
        public DateTime StartTimeStamp { get; set; }
        public DateTime EndTimeStamp { get; set; }
    }
}
