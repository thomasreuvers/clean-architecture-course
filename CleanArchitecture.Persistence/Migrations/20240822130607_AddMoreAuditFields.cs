using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LeaveAllocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "LeaveAllocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedBy", "DateCreated", "DateModified", "ModifiedBy" },
                values: new object[] { null, new DateTime(2024, 8, 22, 15, 6, 6, 615, DateTimeKind.Local).AddTicks(8570), new DateTime(2024, 8, 22, 15, 6, 6, 615, DateTimeKind.Local).AddTicks(8630), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LeaveAllocations");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "LeaveAllocations");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 4, 25, 9, 56, 35, 981, DateTimeKind.Local).AddTicks(4370), new DateTime(2024, 4, 25, 9, 56, 35, 981, DateTimeKind.Local).AddTicks(4420) });
        }
    }
}
