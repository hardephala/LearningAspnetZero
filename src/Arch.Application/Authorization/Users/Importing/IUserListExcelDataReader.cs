using System.Collections.Generic;
using Arch.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace Arch.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
