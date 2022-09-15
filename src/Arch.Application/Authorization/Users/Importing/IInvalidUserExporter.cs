using System.Collections.Generic;
using Arch.Authorization.Users.Importing.Dto;
using Arch.Dto;

namespace Arch.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
