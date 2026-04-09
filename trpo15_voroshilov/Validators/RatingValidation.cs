using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace trpo15_voroshilov.Validators
{
    public class RatingValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = value as string;
            text = text.Replace('.', ',');
            double rating;
            if (!double.TryParse(text, out rating)) return new ValidationResult(false, "Только число!");

            if (rating > 5) return new ValidationResult(false, "Максимум 5!");

            if (rating < 0) return new ValidationResult(false, "Строго больше нуля!");

            return ValidationResult.ValidResult;
        }
    }
}
