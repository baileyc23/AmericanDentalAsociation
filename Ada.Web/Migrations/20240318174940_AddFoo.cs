using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ada.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddFoo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Foo",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foo",
                table: "Categories");
        }
    }
}
