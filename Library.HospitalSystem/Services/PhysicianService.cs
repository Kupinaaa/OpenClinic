using System;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Net.NetworkInformation;
using Library.HospitalSystem.Models;

namespace Library.HospitalSystem.Services;

public class PhysicianService
{
    private List<Physician> Physicians;
    private static PhysicianService? _instance;
    private uint _lastIdCreated; // Doesn't have to be static because it's a singleton
    public static PhysicianService Current 
    {
        get 
        {
            if(_instance == null) _instance = new PhysicianService();
            return _instance;
        }
    }
    private PhysicianService() 
    {
        Physicians = new List<Physician>();
        _lastIdCreated = 0;
    }

    public uint NextId // Not static because this class is a singleton
    {
        get
        {
            return ++_lastIdCreated;
        }   
    }

    public void AddPhysician(Physician p) 
    {
        Physicians.Add(p);
    }

    public void ListAllPhysicians() 
    {
        Physicians.ForEach(Console.WriteLine);
    }

    public bool TryFindPhysicianByID(uint id, [NotNullWhen(true)] out Physician? Physician) 
    {
        Physician = Physicians.FirstOrDefault(p => p.Id == id);
        if (Physician == null) return false;
        else return true;
    }

    public bool TryFindPhysicianByQuery(string query, [NotNullWhen(true)] out List<Physician>? foundPhysicians) 
    {
        foundPhysicians = Physicians.Where(p => p.Id.ToString().Contains(query) || p.Name.Contains(query) || p.LisenceNumber.ToString().Contains(query) || p.GraduationDate.ToString().Contains(query) || string.Join(", ", p.Specializations).Contains(query)).ToList();

        if (foundPhysicians == null) return false;
        else return true;
    }

    public bool TryDeletePhysicianById(uint id, [NotNullWhen(true)] out List<Physician>? deletedPhysicians) 
    {
        deletedPhysicians = Physicians.Where(p => p.Id == id).ToList(); // Should not be more than one, since ids are generated, but I'll output a list just in case.
        if (deletedPhysicians == null) return false;
        else 
        {
            Physicians.RemoveAll(p => p.Id == id);
            return true;
        }
    }
}
