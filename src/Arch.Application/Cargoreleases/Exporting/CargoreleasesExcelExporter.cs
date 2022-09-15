using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Arch.DataExporting.Excel.NPOI;
using Arch.Cargoreleases.Dtos;
using Arch.Dto;
using Arch.Storage;

namespace Arch.Cargoreleases.Exporting
{
    public class CargoreleasesExcelExporter : NpoiExcelExporterBase, ICargoreleasesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public CargoreleasesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetCargoreleaseForViewDto> cargoreleases)
        {
            return CreateExcelPackage(
                "Cargoreleases.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Cargoreleases"));

                    AddHeader(
                        sheet,
                        L("priority"),
                        L("blno"),
                        L("invoicevalidity"),
                        L("terminal"),
                        L("deliveryorderno"),
                        L("customercode"),
                        L("agencycode"),
                        L("agentcode"),
                        L("entrybyrepcode"),
                        L("entrymode"),
                        L("entrydate"),
                        L("approveby"),
                        L("approvecomment"),
                        L("approvedate"),
                        L("updatedby"),
                        L("updatecomment"),
                        L("updatedate"),
                        L("releaseby"),
                        L("releasestatus"),
                        L("releasecomment"),
                        L("releasedate"),
                        L("status"),
                        L("ipaddr")
                        );

                    AddObjects(
                        sheet, cargoreleases,
                        _ => _.Cargorelease.priority,
                        _ => _.Cargorelease.blno,
                        _ => _timeZoneConverter.Convert(_.Cargorelease.invoicevalidity, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Cargorelease.terminal,
                        _ => _.Cargorelease.deliveryorderno,
                        _ => _.Cargorelease.customercode,
                        _ => _.Cargorelease.agencycode,
                        _ => _.Cargorelease.agentcode,
                        _ => _.Cargorelease.entrybyrepcode,
                        _ => _.Cargorelease.entrymode,
                        _ => _timeZoneConverter.Convert(_.Cargorelease.entrydate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Cargorelease.approveby,
                        _ => _.Cargorelease.approvecomment,
                        _ => _timeZoneConverter.Convert(_.Cargorelease.approvedate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Cargorelease.updatedby,
                        _ => _.Cargorelease.updatecomment,
                        _ => _timeZoneConverter.Convert(_.Cargorelease.updatedate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Cargorelease.releaseby,
                        _ => _.Cargorelease.releasestatus,
                        _ => _.Cargorelease.releasecomment,
                        _ => _timeZoneConverter.Convert(_.Cargorelease.releasedate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Cargorelease.status,
                        _ => _.Cargorelease.ipaddr
                        );

                    for (var i = 1; i <= cargoreleases.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[3], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(3); for (var i = 1; i <= cargoreleases.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[11], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(11); for (var i = 1; i <= cargoreleases.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[14], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(14); for (var i = 1; i <= cargoreleases.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[17], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(17); for (var i = 1; i <= cargoreleases.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[21], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(21);
                });
        }
    }
}