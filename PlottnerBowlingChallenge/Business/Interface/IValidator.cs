using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlottnerBowlingChallenge.Business.Interface
{
    public interface IValidator<T>
    {
        public List<ValidationResult> Validate(T value);
    }
}