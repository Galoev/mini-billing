using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Billing.WebApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Component",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity_type = table.Column<int>(type: "integer", nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Component", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    additional_info = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity_type = table.Column<int>(type: "integer", nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    payment_status = table.Column<int>(type: "integer", nullable: false),
                    delivery_status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id);
                    table.ForeignKey(
                        name: "Order_customer_id_fkey",
                        column: x => x.customer_id,
                        principalTable: "Customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoodComponent",
                columns: table => new
                {
                    good_id = table.Column<Guid>(type: "uuid", nullable: false),
                    component_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("GoodComponent_pkey", x => new { x.good_id, x.component_id });
                    table.ForeignKey(
                        name: "GoodComponent_component_id_fkey",
                        column: x => x.component_id,
                        principalTable: "Component",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "GoodComponents_goods_id_fkey",
                        column: x => x.good_id,
                        principalTable: "Goods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderGood",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    good_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("OrderGood_pkey", x => new { x.order_id, x.good_id });
                    table.ForeignKey(
                        name: "OrderGood_good_id_fkey",
                        column: x => x.good_id,
                        principalTable: "Goods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "OrderGood_order_id_fkey",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "Customer_phone_key",
                table: "Customer",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodComponent_component_id",
                table: "GoodComponent",
                column: "component_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_customer_id",
                table: "Order",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderGood_good_id",
                table: "OrderGood",
                column: "good_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodComponent");

            migrationBuilder.DropTable(
                name: "OrderGood");

            migrationBuilder.DropTable(
                name: "Component");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
