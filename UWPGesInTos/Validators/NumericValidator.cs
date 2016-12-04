using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPGesInTos.Validators
{
    public class NumericValidator : ValidationRule
    {

        private int _minimumLength = -1;
        private int _maximumLength = -1;
        private string _errorMessage;

        /// <summary>
        /// Gets or sets the minimum length.
        /// </summary>
        /// <value>
        /// The minimum length.
        /// </value>
        public int MinimumLength
        {
            get { return _minimumLength; }
            set { _minimumLength = value; }
        }

        /// <summary>
        /// Gets or sets the maximum length.
        /// </summary>
        /// <value>
        /// The maximum length.
        /// </value>
        public int MaximumLength
        {
            get { return _maximumLength; }
            set { _maximumLength = value; }
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }
        

        /// <summary>
        /// Validates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cultureinfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureinfo)
        {
            ValidationResult result = new ValidationResult(null);

            string inputString = (value ?? string.Empty).ToString();

            if (inputString.Length < this.MinimumLength || (this.MaximumLength > 0 && inputString.Length > this.MaximumLength))
            {
                result = new ValidationResult(this.ErrorMessage);
            }

            return result;
        }

    }
}
