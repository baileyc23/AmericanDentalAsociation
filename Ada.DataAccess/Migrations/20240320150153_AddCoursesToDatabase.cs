using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ada.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddCoursesToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instructor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Price4 = table.Column<double>(type: "float", nullable: true),
                    PricePrivate = table.Column<double>(type: "float", nullable: true),
                    Online = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Instructor", "Online", "Price", "Price4", "PricePrivate", "Title" },
                values: new object[,]
                {
                    { 1, "A 3 day Training Class on ASP.NET 8 Development using the latest version of Visual Studio", "Lino Tadros", true, 2250.0, 1999.0, 15000.0, "ASP.NET Core 8 for Developers" },
                    { 2, "A 3 day Training Class of Microsoft Fabric for Data Analytics", "Lino Tadros", true, 3000.0, 2600.0, 25000.0, "Mastering Microsoft Fabric" },
                    { 3, "A 2 day Training Class for Sitefinity development using MVC in ASP.NET", "Lino Tadros", true, 1599.0, 1299.0, 12000.0, "Mastering Sitefinity Development" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
