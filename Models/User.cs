using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiagnoseMe.Helpers;

namespace DiagnoseMe.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public EGender Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public User(string name, string surname, EGender gender, string email, string password)
        {
            Name = name;
            Surname = surname;
            Gender = gender;
            Email = email;
            Password = password;
        }
    }
}
