using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedcompositiontable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Composition_Clothes_ClothesId",
                table: "Composition");

            migrationBuilder.DropForeignKey(
                name: "FK_Composition_Foods_FoodId",
                table: "Composition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Composition",
                table: "Composition");

            migrationBuilder.RenameTable(
                name: "Composition",
                newName: "Compositions");

            migrationBuilder.RenameIndex(
                name: "IX_Composition_FoodId",
                table: "Compositions",
                newName: "IX_Compositions_FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_Composition_ClothesId",
                table: "Compositions",
                newName: "IX_Compositions_ClothesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Compositions",
                table: "Compositions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Compositions_Clothes_ClothesId",
                table: "Compositions",
                column: "ClothesId",
                principalTable: "Clothes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Compositions_Foods_FoodId",
                table: "Compositions",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compositions_Clothes_ClothesId",
                table: "Compositions");

            migrationBuilder.DropForeignKey(
                name: "FK_Compositions_Foods_FoodId",
                table: "Compositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Compositions",
                table: "Compositions");

            migrationBuilder.RenameTable(
                name: "Compositions",
                newName: "Composition");

            migrationBuilder.RenameIndex(
                name: "IX_Compositions_FoodId",
                table: "Composition",
                newName: "IX_Composition_FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_Compositions_ClothesId",
                table: "Composition",
                newName: "IX_Composition_ClothesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Composition",
                table: "Composition",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Composition_Clothes_ClothesId",
                table: "Composition",
                column: "ClothesId",
                principalTable: "Clothes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Composition_Foods_FoodId",
                table: "Composition",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id");
        }
    }
}
