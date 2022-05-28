using Microsoft.EntityFrameworkCore.Migrations;

namespace API.data.migrations
{
    public partial class ExtendUserEntity1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lookingFor",
                table: "users",
                newName: "LookingFor");

            migrationBuilder.RenameColumn(
                name: "KonwnAs",
                table: "users",
                newName: "KnownAs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LookingFor",
                table: "users",
                newName: "lookingFor");

            migrationBuilder.RenameColumn(
                name: "KnownAs",
                table: "users",
                newName: "KonwnAs");
        }
    }
}
