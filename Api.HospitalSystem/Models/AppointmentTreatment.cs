using System;

namespace Api.HospitalSystem.Models;

public class AppointmentTreatment
{
    public int Id { get; set; } = 0;
    public int AppointmentId { get; set; } = 0;
    public int TreatmentId { get; set; } = 0;
    public Treatment Treatment { get; set; } = null!;
    public double OutOfPocket { get; set; } = 0;
}