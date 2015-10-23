namespace ZE1Sharp.Models
{
    using Newtonsoft.Json;

    public class VideoInfo : Response
    {
        [JsonProperty("w")]
        public int Width { get; set; }

        [JsonProperty("h")]
        public int Height { get; set; }
    }
}