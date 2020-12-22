using E3.ReactorManager.ReportsManager.Model.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;

namespace E3.RemoteMonitoringSystem.UI.ViewModels
{
    public class ReportsViewModel : BindableBase
    {
        private readonly IReportPrinter reportPrinter;
        private readonly IRegionManager regionManager;

        public ReportsViewModel(IReportPrinter reportPrinter, IRegionManager regionManager)
        {
            this.reportPrinter = reportPrinter;
            this.regionManager = regionManager;
        }

        public void Navigate(string page)
        {
            regionManager.RequestNavigate("SelectedViewPane", page);
        }

        #region Commands
        public ICommand NavigateCommand
        {
            get => new DelegateCommand<string>(Navigate);
        }

        public ICommand ClearReportPreviewCommand
        {
            get => new DelegateCommand(() => reportPrinter.ClearReportPreview());
        }
        #endregion
    }
}
