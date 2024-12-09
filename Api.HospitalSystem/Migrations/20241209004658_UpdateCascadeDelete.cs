using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.HospitalSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Appointments_AppointmentId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Patients_PatientId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_PatientId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Bills");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Appointments_AppointmentId",
                table: "Bills",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Appointments_AppointmentId",
                table: "Bills");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Bills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PatientId",
                table: "Bills",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Appointments_AppointmentId",
                table: "Bills",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Patients_PatientId",
                table: "Bills",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
