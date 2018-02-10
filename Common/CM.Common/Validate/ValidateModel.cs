using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace CM.Common.Validate
{
    public class ValidateModel<T> where T : class
    {
        public static bool Validate(T model) 
        {
            ValidationContext validationContext = new ValidationContext(model);
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(model, validationContext, results, true);
            return isValid;
        }

    }
}