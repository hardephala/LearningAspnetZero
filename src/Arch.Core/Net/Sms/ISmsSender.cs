using System.Threading.Tasks;

namespace Arch.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}