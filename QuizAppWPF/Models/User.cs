using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAppWPF.Models
{
    public class User
    {
        public int Id { get; set; }            // Unique ID
        public string Username { get; set; }   // Username
        public string Password { get; set; }   // Password
        public string Role { get; set; }       // "User" or "Admin"

        public User()
        {
            Username = "";
            Password = "";
            Role = "User";
        }
    }
}
