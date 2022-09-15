using System.Threading.Tasks;
using Arch.Sessions.Dto;

namespace Arch.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
