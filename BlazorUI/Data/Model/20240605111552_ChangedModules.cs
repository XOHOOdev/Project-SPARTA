using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sparta.BlazorUI.Data.Model
{
    /// <inheritdoc />
    public partial class ChangedModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeId",
                table: "MD_Modules",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ModuleType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MD_Modules_TypeId",
                table: "MD_Modules",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MD_Modules_ModuleType_TypeId",
                table: "MD_Modules",
                column: "TypeId",
                principalTable: "ModuleType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MD_Modules_ModuleType_TypeId",
                table: "MD_Modules");

            migrationBuilder.DropTable(
                name: "ModuleType");

            migrationBuilder.DropIndex(
                name: "IX_MD_Modules_TypeId",
                table: "MD_Modules");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "MD_Modules");
        }
    }
}
