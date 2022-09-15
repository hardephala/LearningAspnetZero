using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Arch.DataExporting.Excel.NPOI;
using Arch.Longstandings.Dtos;
using Arch.Dto;
using Arch.Storage;

namespace Arch.Longstandings.Exporting
{
    public class LongstandingsExcelExporter : NpoiExcelExporterBase, ILongstandingsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public LongstandingsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetLongstandingForViewDto> longstandings)
        {
            return CreateExcelPackage(
                "Longstandings.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Longstandings"));

                    AddHeader(
                        sheet,
                        L("customercode"),
                        L("blno"),
                        L("containerno"),
                        L("containertype"),
                        L("freetext"),
                        L("location"),
                        L("lastmove"),
                        L("days"),
                        L("status"),
                        L("releasedby"),
                        L("releasedreason"),
                        L("releasecomment"),
                        L("validitydate"),
                        L("shipoperator")
                        );

                    AddObjects(
                        sheet, longstandings,
                        _ => _.Longstanding.customercode,
                        _ => _.Longstanding.blno,
                        _ => _.Longstanding.containerno,
                        _ => _.Longstanding.containertype,
                        _ => _.Longstanding.freetext,
                        _ => _.Longstanding.location,
                        _ => _.Longstanding.lastmove,
                        _ => _.Longstanding.days,
                        _ => _.Longstanding.status,
                        _ => _.Longstanding.releasedby,
                        _ => _.Longstanding.releasedreason,
                        _ => _.Longstanding.releasecomment,
                        _ => _timeZoneConverter.Convert(_.Longstanding.validitydate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Longstanding.shipoperator
                        );

                    for (var i = 1; i <= longstandings.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[13], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(13);
                });
        }
    }
}