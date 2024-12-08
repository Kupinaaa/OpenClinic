using System;

namespace Api.HospitalSystem.Dtos.TreatmentDtos;

public class TreatmentDto
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; } = 0;
}
