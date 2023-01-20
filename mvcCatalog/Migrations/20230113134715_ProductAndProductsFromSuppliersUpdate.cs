using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvcCatalog.Migrations
{
    /// <inheritdoc />
    public partial class ProductAndProductsFromSuppliersUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDiscount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NewPrice",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsDiscount",
                table: "ProductsFromSuppliers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "NewPrice",
                table: "ProductsFromSuppliers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDiscount",
                table: "ProductsFromSuppliers");

            migrationBuilder.DropColumn(
                name: "NewPrice",
                table: "ProductsFromSuppliers");

            migrationBuilder.AddColumn<bool>(
                name: "IsDiscount",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "NewPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
