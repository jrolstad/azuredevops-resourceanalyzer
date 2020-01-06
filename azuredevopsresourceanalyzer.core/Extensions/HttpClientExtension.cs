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
            var result = await client.GetAsync(url);
            var data = await result.Content.ReadAsAsync<T>();

            return data;
        }

        
    }
}