using MicroservicesLearning.PlatformService.Dtos;
using System.Text;
using System.Text.Json;

namespace MicroservicesLearning.PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;

        public HttpCommandDataClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendPlatformToCommand(PlatformReadDto platformReadDto)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(platformReadDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("", httpContent);
        }
    }
}
