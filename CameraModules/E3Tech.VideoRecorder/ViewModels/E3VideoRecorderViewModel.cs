using E3Tech.IO.FileAccess;
using E3Tech.VideoRecorder.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace E3Tech.VideoRecorder.ViewModels
{
    public class E3VideoRecorderViewModel : BindableBase
    {
        private readonly IVideoRecorder videoRecorder;
        private readonly IFileBrowser fileBrowser;
        private bool isRecording = false;

        public E3VideoRecorderViewModel(IFileBrowser fileBrowser, IVideoRecorder videoRecorder)
        {
            this.videoRecorder = videoRecorder;
            this.fileBrowser = fileBrowser;
            RecordVideoCommand = new DelegateCommand(new Action(StartRecord));
            StopRecordVideoCommand = new DelegateCommand(new Action(StopRecording));
        }

        private void StopRecording()
        {
            videoRecorder.StopRecording();
            isRecording = false;
        }

        private void StartRecord()
        {
            if (isRecording)
                return;
            string fileName = fileBrowser.SaveFile("Video1.avi", "avi");
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                videoRecorder.StartRecording(fileName);
                isRecording = true;
            }
        }

        private ICommand recordVideoCommand;
        public ICommand RecordVideoCommand
        {
            get => recordVideoCommand;
            set => recordVideoCommand = value;
        }

        private ICommand stopRecordingVideoCommand;
        public ICommand StopRecordVideoCommand
        {
            get => stopRecordingVideoCommand;
            set => stopRecordingVideoCommand = value;
        }
    }
}
