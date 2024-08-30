using System;
using System.Dynamic;
using System.Net.NetworkInformation;
using Library.HospitalSystem.Models;

namespace Library.HospitalSystem.Services;

public class PatientService
{
    private List<Patient> patients;
    private static PatientService? _instance;
    private uint _lastIdCreated; // Doesn't have to be static because it's a singleton
    public static PatientService Current 
    {
        get 
        {
            if(_instance == null) _instance = new PatientService();
            return _instance;
        }
    }
    private PatientService() 
    {
        patients = new List<Patient>();
        _lastIdCreated = 0;
    }

    public uint NextId // Not static because this class is a singleton
    {
        get
        {
            return ++_lastIdCreated;
        }   
    }

    public void Add(Patient p) 
    {
        patients.Add(p);
    }

    public void ListAll() 
    {
        patients.ForEach(Console.WriteLine);
    }
}
