using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace trpo15_voroshilov.Validators
{
    public class CorrectIntValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = value as string;
            int valueInt;

            if (!int.TryParse(text, out valueInt)) return new ValidationResult(false, "Некорректное число!");

            if (valueInt < 0) return new ValidationResult(false, "Не может быть отрицательным!");
            
            return ValidationResult.ValidResult;
        }
    }
}
