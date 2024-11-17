using WebApp.Aid;

namespace WebApp.Domain.DTOs.Inputs
{
    public class DonationInput
    {
        public string email;
        public string Description;
        public bool Finished;

        public DonationInput()
        {
        }

        public DonationInput(string email, string description)
        {
            this.email = email;
            Description = description;
            Finished = false;
        }

    }
}
