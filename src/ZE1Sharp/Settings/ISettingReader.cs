namespace ZE1Sharp
{
    using System.Threading.Tasks;

    using ZE1Sharp.Models;

    public interface ISettingReader<T, TValue>
        where T : SettingBase<TValue>, new()
    {
        Task<ISettingWriter<T, TValue>> ReadAsync();
    }
}