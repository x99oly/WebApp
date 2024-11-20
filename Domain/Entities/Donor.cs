using Mysqlx.Crud;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using WebApp.Aid;
using WebApp.Domain.DTOs.Inputs;
using WebApp.Domain.DTOs.Outputs;

namespace WebApp.Domain.Entities
{
    public class Donor
    {
        public string Cod { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Donor()
        {
        }

        public Donor(UserInput user)
        {
            Cod = MyString.BuildRandomString(null);
            Name = user.Name;
            Email = user.Email;
        }
        public Donor(string name, string email)
        {
            Cod = MyString.BuildRandomString(null);
            Name = string.IsNullOrEmpty(name)? name : throw new ArgumentNullException(nameof(name));
            Email = MyString.IsValidEmail(email)? email : throw new ArgumentNullException(nameof(email));
        }

        public UserOutput ConvertTo()
        {
            return new UserOutput(this.Name, this.Email, null);
        }
        public void Update(string name, string email)
        {
            if (string.IsNullOrEmpty(Cod)) Cod = MyString.BuildRandomString(null);
            Name = string.IsNullOrEmpty(name) ? name : throw new ArgumentNullException(nameof(name));
            Email = MyString.IsValidEmail(email) ? email : throw new ArgumentNullException(nameof(email));
        }


    }

}
