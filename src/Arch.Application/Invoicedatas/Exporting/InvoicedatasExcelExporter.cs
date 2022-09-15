using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Arch.DataExporting.Excel.NPOI;
using Arch.Invoicedatas.Dtos;
using Arch.Dto;
using Arch.Storage;

namespace Arch.Invoicedatas.Exporting
{
    public class InvoicedatasExcelExporter : NpoiExcelExporterBase, IInvoicedatasExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public InvoicedatasExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetInvoicedataForViewDto> invoicedatas)
        {
            return CreateExcelPackage(
                "Invoicedatas.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Invoicedatas"));

                    AddHeader(
                        sheet,
                        L("blno"),
                        L("validitydate"),
                        L("amount"),
                        L("amountdue"),
                        L("status"),
                        L("invpaiddate"),
                        L("userid"),
                        L("waiver"),
                        L("waivedamount"),
                        L("waivedby"),
                        L("waivecomment"),
                        L("datewaived"),
                        L("CreationTime"),
                        L("LastModificationTime"),
                        (L("Billoflading")) + L("blno")
                        );

                    AddObjects(
                        sheet, invoicedatas,
                        _ => _.Invoicedata.blno,
                        _ => _timeZoneConverter.Convert(_.Invoicedata.validitydate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Invoicedata.amount,
                        _ => _.Invoicedata.amountdue,
                        _ => _.Invoicedata.status,
                        _ => _timeZoneConverter.Convert(_.Invoicedata.invpaiddate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Invoicedata.userid,
                        _ => _.Invoicedata.waiver,
                        _ => _.Invoicedata.waivedamount,
                        _ => _.Invoicedata.waivedby,
                        _ => _.Invoicedata.waivecomment,
                        _ => _timeZoneConverter.Convert(_.Invoicedata.datewaived, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.Invoicedata.CreationTime, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.Invoicedata.LastModificationTime, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Billofladingblno
                        );

                    for (var i = 1; i <= invoicedatas.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[2], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(2); for (var i = 1; i <= invoicedatas.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[6], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(6); for (var i = 1; i <= invoicedatas.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[12], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(12); for (var i = 1; i <= invoicedatas.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[13], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(13); for (var i = 1; i <= invoicedatas.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[14], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(14);
                });
        }
    }
}