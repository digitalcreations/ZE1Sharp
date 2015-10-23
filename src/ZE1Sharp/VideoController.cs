namespace ZE1Sharp
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using ZE1Sharp.Models;

    public class VideoController : ControllerBase
    {
        public VideoController(IAPICaller apiCaller)
            : base(apiCaller)
        {
        }

        public async Task StartAsync()
        {
            await this.ApiCaller
                .RequestAsync<Response>(HttpMethod.Get, "ctrl/rec?action=start")
                .ConfigureAwait(false);
        }

        public async Task StopAsync()
        {
            await this.ApiCaller
                .RequestAsync<Response>(HttpMethod.Get, "ctrl/rec?action=stop")
                .ConfigureAwait(false);
        }

        public async Task<TimeSpan> GetRemainingTimeAsync()
        {
            var response = await this.ApiCaller
                                     .RequestAsync<Response>(HttpMethod.Get, "ctrl/rec?action=remain")
                                     .ConfigureAwait(false);
            return TimeSpan.FromMinutes(Convert.ToInt32(response.Message));
        }
    }
}