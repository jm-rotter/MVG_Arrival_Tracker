using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net;

namespace TrainApp.Services
{
    public class WisdomService
    {
        private readonly HttpClient _client = new();

        public async Task<string> GetRandomWisdomAsync()
        {
            try
            {
                var url = "https://evilinsult.com/generate_insult.php?lang=en&type=json";
                var json = await _client.GetStringAsync(url);
                
                using var doc = JsonDocument.Parse(json);
                var rawInsult = doc.RootElement.GetProperty("insult").GetString() ?? "";

                return WebUtility.HtmlDecode(rawInsult);
            }
            catch { return "Even the API is tired of looking at you."; }
        }
    }
}