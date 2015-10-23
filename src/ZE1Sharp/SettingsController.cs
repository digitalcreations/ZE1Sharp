namespace ZE1Sharp
{
    using ZE1Sharp.Models;

    public class SettingsController : ControllerBase
    {
        public SettingsController(IAPICaller apiCaller = null)
            : base(apiCaller)
        {
        }

        #region String settings
        public ISettingReader<StringSetting, string> SSID 
            => new StringSettingReader(this.ApiCaller, "ssid");
        #endregion

        #region Option settings

        public ISettingReader<OptionSetting, string> PhotoSize
            => new OptionSettingReader(this.ApiCaller, "photosize");

        public ISettingReader<OptionSetting, string> MovieFormat
            => new OptionSettingReader(this.ApiCaller, "mov_fmt");

        public ISettingReader<OptionSetting, string> MeterMode
            => new OptionSettingReader(this.ApiCaller, "meter_mode");

        public ISettingReader<OptionSetting, string> Flicker
            => new OptionSettingReader(this.ApiCaller, "flicker");

        public ISettingReader<OptionSetting, string> ISO
            => new OptionSettingReader(this.ApiCaller, "iso");

        public ISettingReader<OptionSetting, string> WhiteBalance
            => new OptionSettingReader(this.ApiCaller, "wb");

        public ISettingReader<OptionSetting, string> AutoFocusMode
            => new OptionSettingReader(this.ApiCaller, "af_mode");

        public ISettingReader<OptionSetting, string> Focus
            => new OptionSettingReader(this.ApiCaller, "focus");

        public ISettingReader<OptionSetting, string> ContinousAutoFocus
            => new OptionSettingReader(this.ApiCaller, "caf");

        public ISettingReader<OptionSetting, string> PhotoQuality
            => new OptionSettingReader(this.ApiCaller, "photo_q");

        public ISettingReader<OptionSetting, string> TimelapseInterval
            => new OptionSettingReader(this.ApiCaller, "photo_tl");

        public ISettingReader<OptionSetting, string> Burst
            => new OptionSettingReader(this.ApiCaller, "burst");

        public ISettingReader<OptionSetting, string> LED
            => new OptionSettingReader(this.ApiCaller, "led");

        public ISettingReader<OptionSetting, string> Beep
            => new OptionSettingReader(this.ApiCaller, "beep");

        public ISettingReader<OptionSetting, string> DriveMode
            => new OptionSettingReader(this.ApiCaller, "drive_mode");
        #endregion

        #region Range settings
        public ISettingReader<RangeSetting, int> Battery
            => new RangeSettingReader(this.ApiCaller, "battery");

        public ISettingReader<RangeSetting, int> ExposureValue
            => new RangeSettingReader(this.ApiCaller, "ev");

        public ISettingReader<RangeSetting, int> Brightness
            => new RangeSettingReader(this.ApiCaller, "brightness");

        public ISettingReader<RangeSetting, int> Saturation
            => new RangeSettingReader(this.ApiCaller, "saturation");

        public ISettingReader<RangeSetting, int> Sharpness
            => new RangeSettingReader(this.ApiCaller, "sharpness");

        public ISettingReader<RangeSetting, int> Contrast
            => new RangeSettingReader(this.ApiCaller, "contrast");
        #endregion
    }
}