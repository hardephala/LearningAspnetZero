using Arch.ErInvoiceDatas;
using Arch.Invoicedatas;
using Arch.Longstandings;
using Arch.Donotreleases;
using Arch.Cargoreleases;
using Arch.Billofladings;
using Arch.Customers;
using Abp.IdentityServer4vNext;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Arch.Authorization.Delegation;
using Arch.Authorization.Roles;
using Arch.Authorization.Users;
using Arch.Chat;
using Arch.Editions;
using Arch.Friendships;
using Arch.MultiTenancy;
using Arch.MultiTenancy.Accounting;
using Arch.MultiTenancy.Payments;
using Arch.Storage;

namespace Arch.EntityFrameworkCore
{
    public class ArchDbContext : AbpZeroDbContext<Tenant, Role, User, ArchDbContext>, IAbpPersistedGrantDbContext
    {
        public virtual DbSet<ErInvoiceData> ErInvoiceDatas { get; set; }

        public virtual DbSet<Invoicedata> Invoicedatas { get; set; }

        public virtual DbSet<Longstanding> Longstandings { get; set; }

        public virtual DbSet<Donotrelease> Donotreleases { get; set; }

        public virtual DbSet<Cargorelease> Cargoreleases { get; set; }

        public virtual DbSet<Billoflading> Billofladings { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }

        public virtual DbSet<UserDelegation> UserDelegations { get; set; }

        public virtual DbSet<RecentPassword> RecentPasswords { get; set; }

        public ArchDbContext(DbContextOptions<ArchDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(c =>
            {
                c.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<ErInvoiceData>(x =>
                       {
                           x.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Invoicedata>(i =>
                       {
                           i.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Longstanding>(l =>
                       {
                           l.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Invoicedata>(i =>
                       {
                           i.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Longstanding>(l =>
                       {
                           l.HasIndex(e => new { e.TenantId });
                       });

            modelBuilder.Entity<Donotrelease>(d =>
                       {
                           d.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Cargorelease>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Billoflading>(b =>
                       {
                           b.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Customer>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<BinaryObject>(b =>
                       {
                           b.HasIndex(e => new { e.TenantId });
                       });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.Entity<SubscriptionPaymentExtensionData>(b =>
            {
                b.HasQueryFilter(m => !m.IsDeleted)
                    .HasIndex(e => new { e.SubscriptionPaymentId, e.Key, e.IsDeleted })
                    .IsUnique();
            });

            modelBuilder.Entity<UserDelegation>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.SourceUserId });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId });
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}