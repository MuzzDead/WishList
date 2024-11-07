using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WishList.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishes_Users_UserId",
                table: "Wishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishes_Users_UserId1",
                table: "Wishes");

            migrationBuilder.DropIndex(
                name: "IX_Wishes_UserId",
                table: "Wishes");

            migrationBuilder.DropIndex(
                name: "IX_Wishes_UserId1",
                table: "Wishes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Wishes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Wishes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wishes_UserId",
                table: "Wishes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishes_UserId1",
                table: "Wishes",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishes_Users_UserId",
                table: "Wishes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishes_Users_UserId1",
                table: "Wishes",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
