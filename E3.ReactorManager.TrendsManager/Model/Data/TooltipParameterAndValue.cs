using Prism.Mvvm;

namespace E3.ReactorManager.TrendsManager.Model.Data
{
    public class TooltipParameterAndValue : BindableBase
    {
        private string _parameterName;
        public string ParameterName
        {
            get => _parameterName;
            set
            {
                _parameterName = value;
                RaisePropertyChanged();
            }
        }

        private string _parameterValue;
        public string ParameterValue
        {
            get => _parameterValue;
            set
            {
                _parameterValue = value;
                RaisePropertyChanged();
            }
        }
    }
}
