using Microsoft.Data.Edm.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Services {
    public class VarcharValidator : System.Windows.Controls.ValidationRule {

        public override ValidationResult Validate(object varchar, CultureInfo cultureInfo) {
            string varcharStr = (string)varchar;

            if (varcharStr.Length > 40) {
                return new ValidationResult(false,
                $"String is too long.");
            }

            if (String.IsNullOrEmpty(varcharStr)) {
                return new ValidationResult(false,
                  $"String cannot be empty.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
