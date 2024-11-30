using System;
using Api.HospitalSystem.Enums; // Race & Gender enums

namespace Api.HospitalSystem.Dtos;

public class PatientDto
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public string AddressLine { get; set; } = string.Empty;
    public DateTime DOB { get; set; } = DateTime.MinValue;
    public Gender Gender { get; set; } = Gender.NotSpecified;
    public List<Race> Race { get; set; } = new List<Race>{Enums.Race.NotSpecified};
    // public List<AppointmentDto> Appointments { get; set; } = new List<AppointmentDto>();
}
