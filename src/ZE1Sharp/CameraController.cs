using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ZE1Sharp.IntegrationTests")]
[assembly: InternalsVisibleTo("ZE1Sharp.UnitTests")]
namespace ZE1Sharp
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using ZE1Sharp.Models;

    public class CameraController : ControllerBase
    {
        public CameraController(IAPICaller apiCaller = null)
            : base(apiCaller)
        {
        }

        public FileSystemController FileSystem => new FileSystemController(this.ApiCaller);
        public StillController Still => new StillController(this.ApiCaller);
        public VideoController Video => new VideoController(this.ApiCaller);
        public SettingsController Settings => new SettingsController(this.ApiCaller);

        public async Task<CameraInformation> GetInfoAsync()
        {
            return await this.ApiCaller
                             .RequestAsync<CameraInformation>(HttpMethod.Get, "info")
                             .ConfigureAwait(false);
        }

        public async Task ShutdownAsync()
        {
            await this.ApiCaller
                .RequestAsync<object>(HttpMethod.Get, "ctrl/shutdown")
                .ConfigureAwait(false);
        }

        public async Task RebootAsync()
        {
            await this.ApiCaller
                .RequestAsync<object>(HttpMethod.Get, "ctrl/reboot")
                .ConfigureAwait(false);
        }

        public async Task SetDateTimeAsync(DateTime date)
        {
            await this.ApiCaller
                .RequestAsync<object>(HttpMethod.Get, "datetime",
                    new Dictionary<string, string>()
                        {
                            { "date", date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) },
                            { "time", date.ToString("HH:mm:ss", CultureInfo.InvariantCulture) }
                        })
            .ConfigureAwait(false);
        }

        public async Task<CameraSpecificMode> GetModeAsync()
        {
            var str = await this.ApiCaller
                .RequestAsync<Response>(HttpMethod.Get, "ctrl/mode?action=query")
                .ConfigureAwait(false);
            var table = new Dictionary<string, CameraSpecificMode>
                            {
                                { "pb", CameraSpecificMode.Playback },
                                { "pb_ing", CameraSpecificMode.PlaybackPlaying },
                                { "pb_paused", CameraSpecificMode.PlaybackPaused },
                                { "cap", CameraSpecificMode.Still },
                                { "cap_tl_ing", CameraSpecificMode.StillTimelapseCapturing },
                                { "cap_tl_idle", CameraSpecificMode.StillTimelapseIdle },
                                { "cap_idle", CameraSpecificMode.StillIdle },
                                { "cap_burst", CameraSpecificMode.StillBurst },
                                { "rec", CameraSpecificMode.Movie },
                                { "rec_ing", CameraSpecificMode.MovieRecording },
                                { "unknown", CameraSpecificMode.Unknown }
                            };
            return table[str.Message];
        }

        public async Task SetModeAsync(CameraGenericMode mode)
        {
            await this.ApiCaller.RequestAsync<object>(
                    HttpMethod.Get,
                    "ctrl/mode",
                    new Dictionary<string, string>()
                        {
                            {
                                "action",
                                mode == CameraGenericMode.Playback
                                    ? "to_pb"
                                    : mode == CameraGenericMode.Still ? "to_cap" : "to_rec"
                            }
                        });
        }
    }
}