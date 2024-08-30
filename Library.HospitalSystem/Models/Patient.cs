using System.Security.Cryptography;

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
    public string AdressLine { get; set; }
    public DateOnly DOB { get; set; }
    public List<Race> Race { get; set; }
    public Gender Gender { get; set; }

    public Patient() {
        Id = 0;
        Name = string.Empty;
        AdressLine = string.Empty;
        DOB = DateOnly.MinValue;
        Race = new List<Race> { HospitalSystem.Models.Race.NotSpecified };
        Gender = Gender.NotSpecified;
    }

    public override string ToString()
    {
        string genderString = Gender switch
        {
            Gender.Male => "Male",
            Gender.Female => "Female",
            Gender.Other => "Other",
            _ => "UNSPECIFIED"
        };

        List<string> raceStrings = Race.Select((r) => r switch {
            Models.Race.White => "White",
            Models.Race.AmericanIndianorAlaskaNative => "American Indian or Alaska Native",
            Models.Race.Asian => "Asian",
            Models.Race.Black => "Black",
            Models.Race.NativeHawaiianorOtherPacificIslander => "Native Hawaiia or Other Pacific Islander",
            Models.Race.Other => "Other",
            _ => "UNSPECIFIED"
        }).ToList();

        return $"{Id} {Name} {AdressLine} {DOB} {genderString} {string.Join(", ", raceStrings)}";
    }
}
