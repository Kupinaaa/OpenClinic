using System;

namespace Library.HospitalSystem.Models;

public class Appointment
{
    public uint Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateTimeStart { get; set; }
    public DateTime DateTimeEnd { get; set; }
    public uint PhysicianId { get; set; }
    public uint PatientId { get; set; }
    public Appointment()
    {
        Id = 0;
        Title = string.Empty;
        Description = string.Empty;
        DateTimeStart = DateTime.MinValue;
        DateTimeEnd = DateTime.MinValue;
        PhysicianId = 0;
        PatientId = 0;
    }
    public override string ToString()
    {
        return $"{Id} {Title} Start: {DateTimeStart} End: {DateTimeEnd} Physician Id: {PhysicianId} Patient Id: {PatientId}";
    }
}
