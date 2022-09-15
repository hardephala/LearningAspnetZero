using Arch.Customers;
using Arch.ErInvoiceDatas;
using Arch.Invoicedatas;
using System;
using System.Linq;
using Abp.Organizations;
using Arch.Authorization.Roles;
using Arch.MultiTenancy;

namespace Arch.EntityHistory
{
    public static class EntityHistoryHelper
    {
        public const string EntityHistoryConfigurationName = "EntityHistory";

        public static readonly Type[] HostSideTrackedTypes =
        {
            typeof(Customer),
            typeof(ErInvoiceData),
            typeof(Invoicedata),
            typeof(OrganizationUnit), typeof(Role), typeof(Tenant)
        };

        public static readonly Type[] TenantSideTrackedTypes =
        {
            typeof(Customer),
            typeof(ErInvoiceData),
            typeof(Invoicedata),
            typeof(OrganizationUnit), typeof(Role)
        };

        public static readonly Type[] TrackedTypes =
            HostSideTrackedTypes
                .Concat(TenantSideTrackedTypes)
                .GroupBy(type => type.FullName)
                .Select(types => types.First())
                .ToArray();
    }
}