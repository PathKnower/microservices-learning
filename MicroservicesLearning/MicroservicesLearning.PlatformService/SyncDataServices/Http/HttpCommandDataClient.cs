using MicroservicesLearning.PlatformService.Dtos;
using System.Text;
using System.Text.Json;

namespace MicroservicesLearning.PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendPlatformToCommand(PlatformReadDto platformReadDto)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(platformReadDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["CommandServiceUri"]}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to the Commands Service was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync post wasn't ok!");
            }
        }
    }
}
