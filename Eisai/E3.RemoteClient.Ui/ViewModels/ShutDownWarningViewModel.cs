using Prism.Commands;
using Prism.Mvvm;
using System.Timers;
using System.Windows.Input;

namespace E3.RemoteClient.Ui.ViewModels
{
    public class ShutDownWarningViewModel : BindableBase
    {
        private readonly Timer timer = new Timer(1000);

        public ShutDownWarningViewModel()
        {
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            --TimeLeft;
            if (TimeLeft==0)
            {
                timer.Stop();
                ShutDown();
            }
        }

        public ICommand ShutDownCommand
        {
            get => new DelegateCommand(ShutDown);
        }

        private void ShutDown()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private int _timeLeft=10;
        public int TimeLeft
        {
            get { return _timeLeft; }
            set { SetProperty(ref _timeLeft, value); }
        }
    }
}
