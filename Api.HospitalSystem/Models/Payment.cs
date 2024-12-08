using System;

namespace Api.HospitalSystem.Models;

public class Payment
{
    public int Id { get; set; } = 0;
    public double Amount { get; set; } = 0;
    public int PatientId { get; set; } = 0;
}
