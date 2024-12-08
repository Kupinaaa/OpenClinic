using System;

namespace Api.HospitalSystem.Models;

public class Treatment
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; } = 0;
}
