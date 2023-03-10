using System.Globalization;
using System.IO;
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

    public class JsonFormatValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()) ||
                !Path.IsPathFullyQualified(value.ToString()) ||
                !value.ToString().EndsWith(".json"))
            {
                return new ValidationResult(false, "Путь должен заканчиваться на .json и быть системным");
            }
           
            return ValidationResult.ValidResult;
        }
    }
}
