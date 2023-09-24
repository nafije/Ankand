using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ankand.Migrations
{
    /// <inheritdoc />
    public partial class order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Poste_ProduktID",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopinCartItem_Poste_ProduktiID",
                table: "ShopinCartItem");

            migrationBuilder.AlterColumn<string>(
                name: "BiderId",
                table: "Poste",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Order",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "BiderId",
                table: "Oferta",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Oferta_ProduktID",
                table: "OrderItem",
                column: "ProduktID",
                principalTable: "Oferta",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopinCartItem_Oferta_ProduktiID",
                table: "ShopinCartItem",
                column: "ProduktiID",
                principalTable: "Oferta",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Oferta_ProduktID",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopinCartItem_Oferta_ProduktiID",
                table: "ShopinCartItem");

            migrationBuilder.DropIndex(
                name: "IX_Order_UserId",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "BiderId",
                table: "Poste",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Order",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "BiderId",
                table: "Oferta",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Poste_ProduktID",
                table: "OrderItem",
                column: "ProduktID",
                principalTable: "Poste",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopinCartItem_Poste_ProduktiID",
                table: "ShopinCartItem",
                column: "ProduktiID",
                principalTable: "Poste",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
