using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.HospitalSystem.Migrations
{
    /// <inheritdoc />
    public partial class Bill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Patients_PatientId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "OutOfPocket",
                table: "AppointmentTreatments");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "OutOfPocket",
                table: "Payments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AppointmentId",
                table: "Payments",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Appointments_AppointmentId",
                table: "Payments",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Patients_PatientId",
                table: "Payments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Appointments_AppointmentId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Patients_PatientId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_AppointmentId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "OutOfPocket",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OutOfPocket",
                table: "AppointmentTreatments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Patients_PatientId",
                table: "Payments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
