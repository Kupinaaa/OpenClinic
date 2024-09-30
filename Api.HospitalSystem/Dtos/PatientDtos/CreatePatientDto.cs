using System;
using Api.HospitalSystem.Enums;

namespace Api.HospitalSystem.Dtos.PatientDtos;

public class CreatePatientRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string AddressLine { get; set; } = string.Empty;
    public DateOnly DOB { get; set; } = DateOnly.MinValue;
    public Gender Gender { get; set; } = Gender.NotSpecified;
    public List<Race> Race { get; set; } = new List<Race>{Enums.Race.NotSpecified};
}
