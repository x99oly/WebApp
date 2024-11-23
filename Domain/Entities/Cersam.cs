using WebApp.Aid;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain.Entities
{
    public class Cersam
    {

        [Key]
        public string cod { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public int token { get; set; }

        public Cersam() { }


    }
}
