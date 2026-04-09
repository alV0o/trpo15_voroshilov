using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace trpo15_voroshilov.Validators
{
    public class EmptyValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = value as string;
            if (string.IsNullOrEmpty(text)) return new ValidationResult(false, "Обязательное поле!");
            return ValidationResult.ValidResult;
        }
    }
}
