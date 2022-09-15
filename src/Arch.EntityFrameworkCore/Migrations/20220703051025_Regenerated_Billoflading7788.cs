using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arch.Migrations
{
    public partial class Regenerated_Billoflading7788 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Billofladings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    shipmentno = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    blno = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    equipmentno = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    equipmenttype = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    equipmentsize = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    shipperowned = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    shipoperator = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    servicecontract = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    spotbooking = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    consigneecode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    dischargeportcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    dischargeportname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    placeofdeliverycode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    placeofdeliveryname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    finalvesselcode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    finalvesselname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    finalvesselvoyage = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    finalvesseleta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    partpartstatus = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    partpartref = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    depositpayable = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    depositdueamount = table.Column<int>(type: "int", nullable: false),
                    depositwaivedamount = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    depositwaivedreason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    depositwaivedby = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    depositpaymentstatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    releaseoutstandingstatus = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    releaseoutstandingreason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    releaseoutstandingby = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    releaselongstandingstatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    releaselongstandingreason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    releaselongstandingby = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    blnotype = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    blnosubmitstatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    blnosubmittedto = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    blnosubmittedby = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    blnosubmitref = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billofladings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Billofladings_TenantId",
                table: "Billofladings",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Billofladings");
        }
    }
}
