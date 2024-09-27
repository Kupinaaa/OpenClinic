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
    public List<Race> Race { get; set; } = new List<Race> { Models.Race.NotSpecified };
    public Gender Gender { get; set; } = Gender.NotSpecified;
    public List<Appointment> Appointments { get; set; } = new List<Appointment>();

    // TODO: Add medical history, tests, etc.

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

    public string StringifyPatientsRace() 
    {
       return string.Join(", ", Race.Select((r) => r switch {
            Models.Race.White => "White",
            Models.Race.AmericanIndianorAlaskaNative => "American Indian or Alaska Native",
            Models.Race.Asian => "Asian",
            Models.Race.Black => "Black",
            Models.Race.NativeHawaiianorOtherPacificIslander => "Native Hawaiia or Other Pacific Islander",
            Models.Race.Other => "Other",
            _ => "UNSPECIFIED"
        }).ToList()); 
    }

    public string StringifyPatientsGender() {
        return Gender switch
        {
            Gender.Male => "Male",
            Gender.Female => "Female",
            Gender.Other => "Other",
            _ => "UNSPECIFIED"
        }; 
    }
}