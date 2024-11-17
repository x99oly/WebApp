using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Mysqlx.Prepare;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.RegularExpressions;
using WebApp.Domain.DomainSrv;
using WebApp.Domain.Entities;

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


        /// <summary>
        /// IMPLEMENTAÇÃO IMPEDE FAZER POST PARA TESTAR SE USUÁRIO EXISTE...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public async Task PostAsync<T>(T entity, string table)
        {
            ValidateParameters(entity, table);

            using (var connection = await GetConnectionAsync())
            {
                using (var command = CreateCommand(connection))
                {
                    GenerateColumnsAndParameters(entity, out var columns, out var parameters, command);

                    var updateClauses = columns
                        .Select(c => $"{c} = VALUES({c})") // 'VALUES(c)' irá pegar o novo valor para a coluna
                        .ToList();

                    command.CommandText = $@"
                        INSERT INTO {table.ToLower()} ({string.Join(",", columns)})
                        VALUES ({string.Join(",", parameters)})
                        ON DUPLICATE KEY UPDATE {string.Join(",", updateClauses)};";

                    //command.CommandText = $"INSERT INTO {table.ToLower()} ({string.Join(",", columns)}) VALUES ({string.Join(",", parameters)});";

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

        public async Task<T> GetByCodAsync<T>(string cod, string tableName) where T : class
        {
            if (!IsValidTableName(tableName)) throw new ArgumentException(nameof(tableName));

            using (var connection = await GetConnectionAsync())
            {
                var commandText = $"SELECT * FROM {tableName} WHERE COD = @cod";
                var parameters = new Dictionary<string, object>
                {
                    { "@cod", cod }
                };

                return await ExecuteQueryAsync<T>(connection, commandText, parameters);
            }
        }

        public async Task<T> GetByUserCodAsync<T>(string userCod, string tableName) where T : class
        {
            if (!IsValidTableName(tableName)) throw new ArgumentException(nameof(tableName));

            using (var connection = await GetConnectionAsync())
            {
                var commandText = $"SELECT * FROM {tableName} WHERE COD_USER = @cod_user";
                var parameters = new Dictionary<string, object>
                {
                    { "@cod_user", userCod }
                };

                return await ExecuteQueryAsync<T>(connection, commandText, parameters);
            }
        }

        public async Task UpdateAsync<T>(T entity, string table, string keyColumn, object keyValue)
        {
            ValidateParameters(entity, table);
            if (string.IsNullOrEmpty(keyColumn)) throw new ArgumentNullException(nameof(keyColumn), "O nome da coluna chave não pode ser nulo ou vazio.");

            using (var connection = await GetConnectionAsync())
            {
                using (var command = CreateCommand(connection))
                {
                    var setClauses = new List<string>();
                    var properties = entity.GetType().GetProperties();

                    foreach (var property in properties)
                    {
                        string parameterName = $"@{property.Name}";
                        if (!string.Equals(property.Name, keyColumn, StringComparison.OrdinalIgnoreCase))
                        {
                            setClauses.Add($"{property.Name.ToUpper()} = {parameterName}");
                            var value = property.GetValue(entity);
                            command.Parameters.AddWithValue(parameterName, value ?? DBNull.Value);
                        }
                    }

                    command.Parameters.AddWithValue($"@{keyColumn}", keyValue);

                    var setClause = string.Join(", ", setClauses);
                    command.CommandText = $"UPDATE {table.ToLower()} SET {setClause} WHERE {keyColumn} = @{keyColumn};";

                    await ExecuteCommandAsync(command);
                }
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
            var entity = Activator.CreateInstance<T>(); // Inicia o objeto que tem construtor padrão
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

        private MySqlCommand CreateCommand(string commandText, MySqlConnection connection)
        {
            return new MySqlCommand(commandText, connection);
        }

        private MySqlCommand CreateCommand(MySqlConnection connection, string commandText, Dictionary<string, object> parameters)
        {
            var command = CreateCommand(commandText, connection);
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

        public bool IsValidTableName(string tableName)
        {
            var validTableNames = new HashSet<string> { "users", "cersam", "donation", "donation_lot", "pcs", "schedule_Availability" };

            return validTableNames.Contains(tableName.ToLower());
        }


        private void ValidateParameters<T>(T entity, string table)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity), "A entidade deve ser inicializada.");
            if (string.IsNullOrEmpty(table)) throw new ArgumentNullException(nameof(table), "O nome da tabela não pode ser nulo ou vazio.");
        }
    }

}
