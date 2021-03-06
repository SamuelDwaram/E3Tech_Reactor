using System;
using System.Globalization;
using System.Windows.Controls;

namespace E3.ReactorManager.ReportsManager.Helpers
{
    public class DoubleRangeValidationRule : ValidationRule
    {
        public double Min { get; set; }

        public double Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double parameter = 0;

            try
            {
                if (((string)value).Length > 0)
                {
                    parameter = double.Parse((string)value);
                }
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or "
                                             + e.Message);
            }

            if ((parameter <= this.Min) || (parameter >= this.Max))
            {
                return new ValidationResult(false, "Please enter value in the range: " + this.Min + " - " + this.Max + ".");
            }
            return new ValidationResult(true, null);
        }
    }
}
