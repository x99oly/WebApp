using Mysqlx.Crud;
using System.Text.RegularExpressions;
using WebApp.Aid;

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

        public Donor(string name, string email)
        {
            Cod = MyString.BuildRandomString(null);
            Name = string.IsNullOrEmpty(name)? name : throw new ArgumentNullException(nameof(name));
            Email = MyString.IsValidEmail(email)? email : throw new ArgumentNullException(nameof(email));
        }

        public void Update(string name, string email)
        {
            if (string.IsNullOrEmpty(Cod)) Cod = MyString.BuildRandomString(null);
            Name = string.IsNullOrEmpty(name) ? name : throw new ArgumentNullException(nameof(name));
            Email = MyString.IsValidEmail(email) ? email : throw new ArgumentNullException(nameof(email));
        }
    }

}
