using System;

namespace Api.HospitalSystem.Dtos.PhysicianDtos;

public class PhysicianUpdateRequestDto
{
    public string Name { get; set; } = string.Empty;
    public int LisenceNumber { get; set; } = 0;
    public DateTime GraduationDate { get; set; } = DateTime.MinValue;
    public string Specializations { get; set; } = string.Empty;
}
