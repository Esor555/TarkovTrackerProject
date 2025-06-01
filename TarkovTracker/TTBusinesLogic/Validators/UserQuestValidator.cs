using BaseObjects.BaseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarkovTrackerBLL.Interfaces;

namespace TarkovTrackerBLL.Validators
{
    public class UserQuestValidator : IValidator<UserQuest>
    {
        public ValidationResult Validate(UserQuest userQuest)
        {
            var result = new ValidationResult();

            if (userQuest.UserId <= 0)
                result.AddError("UserId must be a positive integer.");

            if (userQuest.QuestId <= 0)
                result.AddError("QuestId must be a positive integer.");

            if (string.IsNullOrWhiteSpace(userQuest.Status))
                result.AddError("Quest status is required.");

            return result;
        }
    }
}
