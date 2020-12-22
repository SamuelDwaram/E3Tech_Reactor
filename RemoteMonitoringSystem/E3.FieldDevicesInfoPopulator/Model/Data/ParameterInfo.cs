using Prism.Mvvm;

namespace E3.FieldDevicesInfoPopulator.Model.Data
{
    public class ParameterInfo : BindableBase
    {
        public ParameterInfo()
        {

        }

        public ParameterInfo(string paramName, string paramValue)
        {
            Name = paramName;
            Value = paramValue;
        }

        public void UpdateParameterValue(string paramValue)
        {
            Value = paramValue;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _value;
        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}
