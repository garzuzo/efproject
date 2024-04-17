using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace efproject.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Task",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Description", "Name", "Points" },
                values: new object[,]
                {
                    { new Guid("2f220809-95c8-40fe-8807-e466234cff50"), null, "Personal activities", 8 },
                    { new Guid("2f220809-95c8-40fe-8807-e466234cff54"), null, "Pending activities", 5 }
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "TaskId", "CategoryId", "CreationDate", "Description", "DueDate", "TaskPriority", "Title" },
                values: new object[,]
                {
                    { new Guid("2f220809-95c8-40fe-8807-e466234cff20"), new Guid("2f220809-95c8-40fe-8807-e466234cff50"), new DateTime(2024, 4, 16, 13, 31, 20, 913, DateTimeKind.Local).AddTicks(540), null, new DateTime(2024, 4, 16, 0, 0, 0, 0, DateTimeKind.Local), 2, "Check .net courses" },
                    { new Guid("2f220809-95c8-40fe-8807-e466234cff30"), new Guid("2f220809-95c8-40fe-8807-e466234cff54"), new DateTime(2024, 4, 16, 13, 31, 20, 913, DateTimeKind.Local).AddTicks(500), null, new DateTime(2024, 4, 16, 0, 0, 0, 0, DateTimeKind.Local), 1, "Check pending activites" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "TaskId",
                keyValue: new Guid("2f220809-95c8-40fe-8807-e466234cff20"));

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "TaskId",
                keyValue: new Guid("2f220809-95c8-40fe-8807-e466234cff30"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("2f220809-95c8-40fe-8807-e466234cff50"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("2f220809-95c8-40fe-8807-e466234cff54"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Task",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
