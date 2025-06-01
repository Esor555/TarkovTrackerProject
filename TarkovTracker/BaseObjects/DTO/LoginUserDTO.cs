using BaseObjects.BaseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseObjects.DTO
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new();
        public User? User { get; set; }
    }

}
