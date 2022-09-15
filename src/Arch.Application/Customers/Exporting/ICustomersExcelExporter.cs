using System.Collections.Generic;
using Arch.Customers.Dtos;
using Arch.Dto;

namespace Arch.Customers.Exporting
{
    public interface ICustomersExcelExporter
    {
        FileDto ExportToFile(List<GetCustomerForViewDto> customers);
    }
}