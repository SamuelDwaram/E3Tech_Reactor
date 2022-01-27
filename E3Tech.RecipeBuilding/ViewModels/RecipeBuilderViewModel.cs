using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using E3Tech.RecipeBuilding.Helpers;
using E3Tech.RecipeBuilding.Model;
using E3Tech.RecipeBuilding.Model.Blocks;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Unity;
using E3Tech.RecipeBuilding.Model.RecipeExecutionInfoProvider;
using Prism.Regions;

namespace E3Tech.RecipeBuilding.ViewModels
{
    public class RecipeBuilderViewModel : BindableBase
    {
        private readonly IRecipeBuilder recipeBuilder;
        private readonly IRecipeReloader recipeReloader;
        private readonly IRecipeExecutor recipeExecutor;
        private readonly IUnityContainer containerProvider;
        private RecipeStepViewModel selectedStep;
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;
        private readonly List<PropertyInfo> existingProperties = new List<PropertyInfo>(typeof(RecipeBuilderViewModel).GetProperties());
        private readonly TaskScheduler taskScheduler;
        private readonly IRecipeExecutionInfoProvider recipeExecutionInfoProvider;

        public RecipeBuilderViewModel(IUnityContainer containerProvider, IRecipeExecutor recipeExecutor, IFieldDevicesCommunicator fieldDevicesCommunicator, IRecipeBuilder recipeBuilder, IRecipeReloader recipeReloader)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.recipeExecutor = recipeExecutor;
            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
            fieldDevicesCommunicator.FieldPointDataReceived += OnLiveDataReceived;
            recipeExecutionInfoProvider = containerProvider.Resolve<IRecipeExecutionInfoProvider>();
            this.containerProvider = containerProvider;
            this.recipeBuilder = recipeBuilder;
            this.recipeReloader = recipeReloader;
            LoadRegisteredBlocks(containerProvider);
            RecipeSteps = new ObservableCollection<RecipeStepViewModel>();
            LoadSteps();
        }

        #region Initialization of Recipe Steps and Registered Recipe Blocks
        private void LoadSteps()
        {
            /*
             * If DeviceId has not started any Recipe and application was running fine
             * then Load recipe from Recipe Builder
             */
            foreach (RecipeStep step in recipeBuilder.RecipeSteps)
            {
                RecipeSteps.Add(new RecipeStepViewModel(containerProvider) { RecipeStep = step });
            }
            //UpdateRecipeParameters();
        }
        private void LoadRegisteredBlocks(IUnityContainer containerProvider)
        {
            AvailableBlocks = new List<IRecipeBlock>();
            //Get all the recipe blocks from the container except Fill and Transfer blocks
            IEnumerable<IRecipeBlock> blocks = containerProvider.ResolveAll<IRecipeBlock>();

            if (blocks.Count() > 0)
            {
                /*
                 * Check if the recipe blocks are loaded in the UnityContainer
                 * and Update Available Blocks with the List returned from the UnityContainer
                 */
                AvailableBlocks = new List<IRecipeBlock>(blocks);
            }
        }
        #endregion

        #region Live Data Handlers
        private void UpdatePropertyValue(Task<LiveDataEventArgs> task)
        {
            var liveDataEventArgs = task.Result;

            if (liveDataEventArgs != null && liveDataEventArgs.PropertyInfo != null && liveDataEventArgs.LiveData != null)
            {
                liveDataEventArgs.PropertyInfo.SetValue(this, liveDataEventArgs.LiveData, null);
            }
        }

        private void OnLiveDataReceived(object sender, FieldPointDataReceivedArgs fieldPointDataChangedArgs)
        {
            UpdateRecipeParameters();
            if (fieldPointDataChangedArgs.FieldDeviceIdentifier == DeviceId)
            {
                var liveDataEventArgs = new LiveDataEventArgs
                {
                    PropertyInfoIdentifier = fieldPointDataChangedArgs.FieldPointDescription,
                    LiveData = fieldPointDataChangedArgs.NewFieldPointData,
                };

                Task.Factory.StartNew(new Func<object, LiveDataEventArgs>(ValidateLiveDataReceived), liveDataEventArgs)
                    .ContinueWith(new Action<Task<LiveDataEventArgs>>(UpdatePropertyValue), taskScheduler);
            }
        }

