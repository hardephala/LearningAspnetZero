using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arch.Migrations
{
    public partial class Added_Cargorelease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargoreleases",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    priority = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    blno = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    invoicevalidity = table.Column<DateTime>(type: "datetime2", nullable: false),
                    terminal = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    deliveryorderno = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    customercode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    agencycode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    agentcode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    entrybyrepcode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    entrymode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    entrydate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    approveby = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    approvecomment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    approvedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedby = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    updatecomment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    releaseby = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    releasestatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    releasecomment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    releasedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ipaddr = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargoreleases", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargoreleases_TenantId",
                table: "Cargoreleases",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cargoreleases");
        }
    }
}
