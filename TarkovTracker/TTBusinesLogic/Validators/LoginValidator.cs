using BaseObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarkovTrackerBLL.Interfaces;

namespace TarkovTrackerBLL.Validators
{
    public class LoginValidator : IValidator<UserDTO>
    {
        public ValidationResult Validate(UserDTO dto)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(dto.Username))
                result.AddError("Username is required.");

            if (string.IsNullOrWhiteSpace(dto.passwordhash))
                result.AddError("Password is required.");

            return result;
        }
    }

}
