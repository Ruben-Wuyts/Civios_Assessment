using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assessment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    StoragePath = table.Column<string>(type: "TEXT", nullable: true),
                    Classification = table.Column<int>(type: "INTEGER", nullable: false),
                    ClassificationReason = table.Column<string>(type: "TEXT", nullable: false),
                    Metadata_Author = table.Column<string>(type: "TEXT", nullable: true),
                    Metadata_Title = table.Column<string>(type: "TEXT", nullable: true),
                    Metadata_CreationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Metadata_Description = table.Column<string>(type: "TEXT", nullable: true),
                    Metadata_IntendedVisibility = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
