using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace E3Tech.VideoPlayer.Helpers
{
    public class MediaPlayPositionTracker : BindableBase
    {
        private readonly DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(100) };

        public MediaPlayPositionTracker()
        {
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            if (this.MediaElement == null || DesignerProperties.GetIsInDesignMode(this.MediaElement))
            {
                timer.Stop();
                return;
            }

            if (MediaElement.IsLoaded)
            {
                Minimum = 0;

                if (this.MediaElement.NaturalDuration != null && MediaElement.NaturalDuration != Duration.Automatic)
                {
                    Maximum = Convert.ToInt32(this.MediaElement.NaturalDuration.TimeSpan.TotalSeconds);
                }
                positionInSeconds = Convert.ToInt32(MediaElement.Position.TotalSeconds);
                RaisePropertyChanged("PositionInSeconds");
                PositionToDisplay = MediaElement.Position.ToString($"hh\\:mm\\:ss");
            }
        }

        private string positionToDisplay;
        public string PositionToDisplay
        {
            get => positionToDisplay;
            set => SetProperty<string>(ref positionToDisplay, value);//MediaElement.Position = TimeSpan.Parse(value);
        }

        public MediaElement MediaElement { get; set; }

        private int minimum;
        public int Minimum
        {
            get => minimum;
            set => SetProperty<int>(ref minimum, value);
        }

        private int maximum;
        public int Maximum
        {
            get => maximum;
            set => SetProperty<int>(ref maximum, value);
        }

        private int positionInSeconds;
        public int PositionInSeconds
        {
            get => positionInSeconds;
            set
            {
                MediaElement.Position = TimeSpan.FromSeconds(value);
                SetProperty<int>(ref positionInSeconds, value);
            }
        }
    }
}
