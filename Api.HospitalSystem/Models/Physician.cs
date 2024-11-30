using System;

namespace Api.HospitalSystem.Models;

public class Physician
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int LisenceNumber { get; set; } = 0;
    public DateTime GraduationDate { get; set; } = DateTime.MinValue;
    public string Specializations { get; set; } = string.Empty;
    public List<Appointment> Appointments { get; set; } = new List<Appointment>();

    public override string ToString()
    {
        return $"{Id} {Name} {LisenceNumber} {GraduationDate} {string.Join(", ", Specializations)}";
    }
}
