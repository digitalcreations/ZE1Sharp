namespace ZE1Sharp
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using ZE1Sharp.Models;

    internal class RangeSettingReader : ISettingReader<RangeSetting, int>
    {
        private readonly IAPICaller _apiCaller;

        private readonly string _name;

        public RangeSettingReader(IAPICaller apiCaller, string name)
        {
            this._apiCaller = apiCaller;
            this._name = name;
        }

        public async Task<ISettingWriter<RangeSetting, int>> ReadAsync()
        {
            var setting = await this._apiCaller
                                    .RequestAsync<RangeSetting>(
                                        HttpMethod.Get,
                                        "/ctrl/get".AttachParameter("k", this._name))
                                    .ConfigureAwait(false);

            return new RangeSettingWriter(this._apiCaller, this._name, setting);
        }
    }
}