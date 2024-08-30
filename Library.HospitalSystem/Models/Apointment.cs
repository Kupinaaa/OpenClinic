using System;

namespace Library.HospitalSystem.Models;

public class Apointment
{
    public uint Id { get; set; }
    public DateTime DateTimeStart { get; set; }
    public DateTime DateTimeEnd { get; set; }
    public uint PhysicianId { get; set; }
    public uint PatientId { get; set; }
}
