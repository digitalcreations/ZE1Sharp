namespace ZE1Sharp
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using ZE1Sharp.Models;

    public class StillController : ControllerBase
    {
        public StillController(IAPICaller apiCaller)
            : base(apiCaller)
        {
        }

        public async Task<FileSystemEntry> StartCaptureAsync()
        {
            var path = await this.CallAsync("cap");
            return new FileSystemEntry(path);
        }

        public async Task<FileSystemEntry> CaptureSingleAsync()
        {
            var path = await this.CallAsync("single");
            return new FileSystemEntry(path);
        }

        public async Task<int> GetRemainingCountAsync()
        {
            var remainString = await this.CallAsync("remain");
            return Convert.ToInt32(remainString);
        }

        private async Task<string> CallAsync(string action)
        {
            var response = await this.ApiCaller
                .RequestAsync<Response>(HttpMethod.Get, "ctrl/still?action=" + action)
                .ConfigureAwait(false);

            return response.Message;
        }
    }
}