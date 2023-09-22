using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcbManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(type: "uuid", nullable: false),
                        Name = table.Column<string>(type: "text", nullable: false),
                        Surname = table.Column<string>(type: "text", nullable: false),
                        Email = table.Column<string>(type: "text", nullable: false),
                        Password = table.Column<string>(type: "text", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(type: "uuid", nullable: false),
                        ImageName = table.Column<string>(type: "text", nullable: false),
                        ImagePath = table.Column<string>(type: "text", nullable: false),
                        UserId = table.Column<Guid>(type: "uuid", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(type: "uuid", nullable: false),
                        ImageId = table.Column<Guid>(type: "uuid", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "PcbDefects",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(type: "uuid", nullable: false),
                        PcbDefectType = table.Column<int>(type: "integer", nullable: false),
                        ReportId = table.Column<Guid>(type: "uuid", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcbDefects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PcbDefects_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserId",
                table: "Images",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_PcbDefects_ReportId",
                table: "PcbDefects",
                column: "ReportId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ImageId",
                table: "Reports",
                column: "ImageId",
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "PcbDefects");

            migrationBuilder.DropTable(name: "Reports");

            migrationBuilder.DropTable(name: "Images");

            migrationBuilder.DropTable(name: "Users");
        }
    }
}
