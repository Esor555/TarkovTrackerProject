using BaseObjects.BaseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarkovTrackerBLL.Interfaces;

namespace TarkovTrackerBLL.Validators
{
    public class QuestValidator : IValidator<Quest>
    {
        public ValidationResult Validate(Quest quest)
        {
            var result = new ValidationResult();

            if (quest == null)
            {
                result.AddError("Quest cannot be null.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(quest.Title))
            {
                result.AddError("Quest title is required.");
            }
            else if (quest.Title.Length > 100)
            {
                result.AddError("Quest title must be less than 100 characters.");
            }

            if (quest.RequiredLevel < 0)
            {
                result.AddError("Required level cannot be negative.");
            }

            if (quest.TraderId <= 0)
            {
                result.AddError("Valid Trader ID is required.");
            }

          

            return result;
        }
    }
}
