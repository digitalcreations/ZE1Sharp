namespace ZE1Sharp
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using ZE1Sharp.Models;

    internal class OptionSettingWriter : ISettingWriter<OptionSetting, string>
    {
        private readonly IAPICaller _apiCaller;
        private readonly string _name;

        public OptionSettingWriter(IAPICaller apiCaller, string name, OptionSetting value)
        {
            this.Value = value;
            this._apiCaller = apiCaller;
            this._name = name;
        }

        public OptionSetting Value { get; }

        public async Task SetAsync(string value)
        {
            if (this.Value.ReadOnly)
            {
                throw new InvalidOperationException($"Setting {this._name} is read only");
            }

            if (!this.Value.Options.Contains(value))
            {
                throw new ArgumentException($"'{value}' is not in the options list for setting {this._name}", nameof(value));
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