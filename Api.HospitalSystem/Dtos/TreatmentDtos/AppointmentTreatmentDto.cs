using System;
using Api.HospitalSystem.Dtos.AppointmentDtos;
namespace Api.HospitalSystem.Dtos.TreatmentDtos;

public class AppointmentTreatmentDto
{
    public int Id { get; set; } = 0;
    public int AppointmentId { get; set; } = 0;
    public int TreatmentId { get; set; } = 0;
    public TreatmentDto Treatment { get; set; } = null!;
}
