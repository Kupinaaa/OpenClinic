using System;

namespace Api.HospitalSystem.Dtos.BillDtos;

public class BillDto
{
    public int Id { get; set; } = 0;
    public int AppointmentId { get; set; } = 0;
    public double Amount { get; set; } = 0;
    public double OutOfPocket { get; set; } = 0;
}
