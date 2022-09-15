using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arch.Migrations
{
    public partial class Added_Invoicedata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "invoicedatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    blno = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    validitydate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    amount = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    amountdue = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    invpaiddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userid = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    waiver = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    waivedamount = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    waivedby = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    waivecomment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    datewaived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoicedatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "longstandings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    customercode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    blno = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    containerno = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    containertype = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    freetext = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    lastmove = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    days = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    releasedby = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    releasedreason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    releasecomment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    validitydate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    shipoperator = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_longstandings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_invoicedatas_TenantId",
                table: "invoicedatas",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_longstandings_TenantId",
                table: "longstandings",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "invoicedatas");

            migrationBuilder.DropTable(
                name: "longstandings");
        }
    }
}
