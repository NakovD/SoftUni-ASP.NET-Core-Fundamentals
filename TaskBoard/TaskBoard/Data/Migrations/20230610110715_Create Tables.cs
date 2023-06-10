using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoard.Data.Migrations
{
    public partial class CreateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ba3dcbf6-63f8-4008-87dc-39e943fe49ca", 0, "ed79f027-4a42-40d1-8378-0e806706ab86", null, false, false, null, null, "TEST@ABV.BG", "AQAAAAEAACcQAAAAEJRIS8i3mdCKaUeSBwUaXwhX9MxgcDZmdksF4ym3DtO3S2u6WVvLb1wZAZMnoPXadw==", null, false, "3dd76b45-4c4c-41d9-a74c-4607302dfc9d", false, "test@abv.bg" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 11, 22, 14, 7, 15, 695, DateTimeKind.Local).AddTicks(8022), "Implement better styling for all public pages", "ba3dcbf6-63f8-4008-87dc-39e943fe49ca", "Improve CSS styles" },
                    { 2, 1, new DateTime(2023, 1, 10, 14, 7, 15, 695, DateTimeKind.Local).AddTicks(8060), "Create android client app for the taskboard RestFull APi", "ba3dcbf6-63f8-4008-87dc-39e943fe49ca", "Android Client App" },
                    { 3, 2, new DateTime(2006, 10, 10, 14, 7, 15, 695, DateTimeKind.Local).AddTicks(8065), "Create windows forms desktop app client for the taskboard Restfull API", "ba3dcbf6-63f8-4008-87dc-39e943fe49ca", "Desktop CLient app" },
                    { 4, 3, new DateTime(2022, 6, 10, 14, 7, 15, 695, DateTimeKind.Local).AddTicks(8101), "Implement [Create task] page for adding new tasks", "ba3dcbf6-63f8-4008-87dc-39e943fe49ca", "Create tasks" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ba3dcbf6-63f8-4008-87dc-39e943fe49ca");
        }
    }
}
