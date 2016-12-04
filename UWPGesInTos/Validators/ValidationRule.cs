using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPGesInTos.Validators
{
    /// <summary>
    /// ValidationRule
    /// </summary>
    public abstract class ValidationRule
    {
        /// <summary>
        /// Validates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public abstract ValidationResult Validate(object value, CultureInfo cultureinfo);

    }
}
