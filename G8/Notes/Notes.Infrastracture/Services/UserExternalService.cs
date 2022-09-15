using Microsoft.Extensions.Logging;
using Notes.Application.Models.External;
using Notes.Application.Services;
using System.Text.Json;

namespace Notes.Infrastracture.Services
{
    public class UserExternalService
        : IUserExternalService
    {
        private readonly HttpClient client;
        private readonly ILogger<UserExternalService> logger;

        public UserExternalService(HttpClient client, ILogger<UserExternalService> logger)
        {
            this.client = client;
            this.logger = logger;
        }
        public async Task<IEnumerable<ExternalUser>> GetExternalUsers(CancellationToken token = default)
        {
            try
            {
                logger.LogInformation("Reading data from external API");
                HttpResponseMessage? response = await client.GetAsync("api/user/random", token);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync(token);
                return Deserilize<ExternalUser>(json);
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to get user data", ex);
                throw;
            }
        }

        private static T? DeserilizeSingle<T>(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            return JsonSerializer.Deserialize<T>(json, options) ?? default;
        }
        private static IEnumerable<T> Deserilize<T>(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            return JsonSerializer.Deserialize<IEnumerable<T>>(json, options) ?? Array.Empty<T>();
        }
    }
}
