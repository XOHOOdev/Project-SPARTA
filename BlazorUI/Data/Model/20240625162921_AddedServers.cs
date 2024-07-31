using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sparta.BlazorUI.Data.Model
{
    /// <inheritdoc />
    public partial class AddedServers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ServerId",
                table: "MD_Modules",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SV_Servers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SV_Servers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MD_Modules_ServerId",
                table: "MD_Modules",
                column: "ServerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MD_Modules_SV_Servers_ServerId",
                table: "MD_Modules",
                column: "ServerId",
                principalTable: "SV_Servers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MD_Modules_SV_Servers_ServerId",
                table: "MD_Modules");

            migrationBuilder.DropTable(
                name: "SV_Servers");

            migrationBuilder.DropIndex(
                name: "IX_MD_Modules_ServerId",
                table: "MD_Modules");

            migrationBuilder.DropColumn(
                name: "ServerId",
                table: "MD_Modules");
        }
    }
}
