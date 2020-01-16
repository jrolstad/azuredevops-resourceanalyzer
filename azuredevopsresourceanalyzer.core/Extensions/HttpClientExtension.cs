using System;
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

            try
            {
                result.EnsureSuccessStatusCode();
                var data = await result.Content.ReadAsAsync<T>();

                return data;
            }
            catch (Exception e)
            {
                var requestAuthorization = result.RequestMessage?.Headers?.Authorization?.Parameter;
                
                var message = $"Unable to parse result for {url}\r\nStatus is {result.StatusCode}\r\nAccess Token is{requestAuthorization}";
                
                throw new ApplicationException(message,e);
            }
           
        }

        
    }
}