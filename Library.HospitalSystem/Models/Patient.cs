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
    public int id { get; set; }
    public string name { get; set; }
    public string adressLine1 { get; set; }
    public string adressLine2 { get; set; }
    public DateOnly DOB { get; set; }
    public List<Race> Race { get; set; }
    public Gender Gender { get; set; }

    Patient() {
        id = -1;
        name = string.Empty;
        adressLine1 = string.Empty;
        adressLine2 = string.Empty;
        DOB = DateOnly.FromDateTime(DateTime.UnixEpoch);
        Race = new List<Race> { HospitalSystem.Models.Race.NotSpecified };
        Gender = Gender.NotSpecified;
    }
}
