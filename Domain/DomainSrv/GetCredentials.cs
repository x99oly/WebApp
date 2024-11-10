using System.Text.Json;

namespace WebApp.Domain.DomainSrv
{
    public class GetCredentials
    {
        private string _jsonFilePath = Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\Credentials.json");
        private string _jsonString;

        public string domainEmail { get; }
        public string domainPassword { get; }
        public string connectionString { get; }

        public GetCredentials()
        {
            _jsonString = File.ReadAllText(_jsonFilePath);
            using (var doc = JsonDocument.Parse(_jsonString))
            {
                domainEmail = doc.RootElement.GetProperty("EmailSettings").GetProperty("DomainEmail").GetString();
                domainPassword = doc.RootElement.GetProperty("EmailSettings").GetProperty("DomainPassword").GetString();
                connectionString = doc.RootElement.GetProperty("MySql").GetProperty("ConnectionString").GetString();
            }
        }
    }
}