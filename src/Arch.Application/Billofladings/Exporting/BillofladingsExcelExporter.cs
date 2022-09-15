using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Arch.DataExporting.Excel.NPOI;
using Arch.Billofladings.Dtos;
using Arch.Dto;
using Arch.Storage;

namespace Arch.Billofladings.Exporting
{
    public class BillofladingsExcelExporter : NpoiExcelExporterBase, IBillofladingsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public BillofladingsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetBillofladingForViewDto> billofladings)
        {
            return CreateExcelPackage(
                "Billofladings.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Billofladings"));

                    AddHeader(
                        sheet,
                        L("shipmentno"),
                        L("blno"),
                        L("equipmentno"),
                        L("equipmenttype"),
                        L("equipmentsize"),
                        L("shipperowned"),
                        L("shipoperator"),
                        L("servicecontract"),
                        L("spotbooking"),
                        L("consigneecode"),
                        L("dischargeportcode"),
                        L("dischargeportname"),
                        L("placeofdeliverycode"),
                        L("placeofdeliveryname"),
                        L("finalvesselcode"),
                        L("finalvesselname"),
                        L("finalvesselvoyage"),
                        L("finalvesseleta"),
                        L("partpartstatus"),
                        L("partpartref"),
                        L("depositpayable"),
                        L("depositdueamount"),
                        L("depositwaivedamount"),
                        L("depositwaivedreason"),
                        L("depositwaivedby"),
                        L("depositpaymentstatus"),
                        L("releaseoutstandingstatus"),
                        L("releaseoutstandingreason"),
                        L("releaseoutstandingby"),
                        L("releaselongstandingstatus"),
                        L("releaselongstandingreason"),
                        L("releaselongstandingby"),
                        L("blnotype"),
                        L("blnosubmitstatus"),
                        L("blnosubmittedto"),
                        L("blnosubmittedby"),
                        L("blnosubmitref")
                        );

                    AddObjects(
                        sheet, billofladings,
                        _ => _.Billoflading.shipmentno,
                        _ => _.Billoflading.blno,
                        _ => _.Billoflading.equipmentno,
                        _ => _.Billoflading.equipmenttype,
                        _ => _.Billoflading.equipmentsize,
                        _ => _.Billoflading.shipperowned,
                        _ => _.Billoflading.shipoperator,
                        _ => _.Billoflading.servicecontract,
                        _ => _.Billoflading.spotbooking,
                        _ => _.Billoflading.consigneecode,
                        _ => _.Billoflading.dischargeportcode,
                        _ => _.Billoflading.dischargeportname,
                        _ => _.Billoflading.placeofdeliverycode,
                        _ => _.Billoflading.placeofdeliveryname,
                        _ => _.Billoflading.finalvesselcode,
                        _ => _.Billoflading.finalvesselname,
                        _ => _.Billoflading.finalvesselvoyage,
                        _ => _timeZoneConverter.Convert(_.Billoflading.finalvesseleta, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Billoflading.partpartstatus,
                        _ => _.Billoflading.partpartref,
                        _ => _.Billoflading.depositpayable,
                        _ => _.Billoflading.depositdueamount,
                        _ => _.Billoflading.depositwaivedamount,
                        _ => _.Billoflading.depositwaivedreason,
                        _ => _.Billoflading.depositwaivedby,
                        _ => _.Billoflading.depositpaymentstatus,
                        _ => _.Billoflading.releaseoutstandingstatus,
                        _ => _.Billoflading.releaseoutstandingreason,
                        _ => _.Billoflading.releaseoutstandingby,
                        _ => _.Billoflading.releaselongstandingstatus,
                        _ => _.Billoflading.releaselongstandingreason,
                        _ => _.Billoflading.releaselongstandingby,
                        _ => _.Billoflading.blnotype,
                        _ => _.Billoflading.blnosubmitstatus,
                        _ => _.Billoflading.blnosubmittedto,
                        _ => _.Billoflading.blnosubmittedby,
                        _ => _.Billoflading.blnosubmitref
                        );

                    for (var i = 1; i <= billofladings.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[18], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(18);
                });
        }
    }
}