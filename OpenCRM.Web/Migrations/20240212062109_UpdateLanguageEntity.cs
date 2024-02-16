using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenCRM.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLanguageEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Translations_Key",
                table: "Translations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Translations_Key",
                table: "Translations",
                column: "Key",
                unique: true);
        }
    }
}
