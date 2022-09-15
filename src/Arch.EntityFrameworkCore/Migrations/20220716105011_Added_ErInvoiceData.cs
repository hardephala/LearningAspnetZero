using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arch.Migrations
{
    public partial class Added_ErInvoiceData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BillofladingId",
                table: "invoicedatas",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ErInvoiceDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    validityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amountdue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillofladingId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErInvoiceDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErInvoiceDatas_Billofladings_BillofladingId",
                        column: x => x.BillofladingId,
                        principalTable: "Billofladings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_invoicedatas_BillofladingId",
                table: "invoicedatas",
                column: "BillofladingId");

            migrationBuilder.CreateIndex(
                name: "IX_ErInvoiceDatas_BillofladingId",
                table: "ErInvoiceDatas",
                column: "BillofladingId");

            migrationBuilder.CreateIndex(
                name: "IX_ErInvoiceDatas_TenantId",
                table: "ErInvoiceDatas",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_invoicedatas_Billofladings_BillofladingId",
                table: "invoicedatas",
                column: "BillofladingId",
                principalTable: "Billofladings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoicedatas_Billofladings_BillofladingId",
                table: "invoicedatas");

            migrationBuilder.DropTable(
                name: "ErInvoiceDatas");

            migrationBuilder.DropIndex(
                name: "IX_invoicedatas_BillofladingId",
                table: "invoicedatas");

            migrationBuilder.DropColumn(
                name: "BillofladingId",
                table: "invoicedatas");
        }
    }
}
