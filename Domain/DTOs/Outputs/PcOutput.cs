
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

        public PcOutput() { }
        public PcOutput(Pc pc)
        {
            Cep = pc.cep;
            Street = pc.street;
            Number = pc.number;
            Neighborhood = pc.neighborhood;
            Complement = pc.complement;
            City = pc.city;
            State = pc.state;
        }

        public PcOutput(string cep, string street, string number, string neighborhood, string complement, string city, string state)
        {
            Cep = cep;
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            Complement = complement;
            City = city;
            State = state;
        }
    }
}
