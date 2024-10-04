using System;

namespace Api.HospitalSystem.Dtos.PhysicianDtos;
public class CreatePhysicianRequestDto
{
    public string Name { get; set; } = string.Empty;
    public int LisenceNumber { get; set; } = 0;
    public DateOnly GraduationDate { get; set; } = DateOnly.MinValue;
    public string Specializations { get; set; } = string.Empty;
}
