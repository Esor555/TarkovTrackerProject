using TTBusinesLogic.DTO;
using TTBusinesLogic.Interfaces;

namespace TTBusinesLogic.BusinesLogic
{
    public class UserService
    {
        private readonly IuserRepository _repository;

        public UserService(IuserRepository repository)
        {
            _repository = repository;
        }

        public void UpdateUserName(UserDTO user, string newName)
        {
            user.Username = newName;
            _repository.Update(user);
        }

        public void UpdateUserLevel(UserDTO user, int newLevel)
        {
            user.Level = newLevel;
            _repository.Update(user);
        }

        public void UpdateUserFaction(UserDTO user, Faction newFaction)
        {
            user.Faction = newFaction;
            _repository.Update(user);
        }
    }
}