using Microsoft.EntityFrameworkCore.Migrations;

namespace GC_Capstone5.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RunTime",
                table: "Favorite",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RunTime",
                table: "Favorite",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
