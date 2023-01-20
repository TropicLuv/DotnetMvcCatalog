using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvcCatalog.Migrations
{
    /// <inheritdoc />
    public partial class Update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Products_ProductId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsFromSuppliers_Products_ProductId",
                table: "ProductsFromSuppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsFromSuppliers_Suppliers_SupplierId",
                table: "ProductsFromSuppliers");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Products_ProductId",
                table: "Image",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsFromSuppliers_Products_ProductId",
                table: "ProductsFromSuppliers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsFromSuppliers_Suppliers_SupplierId",
                table: "ProductsFromSuppliers",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Products_ProductId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsFromSuppliers_Products_ProductId",
                table: "ProductsFromSuppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsFromSuppliers_Suppliers_SupplierId",
                table: "ProductsFromSuppliers");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Products_ProductId",
                table: "Image",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsFromSuppliers_Products_ProductId",
                table: "ProductsFromSuppliers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsFromSuppliers_Suppliers_SupplierId",
                table: "ProductsFromSuppliers",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId");
        }
    }
}
