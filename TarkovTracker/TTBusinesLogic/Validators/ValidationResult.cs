using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarkovTrackerBLL.Validators
{
    public class ValidationResult
    {
        public bool IsValid => Errors.Count == 0;
        public List<string> Errors { get; }

        public ValidationResult()
        {
            Errors = new List<string>();
        }

        public ValidationResult(List<string> errors)
        {
            Errors = errors ?? new List<string>();
        }

        public void AddError(string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                Errors.Add(error);
            }
        }
    }


}