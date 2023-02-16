using System.Globalization;
using System.Windows.Controls;


namespace TODOGO
{
    public class EmptyStringValidationRule: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, "Значение поля не может быть пустым");
            }
            return ValidationResult.ValidResult;
        }
    }
}
