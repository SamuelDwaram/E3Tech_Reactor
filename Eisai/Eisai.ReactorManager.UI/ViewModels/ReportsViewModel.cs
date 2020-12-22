using E3.ReactorManager.DesignExperiment.Model.Data;
using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using Eisai.ReactorManager.UI.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Eisai.ReactorManager.UI.ViewModels
{
    public class ReportsViewModel : BindableBase, IRegionMemberLifetime
    {
        private readonly IDatabaseReader databaseReader;
        private readonly IRegionManager regionManager;
        private readonly DevicesReportHandler devicesReportHandler;

        public ReportsViewModel(IDatabaseReader databaseReader, IRegionManager regionManager, DevicesReportHandler devicesReportHandler)
        {
            this.databaseReader = databaseReader;
            this.regionManager = regionManager;
            this.devicesReportHandler = devicesReportHandler;
        }

        private void PrintBatchReport()
        {
            Task.Factory.StartNew(() => {
                devicesReportHandler.PrintBatchReport(SelectedBatch, SelectedBatch.FieldDeviceIdentifier, SelectedBatch.FieldDeviceLabel, SelectedParameters, SelectedBatch.TimeStarted, SelectedBatch.TimeCompleted);
            });
        }

        public void GetCompletedBatches()
        {
            Task.Factory.StartNew(new Func<IList<Batch>>(devicesReportHandler.GetCompletedBatches))
                .ContinueWith(new Action<Task<IList<Batch>>>(t => CompletedBatches = t.Result));
        }

        private void GetFieldDeviceParameters()
        {
            string deviceId = SelectedBatch.FieldDeviceIdentifier;
            Task.Factory.StartNew(new Func<IList<string>>(() => {
                return (from DataRow row in databaseReader.ExecuteReadCommand($"select Label from dbo.FieldPoints where ToBeLogged='true' and SensorDataSetIdentifier in (select Identifier from dbo.SensorsDataSet where FieldDeviceIdentifier='{deviceId}')", CommandType.Text).AsEnumerable()
                        select Convert.ToString(row["Label"])).ToList();
            })).ContinueWith(new Action<Task<IList<string>>>(t => AvailableFieldDeviceParameters = t.Result));
        }

        public void AddToSelectedParameters(string parameter)
        {
            if (!SelectedParameters.Contains(parameter))
            {
                SelectedParameters.Add(parameter);
            }
        }

        public void RemoveFromSelectedParameters(string parameter)
        {
            if (SelectedParameters.Contains(parameter))
            {
                SelectedParameters.Remove(parameter);
            }
        }

        public void SelectBatch(Batch batch)
        {
            SelectedBatch = batch;
        }

        #region Commands
        private ICommand _addToSelectedParametersCommand;
        public ICommand AddToSelectedParametersCommand
        {
            get => _addToSelectedParametersCommand ?? (_addToSelectedParametersCommand = new DelegateCommand<string>(AddToSelectedParameters));
            set => _addToSelectedParametersCommand = value;
        }

        private ICommand _removeFromSelectedParametersCommand;
        public ICommand RemoveFromSelectedParametersCommand
        {
            get => _removeFromSelectedParametersCommand ?? (_removeFromSelectedParametersCommand = new DelegateCommand<string>(RemoveFromSelectedParameters));
            set => _removeFromSelectedParametersCommand = value;
        }

        private ICommand _getFieldDeviceParametersCommand;
        public ICommand GetFieldDeviceParametersCommand
        {
            get => _getFieldDeviceParametersCommand ?? (_getFieldDeviceParametersCommand = new DelegateCommand(GetFieldDeviceParameters));
            set => _getFieldDeviceParametersCommand = value;
        }

        private ICommand _selectBatchCommand;
        public ICommand SelectBatchCommand
        {
            get => _selectBatchCommand ?? (_selectBatchCommand = new DelegateCommand<Batch>(SelectBatch));
            set => _selectBatchCommand = value;
        }

        private ICommand _getCompletedBatchesCommand;
        public ICommand GetCompletedBatchesCommand
        {
            get => _getCompletedBatchesCommand ?? (_getCompletedBatchesCommand = new DelegateCommand(GetCompletedBatches));
            set => _getCompletedBatchesCommand = value;
        }

        private ICommand _printBatchReportCommand;
        public ICommand PrintBatchReportCommand
        {
            get => _printBatchReportCommand ?? (_printBatchReportCommand = new DelegateCommand(PrintBatchReport));
            set => _printBatchReportCommand = value;
        }

        public ICommand NavigateCommand
        {
            get => new DelegateCommand<string>(page => regionManager.RequestNavigate("SelectedViewPane", page));
        }
        #endregion

        #region Properties
        public bool KeepAlive => false;

        private IList<string> _selectedParameters;
        public IList<string> SelectedParameters
        {
            get => _selectedParameters ?? (_selectedParameters = new List<string>());
            set
            {
                _selectedParameters = value;
                RaisePropertyChanged();
            }
        }

        private IList<string> _availableFieldDeviceParameters;
        public IList<string> AvailableFieldDeviceParameters
        {
            get => _availableFieldDeviceParameters ?? (_availableFieldDeviceParameters = new List<string>());
            set
            {
                _availableFieldDeviceParameters = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _selectedStartDate;
        public DateTime SelectedStartDate
        {
            get => _selectedStartDate != default ? _selectedStartDate : (_selectedStartDate = DateTime.Now);
            set
            {
                _selectedStartDate = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _selectedEndDate;
        public DateTime SelectedEndDate
        {
            get => _selectedEndDate != default ? _selectedEndDate : (_selectedEndDate = DateTime.Now);
            set
            {
                _selectedEndDate = value;
                RaisePropertyChanged();
            }
        }

        private IList<Batch> _completedBatches;
        public IList<Batch> CompletedBatches
        {
            get => _completedBatches ?? (_completedBatches = new List<Batch>());
            set
            {
                _completedBatches = value;
                RaisePropertyChanged();
            }
        }

        private Batch _selectedBatch;
        public Batch SelectedBatch
        {
            get => _selectedBatch ?? (_selectedBatch = new Batch());
            set
            {
                _selectedBatch = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
