using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TTBusinesLogic.BusinesLogic
{
    internal class UserValidator
    {
        internal bool ValidateUser(User user)
        {
            string ErrorString = "";
            if (ValidateId(user.Id) == false) ErrorString += $"UserID is wrong: {user.Id.ToString()}\n";
            if (ValidateName(user.Name) == false) ErrorString += $"UserName is wrong: {user.Name.ToString()} \n";
            if (ValidateLevel(user.Level) == false) ErrorString += $"UserLevel is wrong: {user.Level.ToString()}\n";
            if (ValidateFaction(user.Faction) == false) ErrorString += $"UserFaction is wrong: {user.Faction.ToString()}\n"; 
            if (ErrorString == "") return true;
            throw new Exception(ErrorString);
        }
        internal bool ValidateId(int id) 
        { 
            //add check to see if id is used already 
            return id.GetType() == typeof(int); 
        }
        internal bool ValidateName(string name) 
        {
            //check if name is used already
            return name.GetType() == typeof(string); 
        }
        internal bool ValidateLevel(int level) 
        {
            if (level.GetType() == typeof(string) && level >= 0 && level <= 99) 
            return true;
            return false;
        }
        internal bool ValidateFaction(Faction faction) 
        { 
            return faction.GetType() == typeof(Faction); 
        }
    }
}
