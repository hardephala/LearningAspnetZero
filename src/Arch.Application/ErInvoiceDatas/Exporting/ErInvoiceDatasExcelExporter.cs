using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Arch.DataExporting.Excel.NPOI;
using Arch.ErInvoiceDatas.Dtos;
using Arch.Dto;
using Arch.Storage;

namespace Arch.ErInvoiceDatas.Exporting
{
    public class ErInvoiceDatasExcelExporter : NpoiExcelExporterBase, IErInvoiceDatasExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ErInvoiceDatasExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetErInvoiceDataForViewDto> erInvoiceDatas)
        {
            return CreateExcelPackage(
                "ErInvoiceDatas.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("ErInvoiceDatas"));

                    AddHeader(
                        sheet,
                        L("validityDate"),
                        L("amount"),
                        L("amountdue"),
                        L("status"),
                        (L("Billoflading")) + L("blno")
                        );

                    AddObjects(
                        sheet, erInvoiceDatas,
                        _ => _timeZoneConverter.Convert(_.ErInvoiceData.validityDate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.ErInvoiceData.amount,
                        _ => _.ErInvoiceData.amountdue,
                        _ => _.ErInvoiceData.status,
                        _ => _.Billofladingblno
                        );

                    for (var i = 1; i <= erInvoiceDatas.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[1], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(1);
                });
        }
    }
}