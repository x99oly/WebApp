﻿using ReuseServer.Domain.DomainSrv;
using WebApp.Domain.DomainSrv;
using WebApp.Domain.DTOs.Inputs;
using WebApp.Domain.Entities;
using WebApp.Persistence.MySql;

namespace WebApp.Domain.Services
{
    public class RegisterDonationSrv
    {
        private MySqlPersistence _data;
        private Donation _donation;

        private GmailSvc _email = new GmailSvc();
        private DomainEmailSvc _message = new DomainEmailSvc();

        public RegisterDonationSrv()
        {
            _data = new MySqlPersistence();
        }

        internal async Task Srv(DonationInput input)
        {
            try
            {
                _donation = new Donation();
                await _donation.NewDonation(input);

                await _data.PostAsync(_donation, "donation");

                _email.SendEmail(input.email, _message.SuccessDonationMessage);
            }
            catch (Exception ex) { throw new Exception("Não foi possível salvar doação: " + ex.Message); }
        }
    }
}