        private LiveDataEventArgs ValidateLiveDataReceived(object liveData)
        {
            if (liveData != null)
            {
                var liveDataEventArgs = (LiveDataEventArgs)liveData;

                liveDataEventArgs.PropertyInfo
                    = existingProperties.FirstOrDefault(property => property.Name == liveDataEventArgs.PropertyInfoIdentifier);

                return liveDataEventArgs;
            }

            return null;
        }
        #endregion

        public void UpdateRecipeParameters()
        {
            RecipeStatus = fieldDevicesCommunicator.ReadFieldPointValue<bool>(DeviceId, "RecipeStatus").ToString();
            RecipeEnded = fieldDevicesCommunicator.ReadFieldPointValue<bool>(DeviceId, "RecipeEnded").ToString();
            RecipePaused = fieldDevicesCommunicator.ReadFieldPointValue<bool>(DeviceId, "PauseRecipe").ToString();
            DrainStatus = fieldDevicesCommunicator.ReadFieldPointValue<bool>(DeviceId, "DrainStatus").ToString();
        }

        public void UpdateNavigationParameters(NavigationParameters NavigationParameters)
        {
            this.NavigationParameters = NavigationParameters;
        }

        #region Export & Import Recipe
        private void ExportRecipe()
        {
            recipeBuilder.Export();
        }

