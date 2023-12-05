using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleOLX.Migrations
{
    /// <inheritdoc />
    public partial class NoWatchers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvertUser",
                columns: table => new
                {
                    AdvertsWatchedId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersWatchersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertUser", x => new { x.AdvertsWatchedId, x.UsersWatchersId });
                    table.ForeignKey(
                        name: "FK_AdvertUser_Adverts_AdvertsWatchedId",
                        column: x => x.AdvertsWatchedId,
                        principalTable: "Adverts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertUser_AspNetUsers_UsersWatchersId",
                        column: x => x.UsersWatchersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertUser_UsersWatchersId",
                table: "AdvertUser",
                column: "UsersWatchersId");
        }
    }
}
