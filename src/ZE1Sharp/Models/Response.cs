namespace ZE1Sharp.Models
{
    using Newtonsoft.Json;

    public class Response
    {
        public StatusCode Code { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}
