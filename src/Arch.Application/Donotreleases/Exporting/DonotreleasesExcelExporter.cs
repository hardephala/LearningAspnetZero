using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Arch.DataExporting.Excel.NPOI;
using Arch.Donotreleases.Dtos;
using Arch.Dto;
using Arch.Storage;

namespace Arch.Donotreleases.Exporting
{
    public class DonotreleasesExcelExporter : NpoiExcelExporterBase, IDonotreleasesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public DonotreleasesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetDonotreleaseForViewDto> donotreleases)
        {
            return CreateExcelPackage(
                "Donotreleases.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Donotreleases"));

                    AddHeader(
                        sheet,
                        L("blno"),
                        L("status"),
                        L("releasedby"),
                        L("releasecomment"),
                        L("blockedby"),
                        L("blockedcomment"),
                        L("blockeddate"),
                        L("blockedreference"),
                        L("blcomment")
                        );

                    AddObjects(
                        sheet, donotreleases,
                        _ => _.Donotrelease.blno,
                        _ => _.Donotrelease.status,
                        _ => _.Donotrelease.releasedby,
                        _ => _.Donotrelease.releasecomment,
                        _ => _.Donotrelease.blockedby,
                        _ => _.Donotrelease.blockedcomment,
                        _ => _timeZoneConverter.Convert(_.Donotrelease.blockeddate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Donotrelease.blockedreference,
                        _ => _.Donotrelease.blcomment
                        );

                    for (var i = 1; i <= donotreleases.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[7], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(7);
                });
        }
    }
}