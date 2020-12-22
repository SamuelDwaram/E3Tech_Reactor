using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.ReportsManager.Model.Data;
using E3.ReactorManager.ReportsManager.Model.Interfaces;
using E3.SessionManager.Models;
using E3.SessionManager.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;
using Unity.Resolution;

namespace EcoMotorPanel.UI.ViewModels
{
    public class ExistingSessionViewModel : BindableBase
    {
        private readonly ISessionManager sessionManager;
        private readonly TaskScheduler taskScheduler;
        private readonly IReportPrinter reportPrinter;
        private readonly IDatabaseReader databaseReader;
        private readonly IRegionManager regionManager;

        public ExistingSessionViewModel(IUnityContainer unityContainer, IReportPrinter reportPrinter, IDatabaseReader databaseReader, IRegionManager regionManager)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            sessionManager = unityContainer.Resolve<ISessionManager>(new ResolverOverride[] {
                new ParameterOverride("deviceId", DeviceId)
            });
            this.reportPrinter = reportPrinter;
            this.databaseReader = databaseReader;
            this.regionManager = regionManager;
        }

        public void Loaded()
        {
            Task.Factory.StartNew(new Func<IEnumerable<Session>>(sessionManager.GetSessionsInDevice))
                .ContinueWith((t) => Sessions = t.Result, taskScheduler);
        }


        private void DownloadReport(object id)
        {
            regionManager.RequestNavigate("DynamicView", "ReportPreview");
            Session session = sessionManager.GetSession((int)id);
            IList<LabelValuePair> labelValuePairs = new List<LabelValuePair>
            {
                new LabelValuePair("Trainer", session.TrainerName),
                new LabelValuePair("Trainee", session.TraineeName),
                new LabelValuePair("Region", session.TraineeRegion),
                new LabelValuePair("StartTime", session.StartTimeStamp.ToString("HH:mm:ss dd-MM-yyyy")),
                new LabelValuePair("EndTime", session.EndTimeStamp.ToString("HH:mm:ss dd-MM-yyyy")),
            };
            ReportSection sessionInfo = new ReportSection
            {
                Title = "Session Info",
                Data = labelValuePairs,
                DataType = SectionalDataType.LabelValuePairs,
                EndPageHere = false
            };

            ReportSection actionRecord = new ReportSection
            {
                Title = "Action Record",
                Data = databaseReader.ExecuteReadCommand($"select ActionDescription as [Action Description], Rpm as Rpm, format(TimeStamp, 'HH:mm:ss dd-MM-yyyy') as [Time Stamp] from dbo.ActionRecord where TimeStamp between '{session.StartTimeStamp:yyyy-MM-dd HH:mm:ss}' and '{session.EndTimeStamp:yyyy-MM-dd HH:mm:ss}' order by TimeStamp asc", CommandType.Text),
                DataType = SectionalDataType.Tablular,
                EndPageHere = false
            };
            reportPrinter.PrintReportSections("ECO MOTOR REPORT", new List<ReportSection> {
                sessionInfo, actionRecord
            });
        }

        #region Commands
        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand(Loaded));
            set => SetProperty(ref _loadedCommand, value);
        }

        private ICommand _downloadReportCommand;
        public ICommand DownloadReportCommand
        {
            get => _downloadReportCommand ?? (_downloadReportCommand = new DelegateCommand<object>(DownloadReport));
            set { SetProperty(ref _downloadReportCommand, value); }
        }
        #endregion

        #region Properties
        private string _deviceId;
        public string DeviceId
        {
            get => _deviceId ?? (_deviceId = "Motor_2");
            set { SetProperty(ref _deviceId, value); }
        }

        private IEnumerable<Session> _sessions;
        public IEnumerable<Session> Sessions
        {
            get => _sessions ?? (_sessions = new List<Session>());
            set => SetProperty(ref _sessions, value);
        }
        #endregion

    }
}
