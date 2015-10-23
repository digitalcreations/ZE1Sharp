namespace ZE1Sharp.Models
{
    public class RangeSetting : SettingBase<int>
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Step { get; set; }
    }
}