using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace daolibrary.Migrations
{
    /// <inheritdoc />
    public partial class ultimasmodificaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Generos_GenreId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Qualifies_StarId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.RenameColumn(
                name: "Star",
                table: "Qualifies",
                newName: "Stars");

            migrationBuilder.RenameColumn(
                name: "UserStatus",
                table: "Persons",
                newName: "userStatus");

            migrationBuilder.RenameColumn(
                name: "StarId",
                table: "Movies",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_StarId",
                table: "Movies",
                newName: "IX_Movies_UserId");

            migrationBuilder.AddColumn<long>(
                name: "MovieId",
                table: "Qualifies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<int>(
                name: "userStatus",
                table: "Persons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Persons",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "UserStatus",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasOscar",
                table: "Movies",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifies_MovieId",
                table: "Qualifies",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Persons_UserId",
                table: "Movies",
                column: "UserId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Qualifies_Movies_MovieId",
                table: "Qualifies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Persons_UserId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Qualifies_Movies_MovieId",
                table: "Qualifies");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Qualifies_MovieId",
                table: "Qualifies");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Qualifies");

            migrationBuilder.DropColumn(
                name: "UserStatus",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "HasOscar",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "Stars",
                table: "Qualifies",
                newName: "Star");

            migrationBuilder.RenameColumn(
                name: "userStatus",
                table: "Persons",
                newName: "UserStatus");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Movies",
                newName: "StarId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_UserId",
                table: "Movies",
                newName: "IX_Movies_StarId");

            migrationBuilder.AlterColumn<int>(
                name: "UserStatus",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Email",
                keyValue: null,
                column: "Email",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Persons",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Generos_GenreId",
                table: "Movies",
                column: "GenreId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Qualifies_StarId",
                table: "Movies",
                column: "StarId",
                principalTable: "Qualifies",
                principalColumn: "Id");
        }
    }
}
