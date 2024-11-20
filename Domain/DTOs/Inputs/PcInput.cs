
namespace WebApp.Domain.DTOs.Inputs
{
    public class PcInput
    {
        public string CEP { get; set; }
        public string STREET { get; set; }
        public string? NUMBER { get; set; }
        public string NEIGHBORHOOD { get; set; }
        public string? COMPLEMENT { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }



        public PcInput(string cep, string street, string neighborhood, string city, string state, string? number = null, string? complement = null)
        {
            // Tenta validar os dados e criar o objeto
            ValidateFormat(cep, street, neighborhood, city, state);

            CEP = cep;
            STREET = street;
            NUMBER = number;
            NEIGHBORHOOD = neighborhood;
            COMPLEMENT = complement;
            CITY = city;
            STATE = state;
        }

        public PcInput() { }
        private void ValidateFormat(string cep, string street, string neighborhood, string city, string state)
        {
            if (!IsAValidCep(cep)) throw new ArgumentException("CEP inválido.");
            if (!IsAValidStreet(street)) throw new ArgumentException("Rua inválida.");
            if (!IsAValidNeighborhood(neighborhood)) throw new ArgumentException("Bairro inválido.");
            if (!IsAValidCity(city)) throw new ArgumentException("Cidade inválida.");
            if (!IsAValidState(state)) throw new ArgumentException("Estado inválido.");
        }

        private bool IsAValidCep(string cep)
        {
            return !string.IsNullOrWhiteSpace(cep) && System.Text.RegularExpressions.Regex.IsMatch(cep, @"^\d{5}-\d{3}$") || cep.Length == 8;
        }

        private bool IsAValidStreet(string street)
        {
            return !string.IsNullOrWhiteSpace(street) && street.Any(char.IsLetter);
        }

        private bool IsAValidNeighborhood(string neighborhood)
        {
            return !string.IsNullOrWhiteSpace(neighborhood) && neighborhood.Any(char.IsLetter);
        }

        private bool IsAValidCity(string city)
        {
            return !string.IsNullOrWhiteSpace(city) && !city.Any(char.IsDigit);
        }

        private bool IsAValidState(string state)
        {
            return !string.IsNullOrWhiteSpace(state) && state.Length == 2;
        }


        public override string ToString()
        {
            return $"{STREET}, {NUMBER}, {NEIGHBORHOOD}, {CITY}, {STATE}, {CEP}";
        }
    }
}
