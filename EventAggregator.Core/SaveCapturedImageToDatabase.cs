using Prism.Events;
using System;
using System.Drawing;

namespace EventAggregator.Core
{
    public class SaveCapturedImageToDatabase : PubSubEvent<CapturedImageArgs>
    {
        // Saves the image to database 
        // This event is subscribed only in Data Abstraction Layer
    }

    public class CapturedImageArgs : EventArgs
    {
        public Bitmap ImageBitmap { get; set; }

        public string DeviceId { get; set; }
    }
}
