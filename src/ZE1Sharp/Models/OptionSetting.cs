namespace ZE1Sharp.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class OptionSetting : SettingBase<string>
    {
        [JsonProperty("opts")]
        public IEnumerable<string> Options { get; set; }
    }
}