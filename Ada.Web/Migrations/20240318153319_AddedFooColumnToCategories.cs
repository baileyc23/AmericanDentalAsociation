using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ada.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddedFooColumnToCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "foo",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "foo",
                table: "Categories");
        }
    }
}
