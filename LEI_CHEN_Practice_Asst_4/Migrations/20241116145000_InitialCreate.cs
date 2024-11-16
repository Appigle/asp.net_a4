using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LEI_CHEN_Practice_Asst_4.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ID);
                    table.UniqueConstraint("AK_Provinces_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    ProvCode = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                    table.UniqueConstraint("AK_Cities_Name", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_ProvCode",
                        column: x => x.ProvCode,
                        principalTable: "Provinces",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CityName = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Cities_CityName",
                        column: x => x.CityName,
                        principalTable: "Cities",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "ID", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "AB", "Alberta" },
                    { 2, "BC", "British Columbia" },
                    { 3, "MB", "Manitoba" },
                    { 4, "NB", "New Brunswick" },
                    { 5, "NL", "Newfoundland and Labrador" },
                    { 6, "NT", "Northwest Territories" },
                    { 7, "NS", "Nova Scotia" },
                    { 8, "NU", "Nunavut" },
                    { 9, "ON", "Ontario" },
                    { 10, "PE", "Prince Edward Island" },
                    { 11, "QC", "Quebec" },
                    { 12, "SK", "Saskatchewan" },
                    { 13, "YT", "Yukon" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name", "ProvCode" },
                values: new object[,]
                {
                    { 1, "Ardrossan", "AB" },
                    { 2, "Edmonton", "AB" },
                    { 3, "Vancouver", "BC" },
                    { 4, "Victoria", "BC" },
                    { 5, "Winnipeg", "MB" },
                    { 6, "Woodstock", "NB" },
                    { 7, "Whitbourne", "NL" },
                    { 8, "Yellowknife", "NT" },
                    { 9, "Halifax", "NS" },
                    { 10, "Arviat", "NU" },
                    { 11, "Brampton", "ON" },
                    { 12, "Toronto", "ON" },
                    { 13, "Kensington", "PE" },
                    { 14, "Montreal", "QC" },
                    { 15, "Saskatoon", "SK" },
                    { 16, "Mayo", "YT" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Address", "CityName", "Name", "PostalCode" },
                values: new object[,]
                {
                    { 1, "220 Bay St.", "Toronto", "Bill Anderson", "M6H2X4" },
                    { 2, "1007-33 Isabella St.", "Toronto", "Jilal Akbar", "M6H2Z9" },
                    { 3, "2220 Seneca Blvd., .", "Brampton", "Karishma Patel", "M6H7Y8" },
                    { 4, "10-660 Bolton Ave.", "Victoria", "Mahinder Singh", "D6H2C5" },
                    { 5, "99 Agness St.", "Montreal", "Ken Pu", "H1A0B1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvCode",
                table: "Cities",
                column: "ProvCode");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityName",
                table: "Users",
                column: "CityName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
