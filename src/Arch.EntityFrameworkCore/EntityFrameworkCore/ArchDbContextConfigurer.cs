using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Arch.EntityFrameworkCore
{
    public static class ArchDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ArchDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ArchDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}