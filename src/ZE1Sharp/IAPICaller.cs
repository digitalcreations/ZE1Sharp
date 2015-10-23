namespace ZE1Sharp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    using ZE1Sharp.Models;

    public interface IAPICaller
    {
        Task<T> RequestAsync<T>(HttpMethod method, string path, IDictionary<string, string> parameters = null)
            where T : class, new();

        Task<DownloadedFile> DownloadAsync(string path);
    }
}