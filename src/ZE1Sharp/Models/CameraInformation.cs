namespace ZE1Sharp.Models
{
    using System;
    using System.IO;

    using Newtonsoft.Json;

    public class DownloadedFile
    {
        public DownloadedFile(Stream stream, string mimeType)
        {
            this.Stream = stream;
            this.MimeType = mimeType;
        }

        public Stream Stream { get; }
        public string MimeType { get; }
    }

    public class CameraInformation
    {
        public string Model { get; set; }

        [JsonProperty("sw")]
        public string FirmwareVersion { get; set; }

        [JsonProperty("hw")]
        public int HardwareVersion { get; set; }

        [JsonProperty("mac")]
        public string MacAddress { get; set; }

        [JsonProperty("bt_mac")]
        public string BluetoothMac { get; set; }
    }
}