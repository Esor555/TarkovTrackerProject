using BaseObjects.BaseObject;
using BaseObjects.ennums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarkovTrackerBLL.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TarkovTrackerBLL.Validators
{
    public class UserValidator : IValidator<User>
    {
        public ValidationResult Validate(User user)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(user.Name))
                result.AddError("Username is required.");

            if (user.Level < 0)
                result.AddError("User level cannot be negative.");

            if (!Enum.IsDefined(typeof(Faction), user.Faction))
                result.AddError("Invalid faction selected.");

            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                result.AddError("Password cannot be empty.");

            if (user.PasswordHash?.Length < 6)
                result.AddError("Password must be at least 6 characters.");

            if (string.IsNullOrWhiteSpace(user.Role))
                result.AddError("User role is required.");

            return result;
        }
    }
}
