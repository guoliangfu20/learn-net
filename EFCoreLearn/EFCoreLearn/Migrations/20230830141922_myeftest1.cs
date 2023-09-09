using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreLearn.Migrations
{
    /// <inheritdoc />
    public partial class myeftest1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "Students",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Students",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");
        }
    }
}
