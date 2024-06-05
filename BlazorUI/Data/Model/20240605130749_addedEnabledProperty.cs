using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sparta.BlazorUI.Data.Model
{
    /// <inheritdoc />
    public partial class addedEnabledProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "MD_Modules",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "MD_Modules");
        }
    }
}
