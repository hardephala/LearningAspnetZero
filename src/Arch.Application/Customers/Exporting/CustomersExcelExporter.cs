using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Arch.DataExporting.Excel.NPOI;
using Arch.Customers.Dtos;
using Arch.Dto;
using Arch.Storage;

namespace Arch.Customers.Exporting
{
    public class CustomersExcelExporter : NpoiExcelExporterBase, ICustomersExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public CustomersExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetCustomerForViewDto> customers)
        {
            return CreateExcelPackage(
                "Customers.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Customers"));

                    AddHeader(
                        sheet,
                        L("customerrole"),
                        L("customercode"),
                        L("customername"),
                        L("customergroupcode"),
                        L("customergroupname"),
                        L("primaryemail"),
                        L("altemail"),
                        L("phonenumber"),
                        L("accounttype"),
                        L("linkedcode"),
                        L("password"),
                        L("status"),
                        L("notes")
                        );

                    AddObjects(
                        sheet, customers,
                        _ => _.Customer.customerrole,
                        _ => _.Customer.customercode,
                        _ => _.Customer.customername,
                        _ => _.Customer.customergroupcode,
                        _ => _.Customer.customergroupname,
                        _ => _.Customer.primaryemail,
                        _ => _.Customer.altemail,
                        _ => _.Customer.phonenumber,
                        _ => _.Customer.accounttype,
                        _ => _.Customer.linkedcode,
                        _ => _.Customer.password,
                        _ => _.Customer.status,
                        _ => _.Customer.notes
                        );

                });
        }
    }
}