using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoPay.Migrations
{
    public partial class update01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastUpdateTime",
                table: "Orders",
                nullable: true,
                defaultValue: "GETDATE()",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreateTime",
                table: "Orders",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastUpdateTime",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "CreateTime",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
