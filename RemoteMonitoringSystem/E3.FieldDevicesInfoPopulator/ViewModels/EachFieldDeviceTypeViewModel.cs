using E3.FieldDevicesInfoPopulator.Model.Data;
using E3.FieldDevicesInfoPopulator.Model.Interfaces;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace E3.FieldDevicesInfoPopulator.ViewModels
{
    public class EachFieldDeviceTypeViewModel : BindableBase
    {
        IUnityContainer unityContainer;

        public EachFieldDeviceTypeViewModel(IUnityContainer containerProvider, IFieldDevicesCommunicator fieldDevicesCommunicator)
        {
            unityContainer = containerProvider;
        }

        #region Update Field Devices Info
        public void Update(string fieldDeviceType)
        {
            Task.Factory.StartNew(new Func<object, IList<FieldDevice>>(GetFieldDevicesInfo), fieldDeviceType)
                .ContinueWith(new Action<Task<IList<FieldDevice>>>(UpdateFieldDevicesInfo));
        }

        private void UpdateFieldDevicesInfo(Task<IList<FieldDevice>> task)
        {
            if (task.IsCompleted)
            {
                //Update the FieldDevice Parameters Info Container
                foreach (FieldDevice fieldDevice in task.Result)
                {
                    if (string.IsNullOrWhiteSpace(DataContext_1.DeviceId))
                    {
                        DataContext_1 = GetFieldDeviceDataContext(fieldDevice);
                    }
                    else if (string.IsNullOrWhiteSpace(DataContext_2.DeviceId))
                    {
                        DataContext_2 = GetFieldDeviceDataContext(fieldDevice);
                    }
                    else if (string.IsNullOrWhiteSpace(DataContext_3.DeviceId))
                    {
                        DataContext_3 = GetFieldDeviceDataContext(fieldDevice);
                    }
                    else if (string.IsNullOrWhiteSpace(DataContext_4.DeviceId))
                    {
                        DataContext_4 = GetFieldDeviceDataContext(fieldDevice);
                    }
                }
            }
        }

        public FieldDeviceParameterInfoViewModel GetFieldDeviceDataContext(FieldDevice fieldDevice)
        {
            FieldDeviceParameterInfoViewModel dataContext = unityContainer.Resolve<FieldDeviceParameterInfoViewModel>();
            dataContext.DeviceId = fieldDevice.Identifier;
            dataContext.DeviceLabel = fieldDevice.Label;
            dataContext.PrepareParametersDataDictionary(GetFieldDeviceParameters(fieldDevice));
            dataContext.SubscribeToFieldDeviceCommunicator();
            return dataContext;
        }

        private IList<ParameterInfo> GetFieldDeviceParameters(FieldDevice fieldDevice)
        {
            IList<ParameterInfo> fieldDeviceParameters = new List<ParameterInfo>();
            foreach (SensorsDataSet sensorsDataSet in fieldDevice.SensorsData)
            {
                foreach (FieldPoint fieldPoint in sensorsDataSet.SensorsFieldPoints)
                {
                    if (fieldDeviceParameters.Any(parameterInfo => parameterInfo.Name == fieldPoint.Label))
                    {
                        fieldDeviceParameters.Where(parameterInfo => parameterInfo.Name == fieldPoint.Label).ToList()
                            .ForEach(parameterInfo => parameterInfo.UpdateParameterValue(fieldPoint.Value));
                    }
                    else
                    {
                        fieldDeviceParameters.Add(new ParameterInfo(fieldPoint.Label, fieldPoint.Value));
                    }
                }
            }
            return fieldDeviceParameters;
        }

        private IList<FieldDevice> GetFieldDevicesInfo(object fieldDeviceType)
        {
            IFieldDeviceTypesContainerManager fieldDeviceTypesContainerManager = unityContainer.Resolve<IFieldDeviceTypesContainerManager>();
            return fieldDeviceTypesContainerManager.GetFieldDevicesData((string)fieldDeviceType);
        }
        #endregion

        #region Properties
        private string _fieldDeviceType;
        public string FieldDeviceType
        {
            get => _fieldDeviceType;
            set
            {
                _fieldDeviceType = value;
                RaisePropertyChanged();
            }
        }

        #region Field Devices Data Context
        private FieldDeviceParameterInfoViewModel _dataContext_1;
        public FieldDeviceParameterInfoViewModel DataContext_1
        {
            get => _dataContext_1 ?? (_dataContext_1 = unityContainer.Resolve<FieldDeviceParameterInfoViewModel>());
            set => SetProperty(ref _dataContext_1, value);
        }

        private FieldDeviceParameterInfoViewModel _dataContext_2;
        public FieldDeviceParameterInfoViewModel DataContext_2
        {
            get => _dataContext_2 ?? (_dataContext_2 = unityContainer.Resolve<FieldDeviceParameterInfoViewModel>());
            set => SetProperty(ref _dataContext_2, value);
        }

        private FieldDeviceParameterInfoViewModel _dataContext_3;
        public FieldDeviceParameterInfoViewModel DataContext_3
        {
            get => _dataContext_3 ?? (_dataContext_3 = unityContainer.Resolve<FieldDeviceParameterInfoViewModel>());
            set => SetProperty(ref _dataContext_3, value);
        }

        private FieldDeviceParameterInfoViewModel _dataContext_4;
        public FieldDeviceParameterInfoViewModel DataContext_4
        {
            get => _dataContext_4 ?? (_dataContext_4 = unityContainer.Resolve<FieldDeviceParameterInfoViewModel>());
            set => SetProperty(ref _dataContext_4, value);
        }
        #endregion

        #endregion
    }
}
