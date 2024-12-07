using System;
using Api.HospitalSystem.Dtos.PhysicianDtos;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Dtos.AppointmentDtos;

public class AppointmentWithNavDto
{
    public int Id { get; set; } = 0;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DateTimeStart { get; set; } = DateTime.MinValue;
    public DateTime DateTimeEnd { get; set; } = DateTime.MinValue;
    public int PhysicianId { get; set; } = 0;
    public int PatientId { get; set; } = 0;
    public PhysicianDto? PhysicianNav { get; set; } = null;
    public PatientDto? PatientNav { get; set; } = null;
}
