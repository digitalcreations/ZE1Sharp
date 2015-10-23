namespace ZE1Sharp
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using ZE1Sharp.Models;

    internal class RangeSettingWriter : ISettingWriter<RangeSetting, int>
    {
        private readonly IAPICaller _apiCaller;
        private readonly string _name;

        public RangeSettingWriter(IAPICaller apiCaller, string name, RangeSetting value)
        {
            this.Value = value;
            this._apiCaller = apiCaller;
            this._name = name;
        }

        public RangeSetting Value { get; }

        public async Task SetAsync(int value)
        {
            if (this.Value.ReadOnly)
            {
                throw new InvalidOperationException($"Setting {this._name} is read only");
            }

            if (this.Value.Min > value || this.Value.Max < value)
            {
                throw new ArgumentOutOfRangeException($"'{value}' is not in the allowed range [{this.Value.Min}, {this.Value.Max}] for property {this._name}", nameof(value));
            }

            var response = await this._apiCaller
                                     .RequestAsync<StringSetting>(
                                         HttpMethod.Get,
                                         "/ctrl/set".AttachParameter(this._name, value.ToString()))
                                     .ConfigureAwait(false);
            if (response.Code != StatusCode.OK)
            {
                throw new InvalidOperationException($"Could not set setting {this._name}");
            }
            this.Value.Value = value;
        }
    }
}