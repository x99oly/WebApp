//using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ReuseServer.Domain.DomainSrv
{
    public class HttpResponse
    {

        /// <summary>
        /// Escreve uma resposta HTTP para o cliente com o código de status e a mensagem especificados.
        /// </summary>
        /// <param name="response">O objeto HttpListenerResponse para escrever a resposta.</param>
        /// <param name="statusCode">O código de status HTTP a ser definido para a resposta.</param>
        /// <param name="message">A mensagem a ser incluída no corpo da resposta.</param>
        private static async Task WriteResponse(HttpListenerResponse response, HttpStatusCode statusCode, string message)
        {
            //response.StatusCode = (int)statusCode;
            //response.StatusDescription = statusCode.ToString();
            //response.ContentType = "application/json"; // Corrigido para "application/json"

            //string jsonResponse = JsonConvert.SerializeObject(new { message }); // Corrigido para serializar a mensagem

            //byte[] buffer = Encoding.UTF8.GetBytes(jsonResponse);
            //response.ContentLength64 = buffer.Length;
            //await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            //response.OutputStream.Close();
        }

        /// <summary>
        /// Escreve uma resposta de sucesso (HTTP 200 OK) para o cliente com a mensagem especificada.
        /// </summary>
        /// <param name="resp">O objeto HttpListenerResponse para escrever a resposta.</param>
        /// <param name="message">A mensagem a ser incluída no corpo da resposta.</param>
        public static async Task WriteSuccessResponse(HttpListenerResponse resp, string message)
        {
            await WriteResponse(resp, HttpStatusCode.OK, message);
        }

        /// <summary>
        /// Escreve uma resposta de criação (HTTP 201 Created) para o cliente com a mensagem especificada.
        /// </summary>
        /// <param name="resp">O objeto HttpListenerResponse para escrever a resposta.</param>
        /// <param name="message">A mensagem a ser incluída no corpo da resposta.</param>
        public static async Task WriteCreatedResponse(HttpListenerResponse resp, string message)
        {
            await WriteResponse(resp, HttpStatusCode.Created, message);
        }

        /// <summary>
        /// Escreve uma resposta de solicitação inválida (HTTP 400 Bad Request) para o cliente com a mensagem especificada.
        /// </summary>
        /// <param name="resp">O objeto HttpListenerResponse para escrever a resposta.</param>
        /// <param name="message">A mensagem a ser incluída no corpo da resposta.</param>
        public static async Task WriteBadRequestResponse(HttpListenerResponse resp, string message)
        {
            await WriteResponse(resp, HttpStatusCode.BadRequest, message);
        }

        /// <summary>
        /// Escreve uma resposta de não encontrado (HTTP 404 Not Found) para o cliente.
        /// </summary>
        /// <param name="resp">O objeto HttpListenerResponse para escrever a resposta.</param>
        public static async Task WriteNotFoundResponse(HttpListenerResponse resp)
        {
            await WriteResponse(resp, HttpStatusCode.NotFound, "404 - Arquivo Não Encontrado");
        }

        /// <summary>
        /// Escreve uma resposta de solicitação desconhecida (HTTP 400 Bad Request) para o cliente.
        /// </summary>
        /// <param name="resp">O objeto HttpListenerResponse para escrever a resposta.</param>
        public static async Task WriteUnknownRequestResponse(HttpListenerResponse resp)
        {
            await WriteResponse(resp, HttpStatusCode.BadRequest, "Solicitação desconhecida");
        }

    }
}
