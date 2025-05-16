using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarkovTrackerDAL.test
{
    public class LoginGet
    {
        public string? UserName { get; set; }
        public string? AccesToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
