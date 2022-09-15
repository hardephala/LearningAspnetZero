using System.Collections.Generic;
using Abp;
using Arch.Chat.Dto;
using Arch.Dto;

namespace Arch.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
