using E3.ReactorManager.ControllerProvider.Model.Interfaces;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3.ReactorManager.Interfaces.HardwareAbstractionLayer
{
    /// <summary>
    /// Field Device Data
    /// </summary>
    public class FieldDevice
    {
        /// <summary>
        /// Device Name
        /// </summary>
        public string Label
        {
            get; set;
        }

        /// <summary>
        /// Device Type
        /// </summary>
        public string Type
        {
            get; set;
        }

        /// <summary>
        /// Device Identifier/Tag
        /// </summary>
        public string Identifier
        {
            get; set;
        }

        /// <summary>
        /// Plc Identifier
        /// </summary>
        public Plc RelatedPlc { set; get; }

        /// <summary>
        /// List of the controllers connected to this field device
        /// </summary>
        public IList<IController> ConnectedControllers { get; set; }

        /// <summary>
        /// Parameters Data 
        /// </summary>
        public IList<SensorsDataSet> SensorsData
        {
            set; get;
        }

        /// <summary>
        /// Command Data 
        /// </summary>
        public IList<FieldPoint> CommandPoints
        {
            set; get;
        }

        /// <summary>
        /// Get Status of field Points
        /// </summary>
        public IList<FieldPoint> FieldPointsStatus
        {
            set; get;
        }
    }

    public class Device
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
    }
}
