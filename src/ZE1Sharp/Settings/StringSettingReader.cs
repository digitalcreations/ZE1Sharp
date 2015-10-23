namespace ZE1Sharp
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using ZE1Sharp.Models;

    internal class StringSettingReader : ISettingReader<StringSetting, string>
    {
        private readonly IAPICaller _apiCaller;

        private readonly string _name;

        public StringSettingReader(IAPICaller apiCaller, string name)
        {
            this._apiCaller = apiCaller;
            this._name = name;
        }

        public async Task<ISettingWriter<StringSetting, string>> ReadAsync()
        {
            var setting = await this._apiCaller
                                    .RequestAsync<StringSetting>(
                                        HttpMethod.Get,
                                        "/ctrl/get".AttachParameter("k", this._name))
                                    .ConfigureAwait(false);
            
            return new StringSettingWriter(this._apiCaller, this._name, setting);
        }
    }
}