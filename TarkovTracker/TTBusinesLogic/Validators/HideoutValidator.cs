using BaseObjects.BaseObject;
using System;
using System.Collections.Generic;
using TarkovTrackerBLL.Interfaces;


namespace TarkovTrackerBLL.Validators
{
    public class HideoutStationValidator : IValidator<HideoutStation>
    {
        public ValidationResult Validate(HideoutStation station)
        {
            var result = new ValidationResult();

            if (station == null)
            {
                result.AddError("Hideout station cannot be null.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(station.Name))
            {
                result.AddError("Hideout station name is required.");
            }
            else if (station.Name.Length > 100)
            {
                result.AddError("Hideout station name must be less than 100 characters.");
            }

            if (station.Id < 0)
            {
                result.AddError("Hideout station ID cannot be negative.");
            }

            return result;
        }

        }
}
