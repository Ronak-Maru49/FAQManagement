using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAQManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FaqCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategorySequence = table.Column<int>(type: "int", nullable: false),
                    IsParent = table.Column<bool>(type: "bit", nullable: false),
                    ParentSequence = table.Column<int>(type: "int", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaqCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaqCategories_FaqCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "FaqCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Faqs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    SubCategoryId = table.Column<int>(type: "int", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    FaqSubCategoryId = table.Column<int>(type: "int", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faqs", x => x.Id);
                });

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
                    SubCategorySequence = table.Column<int>(type: "int", nullable: false),
                    Questions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ans = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaqCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaqSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaqSubCategories_FaqCategories_FaqCategoryId",
                        column: x => x.FaqCategoryId,
                        principalTable: "FaqCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FaqCategories_ParentId",
                table: "FaqCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_FaqSubCategories_FaqCategoryId",
                table: "FaqSubCategories",
                column: "FaqCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Faqs");

            migrationBuilder.DropTable(
                name: "FaqSubCategories");

            migrationBuilder.DropTable(
                name: "FaqCategories");
        }
    }
}