        private void ImportRecipe()
        {
            RecipeStep[] recipeSteps = recipeBuilder.Import();
            if (recipeSteps != null)
            {
                RecipeSteps.Clear();
                foreach (RecipeStep step in recipeSteps)
                {
                    RecipeStepViewModel stepViewModel = containerProvider.Resolve<RecipeStepViewModel>();
                    stepViewModel.RecipeStep = step;
                    RecipeSteps.Add(stepViewModel);
                }
            }
            else
            {
                MessageBox.Show("No data in the selected file", "Import Recipe Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Start Recipe
        private void StartRecipe()
        {
            if (recipeBuilder.CheckEndBlockInRecipe(recipeBuilder.RecipeSteps))
            {
                recipeExecutor.Execute(DeviceId, recipeBuilder.RecipeSteps);
            }
            else
            {
                MessageBox.Show("Please Add End Block in the Recipe",
                                "Recipe Execution Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void PauseRecipe()
        {
            fieldDevicesCommunicator
               .SendCommandToDevice(DeviceId,
                                          "PauseRecipe",
                                          "bool",
                                          Boolean.TrueString);
        }
        
        private void ResumeRecipe()
        {
            fieldDevicesCommunicator
               .SendCommandToDevice(DeviceId,
                                          "PauseRecipe",
                                          "bool",
                                          Boolean.FalseString);
        }
        private void SkipDrain()
        {
            fieldDevicesCommunicator
               .SendCommandToDevice(DeviceId,
                                          "SkipDrain",
                                          "bool",
                                          Boolean.TrueString);
        }

        private bool CanStartRecipe()
        {
            return recipeExecutor != null;
        }
        #endregion

        #region Clear Recipe
        public void ClearRecipe()
        {
            recipeBuilder.Clear();
            RecipeSteps.Clear();
            recipeExecutor.ClearRecipe(DeviceId);
            fieldDevicesCommunicator
              .SendCommandToDevice(DeviceId,
                                         "ClearRecipe",
                                         "bool",
                                         Boolean.TrueString);
        }

        private bool CanClearRecipe()
        {
            return recipeExecutor != null;
        }
        #endregion

        private void UpdateBlockExecution(IRecipeBlock block)
        {
            recipeExecutor.UpdateBlock(RecipeSteps.IndexOf(SelectedStep), block, DeviceId);
        }

        private void HandleDeleteStep(RecipeStepViewModel stepViewModel)
        {
            if (stepViewModel != null)
            {
                if (recipeBuilder.RemoveStep(stepViewModel.RecipeStep))
                {
                    RecipeSteps.Remove(stepViewModel);
                }
            }
        }

        private void HandleDeleteBlock(IRecipeBlock obj)
        {
            if (obj != null)
            {
                recipeBuilder.RemoveBlockFromStep(SelectedStep.RecipeStep, obj);
            }
        }

        #region Abort Recipe & Abort RecipeBlock Execution
        public void AbortBlockExecution(IRecipeBlock block)
        {
            if (block != null && SelectedStep != null)
            {
                recipeExecutor.AbortBlockExecution(RecipeSteps.IndexOf(SelectedStep), block, DeviceId);
            }
        }

        public void AbortRecipeExecution()
        {
            recipeExecutor.AbortRecipeExecution(DeviceId);
        }

        private bool CanAbortRecipeExecution()
        {
            return bool.Parse(RecipeStatus);
        }
        #endregion

        public void SetDeviceId(string deviceId)
        {
            this.DeviceId = deviceId;
            //update Field Device Label
            FieldDeviceLabel = fieldDevicesCommunicator.GetFieldDeviceLabel(deviceId);

            if (RecipeSteps.Count == 0)
            {
                /*
                 * If RecipeSteps count was zero check RecipeStatus and RecipeEndedStatus
                 * and Reload Recipe Steps if required
                 */
                Task.Factory.StartNew(new Func<object, bool>(IsReloadRecipeActionRequired), deviceId)
                    .ContinueWith(new Action<Task<bool>>(ReloadRecipeSteps));
            }
        }

        private void ReloadRecipeSteps(Task<bool> task)
        {
            if (task.IsCompleted)
            {
                /* Update Recipe Status */
                this.RecipeStatus = task.Result.ToString();

                if (task.Result)
                {
                    recipeBuilder.ReloadRecipeSteps(new Action<Task>((t) => LoadSteps()), taskScheduler);
                }
            }
        }

        private bool IsReloadRecipeActionRequired(object arg)
        {
            bool recipeStatus = recipeReloader.GetRecipeStatus((string)arg);
            bool recipeEndedStatus = recipeReloader.GetRecipeEndedStatus((string)arg);

            return recipeStatus || recipeEndedStatus;
        }

        private bool IsRecipeEnded() => !bool.Parse(RecipeStatus);

        private void ShowErrorNotificationToUser() => MessageBox.Show("You are allowed to give only 'Clear Recipe' Command", "Invalid Command", MessageBoxButton.OK, MessageBoxImage.Error);

        public void SaveRecipeExecution()
        {
            Task.Factory.StartNew(() => {
                recipeExecutionInfoProvider.SaveRecipeExecutionInfo(RecipeSteps.Select(step => step.RecipeStep).ToList(), DeviceId);
            });
        }

        #region Drag & Drop
        private IRecipeBlock GetBlockInstance(IDataObject dropData)
        {
            IRecipeBlock block = containerProvider.Resolve(dropData.GetData("PersistentObject").GetType()) as IRecipeBlock;
            block.Name = dropData.GetData("Text").ToString();
            return block;
        }

        private void HandleDropOnCell(DataGridCellDropCommandParameters parameters)
        {
            if (IsRecipeEnded())
            {
                IRecipeBlock block = GetBlockInstance(parameters.DataObject);
                int recipeStepIndex = RecipeSteps.IndexOf(parameters.DataContext as RecipeStepViewModel);
                if (IsDropBlockAllowed(block, recipeStepIndex))
                {
                    bool isBlockValid = false;
                    if (RecipeSteps.Count == 1 && block.Name == "Start")
                    {
                        isBlockValid = recipeBuilder.AddNewStep(RecipeSteps.First().RecipeStep, block);
                    }
                    else
                    {
                        isBlockValid = recipeBuilder.UpdateStep(RecipeSteps[recipeStepIndex].RecipeStep, parameters.Block, block);
                    }

                    if (isBlockValid)
                    {
                        block.Configure(containerProvider);
                        UpdateRecipeToExecuter(block, recipeStepIndex);
                    }
                }
            }
            else
            {
                ShowErrorNotificationToUser();
            }
        }

        private bool IsDropBlockAllowed(IRecipeBlock block, int recipeStepIndex)
        {
            return bool.Parse(RecipeStatus) ? GetRecipeStepEditPermission(recipeStepIndex) : true;
        }

        private void UpdateRecipeToExecuter(IRecipeBlock block, int recipeStepIndex)
        {
            recipeExecutor.UpdateBlock(recipeStepIndex, block, DeviceId);
        }

        private bool GetRecipeStepEditPermission(int newlyAddedBlockRecipeStepIndex)
        {
            int currentRunningRecipeStepIndex = 0;

            // Find the current Running step
            foreach (RecipeStepViewModel step in RecipeSteps)
            {
                if (step.RecipeStep.BlockOne.Name == "Start")
                {
                    // Skip. You don't have to check the Start Block status
                }
                else
                {
                    RecipeBlockStatus blockOneStatus = GetRecipeBlockStatus(step.RecipeStep.BlockOne);
                    RecipeBlockStatus blockTwoStatus = GetRecipeBlockStatus(step.RecipeStep.BlockTwo);
                    RecipeBlockStatus blockThreeStatus = GetRecipeBlockStatus(step.RecipeStep.BlockThree);
                    RecipeBlockStatus blockFourStatus = GetRecipeBlockStatus(step.RecipeStep.BlockFour);

                    if (blockOneStatus == RecipeBlockStatus.Started
                        || blockTwoStatus == RecipeBlockStatus.Started
                        || blockThreeStatus == RecipeBlockStatus.Started
                        || blockFourStatus == RecipeBlockStatus.Started)
                    {
                        // Check if atleast one of the Recipe Blocks in that step is running
                        currentRunningRecipeStepIndex = RecipeSteps.IndexOf(step);
                        break;
                    }
                }
            }

            return currentRunningRecipeStepIndex <= newlyAddedBlockRecipeStepIndex ? true : false;
        }

        private RecipeBlockStatus GetRecipeBlockStatus(IRecipeBlock block)
        {
            return block == null ? RecipeBlockStatus.NotConfigured
                    : bool.Parse(block.GetParameterValue("Started")) ?
                        (bool.Parse(block.GetParameterValue("Ended")) ?
                            RecipeBlockStatus.Ended : RecipeBlockStatus.Started) : RecipeBlockStatus.NotStarted;
        }

        private void HandleDropOnGrid(DataGridDropCommandParameters parameters)
        {
            if (IsRecipeEnded())
            {
                IRecipeBlock block = GetBlockInstance(parameters.DataObject);
                RecipeStep step;
                bool firstStep = false;
                if (RecipeSteps.Count == 1 && block.Name == "Start")
                {
                    step = RecipeSteps.First().RecipeStep;
                    firstStep = true;
                }
                else
                {
                    step = new RecipeStep();
                }

                bool isBlockValid = recipeBuilder.AddNewStep(step, block);
                if (isBlockValid)
                {
                    RecipeStepViewModel stepViewModel = containerProvider.Resolve<RecipeStepViewModel>();
                    stepViewModel.RecipeStep = step;
                    if (!firstStep)
                    {
                        RecipeSteps.Add(stepViewModel);
                    }

                    block.Configure(containerProvider);
                    SelectedStep = stepViewModel;
                }
            }
            else
            {
                ShowErrorNotificationToUser();
            }
        }

        public void HandleDropToAddNewStep(GridDropCommandParameters parameters)
        {
            if (IsRecipeEnded())
            {
                IRecipeBlock block = GetBlockInstance(parameters.DataObject);
                int currentRecipeStepIndex = RecipeSteps.IndexOf(parameters.DataContext as RecipeStepViewModel);
                int toBeAddedRecipeStepIndex = currentRecipeStepIndex + 1;
                RecipeStep currentRecipeStep = RecipeSteps[currentRecipeStepIndex].RecipeStep;
                RecipeStep newRecipeStep = new RecipeStep();
                bool isBlockValid = recipeBuilder.AddNewStepWhileRunningRecipe(currentRecipeStep, newRecipeStep, block, toBeAddedRecipeStepIndex);
                if (isBlockValid)
                {
                    RecipeStepViewModel stepViewModel = containerProvider.Resolve<RecipeStepViewModel>();
                    stepViewModel.RecipeStep = newRecipeStep;
                    RecipeSteps.Insert(toBeAddedRecipeStepIndex, stepViewModel);
                    block.Configure(containerProvider);
                    SelectedStep = stepViewModel;

                    if (bool.Parse(RecipeStatus))
                    {
                        // If Recipe is running start it again to update all the steps to the plc
                        StartRecipe();
                    }
                }
            }
            else
            {
                ShowErrorNotificationToUser();
            }
        }

        private void HandleMouseButton(MouseButtonCommandParameters parameters)
        {
            DataObject dropData = new DataObject();
            dropData.SetData(DataFormats.Text, parameters.Data);
            dropData.SetData(DataFormats.Serializable, parameters.Data);
            // send the selected block type to be dynamically instantiated on drop.
            DragDrop.DoDragDrop(parameters.Sender, dropData, DragDropEffects.Copy);
        }
        #endregion

        #region Commands

        private ICommand dropCellCommand;
        public ICommand DropCellCommand
        {
            get => dropCellCommand ?? (dropCellCommand = new DelegateCommand<DataGridCellDropCommandParameters>(new Action<DataGridCellDropCommandParameters>(HandleDropOnCell)));
            set => SetProperty(ref dropCellCommand, value);
        }

        private ICommand dropCommand;
        public ICommand DropCommand
        {
            get => dropCommand ?? (dropCommand = new DelegateCommand<DataGridDropCommandParameters>(new Action<DataGridDropCommandParameters>(HandleDropOnGrid)));
            set => SetProperty(ref dropCommand, value);
        }

        private ICommand _dropCommandToAddNewStep;
        public ICommand DropCommandToAddNewStep
        {
            get => _dropCommandToAddNewStep ?? (_dropCommandToAddNewStep = new DelegateCommand<GridDropCommandParameters>(new Action<GridDropCommandParameters>(HandleDropToAddNewStep)));
            set => SetProperty(ref _dropCommandToAddNewStep, value);
        }

        private ICommand mouseButtonCommand;
        public ICommand MouseButtonCommand
        {
            get => mouseButtonCommand ?? (mouseButtonCommand = new DelegateCommand<MouseButtonCommandParameters>(new Action<MouseButtonCommandParameters>(HandleMouseButton)));
            set => SetProperty(ref mouseButtonCommand, value);
        }

        private ICommand exportRecipeCommand;
        public ICommand ExportRecipeCommand
        {
            get => exportRecipeCommand ?? (exportRecipeCommand = new DelegateCommand(new Action(ExportRecipe)));
            set => SetProperty(ref exportRecipeCommand, value);
        }

        private ICommand startRecipeCommand;
        public ICommand StartRecipeCommand
        {
            get => startRecipeCommand ?? (startRecipeCommand = new DelegateCommand(new Action(StartRecipe), new Func<bool>(CanStartRecipe)));
            set => SetProperty(ref startRecipeCommand, value);
        }
        private ICommand pauseRecipeCommand;
        public ICommand PauseRecipeCommand
        {
            get => pauseRecipeCommand ?? (pauseRecipeCommand = new DelegateCommand(new Action(PauseRecipe)));
            set => SetProperty(ref pauseRecipeCommand, value);
        }
        private ICommand skipDrainCommand;
        public ICommand SkipDrainCommand
        {
            get => skipDrainCommand ?? (skipDrainCommand = new DelegateCommand(new Action(SkipDrain)));
            set => SetProperty(ref skipDrainCommand, value);
        }
        private ICommand resumeRecipeCommand;
        public ICommand ResumeRecipeCommand
        {
            get => resumeRecipeCommand ?? (resumeRecipeCommand = new DelegateCommand(new Action(ResumeRecipe)));
            set => SetProperty(ref resumeRecipeCommand, value);
        }

        private ICommand clearRecipeCommand;
        public ICommand ClearRecipeCommand
        {
            get => clearRecipeCommand ?? (clearRecipeCommand = new DelegateCommand(new Action(ClearRecipe), new Func<bool>(CanClearRecipe)));
            set => SetProperty(ref clearRecipeCommand, value);
        }

        private ICommand deleteStepCommand;
        public ICommand DeleteStepCommand
        {
            get => deleteStepCommand ?? (deleteStepCommand = new DelegateCommand<RecipeStepViewModel>(new Action<RecipeStepViewModel>(HandleDeleteStep)));
            set => SetProperty(ref deleteStepCommand, value);
        }

        private ICommand deleteBlockCommand;
        public ICommand DeleteBlockCommand
        {
            get => deleteBlockCommand ?? (deleteBlockCommand = new DelegateCommand<IRecipeBlock>(new Action<IRecipeBlock>(HandleDeleteBlock)));
            set => SetProperty(ref deleteBlockCommand, value);
        }

        private ICommand updateBlockCommand;
        public ICommand UpdateBlockCommand
        {
            get => updateBlockCommand ?? (updateBlockCommand = new DelegateCommand<IRecipeBlock>(new Action<IRecipeBlock>(UpdateBlockExecution)));
            set => SetProperty(ref updateBlockCommand, value);
        }

        private ICommand abortBlockExecutionCommand;
        public ICommand AbortBlockExecutionCommand
        {
            get => abortBlockExecutionCommand ?? (abortBlockExecutionCommand = new DelegateCommand<IRecipeBlock>(new Action<IRecipeBlock>(AbortBlockExecution)));
            set => SetProperty(ref abortBlockExecutionCommand, value);
        }

        private ICommand abortRecipeExecutionCommand;
        public ICommand AbortRecipeExecutionCommand
        {
            get => abortRecipeExecutionCommand ?? (abortRecipeExecutionCommand = new DelegateCommand(new Action(AbortRecipeExecution), new Func<bool>(CanAbortRecipeExecution)));
            set => SetProperty(ref abortRecipeExecutionCommand, value);
        }
        private ICommand importRecipeCommand;
        public ICommand ImportRecipeCommand
        {
            get => importRecipeCommand ?? (importRecipeCommand = new DelegateCommand(new Action(ImportRecipe)));
            set => SetProperty(ref importRecipeCommand, value);
        }

        private ICommand _saveRecipeExecutionCommand;
        public ICommand SaveRecipeExecutionCommand
        {
            get => _saveRecipeExecutionCommand ?? (_saveRecipeExecutionCommand = new DelegateCommand(SaveRecipeExecution));
            set => SetProperty(ref _saveRecipeExecutionCommand, value);
        }
        #endregion

        #region Properties

        public IList<IRecipeBlock> AvailableBlocks { get; private set; }

        public ObservableCollection<RecipeStepViewModel> RecipeSteps { get; }

        public RecipeStepViewModel SelectedStep { get => selectedStep; set => SetProperty(ref selectedStep, value); }

        public string DeviceId
        {
            get => recipeBuilder.DeviceId;
            private set
            {
                recipeBuilder.DeviceId = value;
                RaisePropertyChanged();
            }
        }

        private string _fieldDeviceLabel;
        public string FieldDeviceLabel
        {
            get => _fieldDeviceLabel;
            set
            {
                _fieldDeviceLabel = value;
                RaisePropertyChanged();
            }
        }

        private string _recipeStatus;
        public string RecipeStatus
        {
            get => _recipeStatus;
            set
            {
                _recipeStatus = value;
                RaisePropertyChanged();
            }
        }
        private string _drainStatus;
        public string DrainStatus
        {
            get => _drainStatus;
            set
            {
                _drainStatus = value;
                RaisePropertyChanged();
            }
        }

        private string _recipeEnded;
        public string RecipeEnded
        {
            get => _recipeEnded;
            set
            {
                _recipeEnded = value;
                RaisePropertyChanged();
            }
        }
        private string _recipePaused;
        public string RecipePaused
        {
            get => _recipePaused;
            set
            {
                _recipePaused = value;
                RaisePropertyChanged();
            }
        }

        private NavigationParameters _navigationParameters;
        public NavigationParameters NavigationParameters
        {
            get => _navigationParameters ?? (_navigationParameters = new NavigationParameters());
            set => SetProperty(ref _navigationParameters, value);
        }

        #endregion
    }

    public enum RecipeBlockStatus
    {
        NotConfigured,   // Null Recipe Block
        NotStarted,
        Started,
        Ended,
    }
}
