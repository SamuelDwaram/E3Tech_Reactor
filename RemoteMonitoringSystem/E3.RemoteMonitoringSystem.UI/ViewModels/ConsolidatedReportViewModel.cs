using E3.RemoteMonitoringSystem.UI.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace E3.RemoteMonitoringSystem.UI.ViewModels
{
    public class ConsolidatedReportViewModel : BindableBase
    {
        private readonly DevicesReportHandler devicesReportHandler;

        public ConsolidatedReportViewModel(DevicesReportHandler devicesReportHandler)
        {
            this.devicesReportHandler = devicesReportHandler;
        }

        private void PrintReport()
        {
            Task.Factory.StartNew(() => devicesReportHandler.PrintConsolidatedReport(SelectedStartDate, SelectedEndDate));
        }

        private ICommand _printReportCommand;
        public ICommand PrintReportCommand
        {
            get => _printReportCommand ?? (_printReportCommand = new DelegateCommand(PrintReport));
            set => SetProperty(ref _printReportCommand, value);
        }

        #region Properties
        private DateTime _selectedStartDate;
        public DateTime SelectedStartDate
        {
            get => _selectedStartDate == default ? DateTime.Now : _selectedStartDate;
            set => SetProperty(ref _selectedStartDate, value);
        }

        private DateTime _selectedEndDate;
        public DateTime SelectedEndDate
        {
            get => _selectedEndDate == default ? DateTime.Now : _selectedEndDate;
            set => SetProperty(ref _selectedEndDate, value);
        }
        #endregion
    }
}
