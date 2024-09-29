using System.Security.Cryptography;

namespace Api.HospitalSystem.Models;

public enum Race {
    NotSpecified,
    White,
    Black,
    AmericanIndianorAlaskaNative,
    Asian,
    NativeHawaiianorOtherPacificIslander,
    Other
}

public enum Gender {
    NotSpecified,
    Male,
    Female,
    Other
}
public class Patient
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public string AdressLine { get; set; } = string.Empty;
    public DateOnly DOB { get; set; } = DateOnly.MinValue;
    public Gender Gender { get; set; } = Gender.NotSpecified;
    public List<Race> Race { get; set; } = new List<Race>{Models.Race.NotSpecified};
    public List<Appointment> Appointments { get; set; } = new List<Appointment>();

    // TODO: Add medical history, tests, etc.
}