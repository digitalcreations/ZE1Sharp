namespace ZE1Sharp
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using ZE1Sharp.Models;

    internal class OptionSettingReader : ISettingReader<OptionSetting, string>
    {
        private readonly IAPICaller _apiCaller;

        private readonly string _name;

        public OptionSettingReader(IAPICaller apiCaller, string name)
        {
            this._apiCaller = apiCaller;
            this._name = name;
        }

        public async Task<ISettingWriter<OptionSetting, string>> ReadAsync()
        {
            var setting = await this._apiCaller
                                    .RequestAsync<OptionSetting>(
                                        HttpMethod.Get,
                                        "/ctrl/get".AttachParameter("k", this._name))
                                    .ConfigureAwait(false);

            return new OptionSettingWriter(this._apiCaller, this._name, setting);
        }
    }
}