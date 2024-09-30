using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.HospitalSystem.Migrations
{
    /// <inheritdoc />
    public partial class enumListBruh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Race",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Race",
                table: "Patients");
        }
    }
}
