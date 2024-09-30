using System.Security.Cryptography;
using Api.HospitalSystem.Enums;

namespace Api.HospitalSystem.Models;

public class Patient
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public string AddressLine { get; set; } = string.Empty;
    public DateOnly DOB { get; set; } = DateOnly.MinValue;
    public Gender Gender { get; set; } = Gender.NotSpecified;
    public List<Race> Race { get; set; } = new List<Race>{Enums.Race.NotSpecified};
    public List<Appointment> Appointments { get; set; } = new List<Appointment>();

    // TODO: Add medical history, tests, etc.
}