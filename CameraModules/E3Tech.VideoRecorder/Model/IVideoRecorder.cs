using E3Tech.VideoRecorder.StreamSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Tech.VideoRecorder.Model
{
    public interface IVideoRecorder
    {
        void StartRecording(string fileName);

        void StopRecording();

        IVideoStreamSource StreamSource { get; }
    }
}
