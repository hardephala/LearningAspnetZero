using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arch.Migrations
{
    public partial class Regenerated_Longstanding3342 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "longstandings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "longstandings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "longstandings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "longstandings",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "longstandings");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "longstandings");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "longstandings");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "longstandings");
        }
    }
}
