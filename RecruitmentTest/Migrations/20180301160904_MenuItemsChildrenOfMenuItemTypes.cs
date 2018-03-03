using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RecruitmentTest.Migrations
{
    public partial class MenuItemsChildrenOfMenuItemTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_MenuItemTypes_MenuItemTypeId",
                table: "MenuItems");

            migrationBuilder.AlterColumn<int>(
                name: "MenuItemTypeId",
                table: "MenuItems",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_MenuItemTypes_MenuItemTypeId",
                table: "MenuItems",
                column: "MenuItemTypeId",
                principalTable: "MenuItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_MenuItemTypes_MenuItemTypeId",
                table: "MenuItems");

            migrationBuilder.AlterColumn<int>(
                name: "MenuItemTypeId",
                table: "MenuItems",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_MenuItemTypes_MenuItemTypeId",
                table: "MenuItems",
                column: "MenuItemTypeId",
                principalTable: "MenuItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}