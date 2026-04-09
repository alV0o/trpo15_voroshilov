using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace trpo15_voroshilov.Validators
{
    public class PriceValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = value as string;

            text = text.Replace('.', ',');
            decimal result;
            if (!decimal.TryParse(text, out result)) return new ValidationResult(false, "Только число!");

            if (result <= 0) return new ValidationResult(false, "Цена должна быть больше нуля!");

            return ValidationResult.ValidResult;

        }
    }
}
