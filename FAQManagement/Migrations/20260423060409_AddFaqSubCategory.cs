using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAQManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddFaqSubCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FaqSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCategoryImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCategorySequence = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaqSubCategories", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaqSubCategories");
        }
    }
}
