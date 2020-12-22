using E3.ReactorManager.LiveGraphManager.Model.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace E3.ReactorManager.LiveGraphManager.ViewModels
{
    public class LiveGraphViewsContainerManagerViewModel : BindableBase
    {
        TaskScheduler taskScheduler;
        private readonly ILiveGraphViewsContainerManager liveGraphViewsContainerManager;

        public LiveGraphViewsContainerManagerViewModel(IUnityContainer containerProvider)
        {
            liveGraphViewsContainerManager = containerProvider.Resolve<ILiveGraphViewsContainerManager>();
        }

        private void UpdateLiveGraphViewDataContext(Task<LiveGraphViewModel> task)
        {
            if (task.IsCompleted)
            {
                LiveGraphViewDataContext = task.Result;
                RaisePropertyChanged("LiveGraphViewDataContext");
            }
            else
            {
                if (task.IsFaulted)
                {
                    // display error to user unable to load Live Graph view 
                }
            }
        }

        public void PageLoaded(string deviceId)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Factory.StartNew(new Func<object, LiveGraphViewModel>(GetLiveGraphViewDataContext), deviceId)
                .ContinueWith(new Action<Task<LiveGraphViewModel>>(UpdateLiveGraphViewDataContext), taskScheduler);
        }

        private LiveGraphViewModel GetLiveGraphViewDataContext(object arg)
        {
            string deviceId = (string)arg;
            LiveGraphViewModel liveGraphViewModel = null;

            if (liveGraphViewsContainerManager.IsDeviceIdValid(deviceId))
            {
                /* Add the DeviceId to the LiveGraphViewsContainer */
                bool isDeviceIdValid = liveGraphViewsContainerManager.AddLiveGraphView(deviceId);

                if (isDeviceIdValid)
                {
                    /* Update the LiveGraphViewDataContext */
                    liveGraphViewModel = liveGraphViewsContainerManager.ResoveLiveGraphView(deviceId);
                }
            }
            else
            {
                /* Update the LiveGraphViewDataContext from the LiveGraphViewsContainer in liveGraphManager */
                liveGraphViewModel = liveGraphViewsContainerManager.ResoveLiveGraphView(deviceId);
            }

            return liveGraphViewModel;
        }

        private LiveGraphViewModel liveGraphViewDataContext;
        public LiveGraphViewModel LiveGraphViewDataContext
        {
            get => liveGraphViewDataContext;
            set
            {
                liveGraphViewDataContext = value;
                RaisePropertyChanged();
            }
        }

        #region Commands
        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand<string>(new Action<string>(PageLoaded)));
            set
            {
                _loadedCommand = value;
                RaisePropertyChanged();
            }
        }
        #endregion

    }
}
