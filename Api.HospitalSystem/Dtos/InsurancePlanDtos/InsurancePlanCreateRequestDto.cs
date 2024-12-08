using System;

namespace Api.HospitalSystem.Dtos.InsurancePlanDtos;

public class InsurancePlanCreateRequestDto
{
    public string Name { get; set; } = string.Empty;
    public double Deductable { get; set; } = 0;
    public double Copay { get; set; } = 0;
    public double CoinsurancePercent { get; set; } = 0;
    public double OOPM { get; set; } = 0;
}
