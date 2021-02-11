using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Billing.WebApi.Migrations
{
    public partial class InitialCustomerAndComponentDataInserted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Component",
                columns: new[] { "id", "description", "quantity_type", "unit_price" },
                values: new object[,]
                {
                    { new Guid("246948a1-2810-4e0e-bf9b-945604598652"), "Curd", 1, 800m },
                    { new Guid("13f41001-349a-4d09-b5d7-34ac43addfb5"), "Milk", 2, 50m },
                    { new Guid("ce48ef77-9004-4d13-bd42-10b6a2f0cf30"), "Egg", 3, 6m },
                    { new Guid("e1393102-58f6-4b3c-9bd0-6f0f48f7cbdd"), "Butter", 1, 1000m },
                    { new Guid("0f0e69dd-785f-419a-8006-40c6d28cd26b"), "Flour", 1, 40m },
                    { new Guid("5ce30744-9be8-44f1-ad4b-1ed0d1cef460"), "Yeast", 1, 200m },
                    { new Guid("5d4f7bff-b8f1-43ae-bded-d27fe9b30140"), "Sugar", 1, 35m },
                    { new Guid("fdba288f-1fb5-46ac-8208-7f26e600ffa0"), "Salt", 1, 40m }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "id", "additional_info", "name", "phone" },
                values: new object[,]
                {
                    { new Guid("e5b1ba6e-7c41-4eb0-af7b-1a8d35acc081"), "22 years old guy", "Sergey", "8(800)535-35-35" },
                    { new Guid("02a6eb94-6e00-4f96-87ce-d969a33bb0fe"), "I don't know how many years old guy", "Ilkin", "8(900)737-37-37" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Component",
                keyColumn: "id",
                keyValue: new Guid("0f0e69dd-785f-419a-8006-40c6d28cd26b"));

            migrationBuilder.DeleteData(
                table: "Component",
                keyColumn: "id",
                keyValue: new Guid("13f41001-349a-4d09-b5d7-34ac43addfb5"));

            migrationBuilder.DeleteData(
                table: "Component",
                keyColumn: "id",
                keyValue: new Guid("246948a1-2810-4e0e-bf9b-945604598652"));

            migrationBuilder.DeleteData(
                table: "Component",
                keyColumn: "id",
                keyValue: new Guid("5ce30744-9be8-44f1-ad4b-1ed0d1cef460"));

            migrationBuilder.DeleteData(
                table: "Component",
                keyColumn: "id",
                keyValue: new Guid("5d4f7bff-b8f1-43ae-bded-d27fe9b30140"));

            migrationBuilder.DeleteData(
                table: "Component",
                keyColumn: "id",
                keyValue: new Guid("ce48ef77-9004-4d13-bd42-10b6a2f0cf30"));

            migrationBuilder.DeleteData(
                table: "Component",
                keyColumn: "id",
                keyValue: new Guid("e1393102-58f6-4b3c-9bd0-6f0f48f7cbdd"));

            migrationBuilder.DeleteData(
                table: "Component",
                keyColumn: "id",
                keyValue: new Guid("fdba288f-1fb5-46ac-8208-7f26e600ffa0"));

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "id",
                keyValue: new Guid("02a6eb94-6e00-4f96-87ce-d969a33bb0fe"));

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "id",
                keyValue: new Guid("e5b1ba6e-7c41-4eb0-af7b-1a8d35acc081"));
        }
    }
}
