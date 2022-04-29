using PlottnerBowlingChallenge.Business.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlottnerBowlingChallenge.Business.Validators
{
    internal class BaseValidator<T> : IValidator<T>
    {
        public new List<ValidationResult> Validate(T value)
        {
            if(value == null)
            {

            }

            ValidationContext vc = new ValidationContext(value);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(value, vc, results, true);
            return results
        }
    }
}
