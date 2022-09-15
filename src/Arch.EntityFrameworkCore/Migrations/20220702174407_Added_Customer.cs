using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arch.Migrations
{
    public partial class Added_Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    customerrole = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    customercode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    customername = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    customergroupcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    customergroupname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    primaryemail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    altemail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    phonenumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    accounttype = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    linkedcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", maxLength: 999999, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customers_TenantId",
                table: "customers",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customers");
        }
    }
}
