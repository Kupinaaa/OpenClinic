using System;
using System.Globalization;
using Library.HospitalSystem.Models;
using Library.HospitalSystem.Services;

namespace Library.HospitalSystem.Helpers;

public class PhysicianHelper
{
    private PhysicianService PhysicianService = PhysicianService.Current;
    public void CreatePhysician(uint? Id = null) // Get next ID from the service
    {
        uint PhysicianId = PhysicianService.NextId; // Get next ID from the service
        string PhysicianName, PhysicianSpecializations;
        DateTime PhysicianDateOfGraduation;

        Console.WriteLine("Input the physician's name:");
        PhysicianName = Console.ReadLine() ?? "John Doe";

        uint PhysicianLisenceNumber;
        do {
            Console.WriteLine("Input your physician's lisence number");
        } while (!uint.TryParse(Console.ReadLine(), out PhysicianLisenceNumber));

        Console.WriteLine("Input your Physicians date of graduation:");
        while (!DateTime.TryParse(Console.ReadLine(), new CultureInfo("en-US"), out PhysicianDateOfGraduation)) 
        {
            Console.WriteLine("Please try again. Input a correctly formated string:");
        }

        Console.WriteLine("Input the physician's specializations as a comma separated list:");
        PhysicianSpecializations = Console.ReadLine() ?? string.Empty;

        if(Id == null) 
        {
            var Physician = new Physician{
                Id = PhysicianId,
                Name = PhysicianName,
                GraduationDate = PhysicianDateOfGraduation, 
                LisenceNumber = PhysicianLisenceNumber,
                Specializations = PhysicianSpecializations
            };
            PhysicianService.AddPhysician(Physician);
            Console.WriteLine(Physician);
        }
        else
        {
            if (PhysicianService.TryFindPhysicianByID(Id ?? 0, out Physician? Physician)) 
            {
                Physician.Id = PhysicianId;
                Physician.Name = PhysicianName;
                Physician.GraduationDate = PhysicianDateOfGraduation;
                Physician.LisenceNumber = PhysicianLisenceNumber;
                Physician.Specializations = PhysicianSpecializations;
            }
            Console.WriteLine(Physician);
        }

    }

    public void ListAllPhysicians() 
    {
        Console.WriteLine("List of all Physicians:");
        PhysicianService.ListAllPhysicians();
    }

    public void DisplayPhysicianById() 
    {
        uint id;
        do {
            Console.WriteLine("Enter the id of your Physician:");
        } while (!uint.TryParse(Console.ReadLine(), out id));

        if (PhysicianService.TryFindPhysicianByID(id, out Physician? Physician)) 
        {
            Console.WriteLine(Physician);
        } 
        else 
        {
            Console.WriteLine("Physician not found!");
        }
    }

    public void SearchPhysicianByQuery() {
        Console.WriteLine("Enter your search query:");
        string query = Console.ReadLine() ?? "";

        if(PhysicianService.TryFindPhysicianByQuery(query, out List<Physician>? Physicians)) 
        {
            Physicians?.ForEach(Console.WriteLine);
        } 
        else 
        {
            Console.WriteLine("Physician not found");
        }
    }

    public void DeletePhysicianById() {
        uint Id;

        do {
            Console.WriteLine("Enter the id of the Physician you would like to delete:");
        } while (!uint.TryParse(Console.ReadLine() ?? "0", out Id));

        if (PhysicianService.TryDeletePhysicianById(Id, out List<Physician>? Physicians)) 
        {
            Console.WriteLine("Sucessfuly deleted Physician:");
            Physicians?.ForEach(Console.WriteLine);
        } 
        else 
        {
            Console.WriteLine("No Physicians were deleted.");
        }
    }

    public void UpdatePhysicianById() {
        uint Id;

        do {
            Console.WriteLine("Enter the id of the Physician you would like to update:");
        } while (!uint.TryParse(Console.ReadLine() ?? "0", out Id));

        if (PhysicianService.TryFindPhysicianByID(Id, out Physician? Physician)) 
        {
            Console.WriteLine("The Physician you are trying to change is:");
            Console.WriteLine(Physician);

            CreatePhysician(Id);
        } 
        else 
        {
            Console.WriteLine("Physician not found.");
        }
    }
}
