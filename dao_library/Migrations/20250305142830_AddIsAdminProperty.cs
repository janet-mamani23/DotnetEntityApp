using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace daolibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAdminProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Persons_UserId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_UserId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "userStatus",
                table: "Persons",
                newName: "UserStatus");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Persons",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "UserStatus",
                table: "Persons",
                newName: "userStatus");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Movies",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_UserId",
                table: "Movies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Persons_UserId",
                table: "Movies",
                column: "UserId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
