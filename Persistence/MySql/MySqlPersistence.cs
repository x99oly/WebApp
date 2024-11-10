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

            using (var connection = await GetConnectionAsync())
            {
                using (var command = CreateCommand(connection))
                {
                    GenerateColumnsAndParameters(entity, out var columns, out var parameters, command);

                    command.CommandText = $"INSERT INTO {table.ToLower()} ({string.Join(",", columns)}) VALUES ({string.Join(",", parameters)});";

                    await ExecuteCommandAsync(command);
                }
            }
        }

        public async Task<T> GetByEmailAsync<T>(string email) where T : class
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));

            using (var connection = await GetConnectionAsync())
            {
                var commandText = $"SELECT * FROM users WHERE EMAIL = @email";
                var parameters = new Dictionary<string, object>
                {
                    { "@email", email }
                };

                return await ExecuteQueryAsync<T>(connection, commandText, parameters);
            }
        }



        // *********************************************************************************************** //
        // *****************************  MÉTODOS AUXILIÁRES E PRIVADOS ********************************** //
        // *********************************************************************************************** //

        private async Task<T> ExecuteQueryAsync<T>(MySqlConnection connection, string commandText, Dictionary<string, object> parameters) where T : class
        {
            using (var command = CreateCommand(connection, commandText, parameters))
            {
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return MapToEntity<T>(reader);
                }
                return default;
            }
        }

        private T MapToEntity<T>(DbDataReader reader) where T : class
        {
            var entity = Activator.CreateInstance<T>();
            foreach (var property in typeof(T).GetProperties())
            {
                if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                {
                    var value = reader[property.Name];
                    property.SetValue(entity, value);
                }
            }
            return entity;
        }

        private async Task<MySqlConnection> GetConnectionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        private MySqlCommand CreateCommand(MySqlConnection connection)
        {
            return new MySqlCommand
            {
                Connection = connection
            };
        }

        private MySqlCommand CreateCommand(MySqlConnection connection, string commandText, Dictionary<string, object> parameters)
        {
            var command = new MySqlCommand(commandText, connection);
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue(param.Key, param.Value);
            }
            return command;
        }

        private void GenerateColumnsAndParameters<T>(T entity, out List<string> columns, out List<string> parameters, MySqlCommand command)
        {
            var properties = entity.GetType().GetProperties();
            columns = new List<string>();
            parameters = new List<string>();

            foreach (var property in properties)
            {
                string parameterName = $"@{property.Name}";
                columns.Add(property.Name.ToUpper());
                parameters.Add(parameterName);
                var value = property.GetValue(entity);
                command.Parameters.AddWithValue(parameterName, value ?? DBNull.Value);
            }
        }

        private async Task ExecuteCommandAsync(MySqlCommand command)
        {
            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao executar comando no banco de dados: {ex.Message}", ex);
            }
        }


        private void ValidateParameters<T>(T entity, string table)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity), "A entidade deve ser inicializada.");
            if (string.IsNullOrEmpty(table)) throw new ArgumentNullException(nameof(table), "O nome da tabela não pode ser nulo ou vazio.");
        }
    }

}
