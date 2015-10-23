namespace ZE1Sharp.Models
{
    using Newtonsoft.Json;

    public abstract class SettingBase<TValue> : Response
    {
        public string Key { get; set; }
        public SettingType Type { get; set; }
        [JsonProperty("ro")]
        public bool ReadOnly { get; set; }
        public TValue Value { get; set; }
    }
}