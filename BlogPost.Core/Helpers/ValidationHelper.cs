using System;
using System.ComponentModel.DataAnnotations;
using BlogPost.Core.Exceptions;

namespace Services.Helper
{
    public class ValidationHelper
    {
        internal static void ModelValidation(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject
                (obj, validationContext, validationResults, true);
            if (!isValid)
            {
                string errorMessages = string.Join(Environment.NewLine, validationResults.Select(result => result.ErrorMessage));

                throw new ArgumentValidationException($"Validation failed{errorMessages}" , obj);
            }

        }
    }
}
