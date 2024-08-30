using System;
using System.Globalization;
using Library.HospitalSystem.Models;
using Library.HospitalSystem.Services;

namespace Library.HospitalSystem.Helpers;

public class PatientHelper
{
    private PatientService patientService = PatientService.Current;
    public void CreatePatient() // Get next ID from the service
    {
        uint patientId = patientService.NextId; // Get next ID from the service
        string patientName, patientAdressLine;
        DateOnly patientDOB;
        var patientRaces = new List<Race>();
        Gender patientGender = Gender.NotSpecified;

        Console.WriteLine("Input the patients name:");
        patientName = Console.ReadLine() ?? "John Doe";

        Console.WriteLine("Where does your patient live (Plese fit the whole adress into one line. It is your job to correctly format this string.):");
        patientAdressLine = Console.ReadLine() ?? "";

        Console.WriteLine("Input your patients DOB:");
        while (!DateOnly.TryParse(Console.ReadLine(), new CultureInfo("en-US"), out patientDOB)) 
        {
            Console.WriteLine("Please try again. Input a correctly formated string:");
        }

        Console.WriteLine("How many races does your patient have?");
        int numberOfRaces = 0;
        while(!int.TryParse(Console.ReadLine(), out numberOfRaces) && numberOfRaces < 5) 
        {
            Console.WriteLine("Please enter a valid number:");
        }
        while(numberOfRaces-- > 0) 
        {
            Race addRace = Race.NotSpecified;
            string choice = string.Empty;
            while (choice.Length != 1) 
            {
                Console.WriteLine("Please enter the race(s):");
                Console.WriteLine("(W)hite, (B)lack, (A)merican Indian or Alaskan Native, A(S)ian, Native (H)awai or Other Pacific Islander, (O)ther");
                choice = Console.ReadLine() ?? "O";
                if (choice.Length == 1)
                {
                    switch (choice) 
                    {
                        case "w":
                        case "W":
                            addRace = Race.White;
                            break;
                        case "B":
                        case "b":
                            addRace = Race.Black;
                            break;
                        case "A":
                        case "a":
                            addRace = Race.AmericanIndianorAlaskaNative;
                            break;
                        case "S":
                        case "s":
                            addRace = Race.Asian;
                            break;
                        case "H":
                        case "h":
                            addRace = Race.NativeHawaiianorOtherPacificIslander;
                            break;
                        case "O":
                        case "o":
                            addRace = Race.Other;
                            break;
                        default:
                            Console.WriteLine("Please use a specifide character for the race");
                            break;
                    }

                    patientRaces.Add(addRace);
                } else {
                    Console.WriteLine("Please use one character");
                }
            }
        }

        string choiceGender = string.Empty;
        while (choiceGender.Length != 1) 
        {
            Console.WriteLine("Please enter the patients gender (M/F/O):");
            choiceGender = Console.ReadLine() ?? "O";
            if (choiceGender.Length == 1)
            {
                switch (choiceGender) 
                {
                    case "M":
                    case "m":
                        patientGender = Gender.Male;
                        break;
                    case "F":
                    case "f":
                        patientGender = Gender.Female;
                        break;
                    case "O":
                        patientGender = Gender.Other;
                        break;
                    default:
                        patientGender = Gender.NotSpecified;
                        break;
                }
            } else {
                Console.WriteLine("Please use one character");
            }
        }
        var patient = new Patient{
            Id = patientId,
            Name = patientName,
            AdressLine = patientAdressLine,
            DOB = patientDOB,
            Race = patientRaces,
            Gender = patientGender
        };
        patientService.Add(patient);
        patientService.ListAll();
    }

    public void ListAll() 
    {
        Console.WriteLine("List of all patients:");
        patientService.ListAll();
    }

    public void DisplayPatientById() 
    {
        uint id;
        do {
            Console.WriteLine("Enter the id of your patient:");
        } while (!uint.TryParse(Console.ReadLine(), out id));

        if (patientService.TryFindPatientByID(id, out Patient? patient)) 
        {
            Console.WriteLine(patient);
        } else {
            Console.WriteLine("Patient not found!");
        }
    }
}
