using System;

namespace Api.HospitalSystem.Models;

public class Bill
{
    public int Id { get; set; } = 0;
    public int AppointmentId { get; set; } = 0;
    public double Amount { get; set; } = 0;
    public double OutOfPocket { get; set; } = 0;
    public Appointment AppointmentNav { get; set; } = null!;
}
