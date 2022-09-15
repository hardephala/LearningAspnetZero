using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Arch.MultiTenancy.Accounting.Dto;

namespace Arch.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
