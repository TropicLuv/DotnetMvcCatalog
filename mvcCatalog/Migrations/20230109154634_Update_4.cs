using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvcCatalog.Migrations
{
    /// <inheritdoc />
    public partial class Update4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryCategoryId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ParentCategoryCategoryId",
                table: "Categories",
                newName: "ParentCategoryIDCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentCategoryCategoryId",
                table: "Categories",
                newName: "IX_Categories_ParentCategoryIDCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryIDCategoryId",
                table: "Categories",
                column: "ParentCategoryIDCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryIDCategoryId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ParentCategoryIDCategoryId",
                table: "Categories",
                newName: "ParentCategoryCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentCategoryIDCategoryId",
                table: "Categories",
                newName: "IX_Categories_ParentCategoryCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryCategoryId",
                table: "Categories",
                column: "ParentCategoryCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");
        }
    }
}
