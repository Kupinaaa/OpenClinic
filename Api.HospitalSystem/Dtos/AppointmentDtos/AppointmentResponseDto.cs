using System;

namespace Api.HospitalSystem.Dtos.AppointmentDtos;

public class AppointmentResponseDto
{
    public int Id { get; set; } = 0;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DateTimeStart { get; set; } = DateTime.MinValue;
    public DateTime DateTimeEnd { get; set; } = DateTime.MinValue;
    public int PhysicianId { get; set; } = 0;
    public int PatientId { get; set; } = 0;
}
