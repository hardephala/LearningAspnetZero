using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Arch.Dto;

namespace Arch.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
