namespace ZE1Sharp
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using ZE1Sharp.Models;

    internal class StringSettingWriter : ISettingWriter<StringSetting, string>
    {
        private readonly IAPICaller _apiCaller;
        private readonly string _name;

        public StringSettingWriter(IAPICaller apiCaller, string name, StringSetting value)
        {
            this.Value = value;
            this._apiCaller = apiCaller;
            this._name = name;
        }

        public StringSetting Value { get; }

        public async Task SetAsync(string value)
        {
            if (this.Value.ReadOnly)
            {
                throw new InvalidOperationException($"Setting {this._name} is read only");
            }

            var response = await this._apiCaller
                .RequestAsync<StringSetting>(
                    HttpMethod.Get,
                    "/ctrl/set".AttachParameter(this._name, value))
                .ConfigureAwait(false);
            if (response.Code != StatusCode.OK)
            {
                throw new InvalidOperationException($"Could not set setting {this._name}");
            }
            this.Value.Value = value;
        }
    }
}