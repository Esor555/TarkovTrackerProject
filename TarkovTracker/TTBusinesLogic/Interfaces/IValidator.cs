using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarkovTrackerBLL.Validators;

namespace TarkovTrackerBLL.Interfaces
{
    public interface IValidator<T>
    {
        ValidationResult Validate(T item);
    }

}
