using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskItemIsArchived : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isArchived",
                table: "Tasks",
                newName: "IsArchived");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsArchived",
                table: "Tasks",
                newName: "isArchived");
        }
    }
}
