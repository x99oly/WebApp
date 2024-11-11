using WebApp.Aid;

namespace WebApp.Domain.DTOs.Inputs
{
    public class DonationInput
    {
        public string CodUser;
        public string Description;
        public bool Finished;

        public DonationInput()
        {
        }

        public DonationInput(string codUser, string description)
        {
            CodUser = codUser;
            Description = description;
            Finished = false;
        }

    }
}
