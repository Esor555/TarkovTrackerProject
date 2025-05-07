using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TTBusinesLogic.BusinesLogic;
using TTBusinesLogic.DAL;
using TTBusinesLogic.DTO;
using TTBusinesLogic.enums;

namespace TarkovTracker.Pages
{
    public class Register : PageModel
    {
        private readonly UserService _userService;
        
        public List<User> Users { get; set; } = new();

        [BindProperty] 
        public UserDTO UserDto { get; set; }

        
        public Register(IConfiguration configuration)
        {
            
            string connectionString = configuration["ConnectionStrings:1"];
            var userRepository = new UserRepository(connectionString);
            _userService = new UserService(connectionString);
        }

       
        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid)
            {
             
                return Page();
            }
            var newUser = new User()
            {
                Name = UserDto.Username,
                Level = UserDto.Level,
                Faction = UserDto.Faction,
                PasswordHash = UserDto.password
            };

            if (_userService.AddUser(newUser))
            {

            }
      
            return Page();
        }
    }
}
