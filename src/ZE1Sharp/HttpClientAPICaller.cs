namespace ZE1Sharp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using ZE1Sharp.Models;

    public class HttpClientAPICaller : IAPICaller
    {
        public async Task<T> RequestAsync<T>(HttpMethod method, string path, IDictionary<string, string> parameters = null) where T : class, new()
        {
            var url = new Uri(new Uri("http://10.98.32.1/"), path);
            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(new HttpRequestMessage(method, url.AttachParameters(parameters)));
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException($"Response code {response.StatusCode} when calling {url}");
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public async Task<DownloadedFile> DownloadAsync(string path)
        {
            var url = new Uri(new Uri("http://10.98.32.1/"), path);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                return new DownloadedFile(
                    await response.Content.ReadAsStreamAsync(),
                    response.Content.Headers.ContentType.MediaType);
            }
        }
    }
}