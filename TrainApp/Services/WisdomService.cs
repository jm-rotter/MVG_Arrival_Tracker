using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net;

namespace TrainApp.Services
{
    public class WisdomService
    {
        private readonly HttpClient _client = new();

        public async Task<(string quote,string author)> GetRandomWisdomAsync()
        {
            try
            {
                var url = "https://zenquotes.io/api/random?";
                var json = await _client.GetStringAsync(url);
                
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement[0];

                var quote = root.GetProperty("q").GetString() ?? "No wisdom found.";
                var author = root.GetProperty("a").GetString() ?? "Unknown";

                return (WebUtility.HtmlDecode(quote), author);
            }
            catch 
            { 
                return ("The only true wisdom is in knowing you know nothing.", "Socrates"); 
            }
        }
    }
}