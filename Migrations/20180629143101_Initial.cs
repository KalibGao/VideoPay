using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoPay.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderNo = table.Column<string>(maxLength: 128, nullable: true),
                    OrderAmt = table.Column<int>(nullable: false),
                    PayType = table.Column<string>(maxLength: 128, nullable: true),
                    HasPaid = table.Column<bool>(nullable: false),
                    SN = table.Column<string>(maxLength: 128, nullable: true),
                    CreateTime = table.Column<string>(nullable: true),
                    LastUpdateTime = table.Column<string>(nullable: true),
                    ItemName = table.Column<string>(maxLength: 128, nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNo",
                table: "Orders",
                column: "OrderNo",
                unique: true,
                filter: "[OrderNo] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
