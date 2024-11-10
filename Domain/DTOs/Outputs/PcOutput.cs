
using WebApp.Domain.Entities;

namespace WebApp.Domain.DTOs.Outputs
{
    public class PcOutput
    {
        public string UserName { get; set; }
        public string Cep { get; set; }
        public string Street { get; set; }
        public string? Number { get; set; }
        public string Neighborhood { get; set; }
        public string? Complement { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        internal PcOutput(Pc pc)
        {
            Cep = pc.cep;
            Street = pc.street;
            Number = pc.number;
            Neighborhood = pc.neighborhood;
            Complement = pc.complement;
            City = pc.city;
            State = pc.state;
        }
    }
}
