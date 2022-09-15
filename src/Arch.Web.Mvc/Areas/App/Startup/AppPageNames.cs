namespace Arch.Web.Areas.App.Startup
{
    public class AppPageNames
    {
        public static class Common
        {
            public const string Customers = "Administration.Customers.Customers";
            public const string ErInvoiceDatas = "ErInvoiceDatas.ErInvoiceDatas";
            public const string Invoicedatas = "Invoicedatas.Invoicedatas";
            public const string Longstandings = "Longstandings.Longstandings";
            public const string Invoices = "Invoices.Invoices";
            public const string Donotreleases = "Donotreleases.Donotreleases";
            public const string Cargoreleases = "Cargoreleases.Cargoreleases";
            public const string Billofladings = "Billofladings.Billofladings";
            public const string Administration = "Administration";
            public const string Roles = "Administration.Roles";
            public const string Users = "Administration.Users";
            public const string AuditLogs = "Administration.AuditLogs";
            public const string OrganizationUnits = "Administration.OrganizationUnits";
            public const string Languages = "Administration.Languages";
            public const string DemoUiComponents = "Administration.DemoUiComponents";
            public const string UiCustomization = "Administration.UiCustomization";
            public const string WebhookSubscriptions = "Administration.WebhookSubscriptions";
            public const string DynamicProperties = "Administration.DynamicProperties";
            public const string DynamicEntityProperties = "Administration.DynamicEntityProperties";
        }

        public static class Host
        {
            public const string Tenants = "Tenants";
            public const string Editions = "Editions";
            public const string Maintenance = "Administration.Maintenance";
            public const string Settings = "Administration.Settings.Host";
            public const string Dashboard = "Dashboard";
        }

        public static class Tenant
        {
            public const string Dashboard = "Dashboard.Tenant";
            public const string Settings = "Administration.Settings.Tenant";
            public const string SubscriptionManagement = "Administration.SubscriptionManagement.Tenant";
        }
    }
}