using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json;
using WebApp.Domain.DomainSrv;

namespace WebApp.Persistence.MySql
{
    public class MySqlPersistence
    {
        private readonly string _connectionString = "Server=localhost;Database=reuse;User=root;Password=senha;";
        private GetCredentials credentials;
        public MySqlPersistence()
        {
            credentials = new GetCredentials();
            _connectionString = credentials.connectionString;
        }

        public async Task PostAsync<T>(T entity, string table)
        {
            ValidateParameters(entity, table);

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand())
                {
                    command.Connection = connection;

                    var properties = entity.GetType().GetProperties();
                    var columns = new List<string>();
                    var parameters = new List<string>();

                    foreach (var property in properties)
                    {
                        string parameterName = $"@{property.Name}";
                        columns.Add(property.Name.ToUpper());
                        parameters.Add(parameterName);
                        var value = property.GetValue(entity);
                        command.Parameters.AddWithValue(parameterName, value ?? DBNull.Value);
                    }

                    string columnString = string.Join(",", columns);
                    string parameterString = string.Join(",", parameters);
                    command.CommandText = $"INSERT INTO {table.ToLower()} ({columnString}) VALUES ({parameterString});";

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Erro ao salvar no banco de dados: {ex.Message}", ex);
                    }
                }
            }
        }

        private void ValidateParameters<T>(T entity, string table)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity), "A entidade deve ser inicializada.");
            if (string.IsNullOrEmpty(table)) throw new ArgumentNullException(nameof(table), "O nome da tabela não pode ser nulo ou vazio.");
        }
    }

}
