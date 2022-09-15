using System.Collections.Generic;
using Arch.Authorization.Users.Dto;
using Arch.Dto;

namespace Arch.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}