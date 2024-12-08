using System;

namespace Api.HospitalSystem.Dtos.TreatmentDtos;

public class TreatmentCreateRequestDto
{
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; } = 0;
}
