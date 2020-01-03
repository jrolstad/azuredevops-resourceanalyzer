using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace azuredevopsresourceanalyzer.core.Extensions
{
    public static class HttpClientExtension
    {
        public static async Task<T> GetAsJson<T>(this HttpClient client, string url)
        {
            var result = await client.GetStringAsync(url);
            
            var data = JsonConvert.DeserializeObject<T>(result);

            return data;
        }
    }
}