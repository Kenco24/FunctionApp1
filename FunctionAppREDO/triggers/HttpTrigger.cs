using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace FunctionApp1
{
    public class GetUsersFunction
    {
        private readonly ILogger<GetUsersFunction> _logger;

        public GetUsersFunction(ILogger<GetUsersFunction> logger)
        {
            _logger = logger;
        }

        [Function("GetUsers")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "users")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP GET trigger function processed a request.");

            // Simulate fetching user data from a database
            // await _userRepo.GetUsersAsync();
            // error handling if users == null etc.. if everything is good return users
            var users = new List<object>
            {
                new { UserId = 1, FullName = "Alice", Email = "alice@example.com" },
                new { UserId = 2, FullName = "Bob", Email = "bob@example.com" },
                new { UserId = 3, FullName = "Charlie", Email = "charlie@example.com" }
            };

            //returning users as json
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");

            // Serialize the list of users to JSON and write asynchronously
            await response.WriteStringAsync(JsonSerializer.Serialize(users));


            return response;


            // catch (exepction ex) { return error}
        }
    }
}
