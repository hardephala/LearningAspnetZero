using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arch.Migrations
{
    public partial class Added_Donotrelease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "donotreleases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    blno = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    releasedby = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    releasecomment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    blockedby = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    blockedcomment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    blockeddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    blockedreference = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    blcomment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_donotreleases", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_donotreleases_TenantId",
                table: "donotreleases",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "donotreleases");
        }
    }
}
