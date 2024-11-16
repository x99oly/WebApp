using WebApp.Aid;
using WebApp.Domain.DTOs.Inputs;

namespace WebApp.Domain.Entities
{
    internal class Donation
    {
        public string Cod { get; private set; }
        public string Cod_User { get; private set; }
        public string Cod_Pc { get; private set; }
        public string? Cod_Cersam { get; private set; }
        public DateTime Date { get; private set; }
        public string Description { get; private set; }
        public bool Finished { get; private set; }

        public Donation()
        {
            Cod = MyString.BuildRandomString(null);
        }

        public Donation(DonationInput input)
        {
            if(string.IsNullOrEmpty(Cod)) Cod = MyString.BuildRandomString(null);

            Cod_User = input.CodUser;
            Description = input.Description;
            Finished = input.Finished;
            Date = DateTime.Now;
        }
        public void UpdateEntity(string? codUser, string? codPc, string? codCersam, string? description)
        {
            if (!string.IsNullOrEmpty(codUser)) Cod_User = codUser;
            if (!string.IsNullOrEmpty(codPc)) Cod_Pc = codPc;
            if (!string.IsNullOrEmpty(codCersam)) Cod_Cersam = codCersam;
            if (!string.IsNullOrEmpty(description)) Description = description;
        }

        public void FinishDonation()
        {
            if (string.IsNullOrEmpty(Cod_User)) throw new InvalidOperationException("Usuário não pode ser nulo.");
            if (string.IsNullOrEmpty(Cod_Pc)) throw new InvalidOperationException("Código do Ponto de Coleta não pode ser nulo ou vazio.");
            if (string.IsNullOrEmpty(Cod_Cersam)) throw new InvalidOperationException("Código do Cersam não pode ser nulo ou vazio.");

            Finished = true;
        }
    }
}
