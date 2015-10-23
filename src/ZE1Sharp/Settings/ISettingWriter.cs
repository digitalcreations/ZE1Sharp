namespace ZE1Sharp
{
    using System.Threading.Tasks;

    using ZE1Sharp.Models;

    public interface ISettingWriter<T, in TValue>
        where T : SettingBase<TValue>, new()
    {
        T Value { get; }
        Task SetAsync(TValue value);
    }
}