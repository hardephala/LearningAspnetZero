using System.Threading.Tasks;
using Abp.Dependency;

namespace Arch.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}