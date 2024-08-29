namespace Library.HospitalSystem.Models;

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
    public uint Id { get; set; }
    public string Name { get; set; }
    public string AdressLine1 { get; set; }
    public string AdressLine2 { get; set; }
    public DateOnly DOB { get; set; }
    public List<Race> Race { get; set; }
    public Gender Gender { get; set; }

    Patient() {
        Id = 0;
        Name = string.Empty;
        AdressLine1 = string.Empty;
        AdressLine2 = string.Empty;
        DOB = DateOnly.FromDateTime(DateTime.UnixEpoch);
        Race = new List<Race> { HospitalSystem.Models.Race.NotSpecified };
        Gender = Gender.NotSpecified;
    }
}
