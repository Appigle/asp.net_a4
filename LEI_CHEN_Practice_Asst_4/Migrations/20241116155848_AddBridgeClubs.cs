using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LEI_CHEN_Practice_Asst_4.Migrations
{
    /// <inheritdoc />
    public partial class AddBridgeClubs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BridgeClubs",
                columns: table => new
                {
                    ClubID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClubName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CityName = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BridgeClubs", x => x.ClubID);
                    table.ForeignKey(
                        name: "FK_BridgeClubs_Cities_CityName",
                        column: x => x.CityName,
                        principalTable: "Cities",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBridgeClubs",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    ClubID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBridgeClubs", x => new { x.UserID, x.ClubID });
                    table.ForeignKey(
                        name: "FK_UserBridgeClubs_BridgeClubs_ClubID",
                        column: x => x.ClubID,
                        principalTable: "BridgeClubs",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBridgeClubs_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BridgeClubs_CityName",
                table: "BridgeClubs",
                column: "CityName");

            migrationBuilder.CreateIndex(
                name: "IX_UserBridgeClubs_ClubID",
                table: "UserBridgeClubs",
                column: "ClubID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBridgeClubs");

            migrationBuilder.DropTable(
                name: "BridgeClubs");
        }
    }
}
