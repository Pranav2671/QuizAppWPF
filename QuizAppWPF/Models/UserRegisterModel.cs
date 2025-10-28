using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAppWPF.Models
{
    internal class UserRegisterModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public UserRegisterModel()
        {
            Username = "";
            Password = "";
            Role = "User";
        }
    }
}
