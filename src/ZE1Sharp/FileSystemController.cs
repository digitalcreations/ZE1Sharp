namespace ZE1Sharp
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using ZE1Sharp.Models;

    public class FileSystemController : ControllerBase
    {
        public const string RootFolder = "/DCIM";

        internal FileSystemController(IAPICaller apiCaller) : base(apiCaller)
        {
        }

        public async Task<ICollection<FileSystemEntry>> GetFolderAsync(FileFilter filter = FileFilter.None)
        {
            return await this.GetFolderAsync(RootFolder + "/", filter).ConfigureAwait(false);
        }

        public async Task<ICollection<FileSystemEntry>> GetFolderAsync(FileSystemEntry entry, FileFilter filter = FileFilter.None)
        {
            return await this.GetFolderAsync(entry.Path, filter).ConfigureAwait(false);
        }

        public async Task<DownloadedFile> DownloadFileAsync(FileSystemEntry file)
        {
            return await this.ApiCaller.DownloadAsync(file.Path).ConfigureAwait(false);
        }

        public async Task<DownloadedFile> DownloadThumbnailAsync(FileSystemEntry file)
        {
            return await this.ApiCaller
                             .DownloadAsync(file.Path.AttachParameters(new Dictionary<string, string> { { "act", "thm" } }))
                             .ConfigureAwait(false);
        }

        public async Task<VideoInfo> GetVideoInfoAsync(FileSystemEntry file)
        {
            return await this.ApiCaller
                .RequestAsync<VideoInfo>(HttpMethod.Get, file.Path.AttachParameter("act", "info"))
                .ConfigureAwait(false);
        }

        public async Task DeleteFileAsync(FileSystemEntry file)
        {
            await this.ApiCaller
                .RequestAsync<Response>(HttpMethod.Get, file.Path, new Dictionary<string, string> { { "act", "rm" } })
                .ConfigureAwait(false);
        }

        private async Task<ICollection<FileSystemEntry>> GetFolderAsync(string path, FileFilter filter = FileFilter.None)
        {
            var parameters = new Dictionary<string, string>();
            if (filter.HasFlag(FileFilter.Image))
            {
                parameters.Add("p", "1");
            }

            if (filter.HasFlag(FileFilter.Video))
            {
                parameters.Add("v", "1");
            }

            var response = await this.ApiCaller
                                     .RequestAsync<FolderInformation>(HttpMethod.Get, path, parameters)
                                     .ConfigureAwait(false);
            return response.Files.Select(r => new FileSystemEntry(path, r)).ToList();
        }
    }
}