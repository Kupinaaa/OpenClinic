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

    public void AddPatient(Patient p) 
    {
        patients.Add(p);
    }

    public void ListAllPatients() 
    {
        patients.ForEach(Console.WriteLine);
    }

    public bool TryFindPatientByID(uint id, out Patient? patient) 
    {
        patient = patients.FirstOrDefault(p => p.Id == id);
        if (patient == null) return false;
        else return true;
    }

    public bool TryFindPatientByQuery(string query, out List<Patient>? foundPatients) 
    {
        foundPatients = patients.Where(p => p.Name.ToUpper().Contains(query.ToUpper()) |
            p.DOB.ToString().Contains(query) || p.AdressLine.ToUpper().Contains(query.ToUpper()) ||
            p.StringifyPatientsGender().ToUpper().Contains(query.ToUpper()) ||
            p.StringifyPatientsRace().ToUpper().Contains(query.ToUpper())).ToList();

        if (foundPatients == null) return false;
        else return true;
    }

    public bool TryDeletePatientById(uint id, out List<Patient>? deletedPatients) 
    {
        deletedPatients = patients.Where(p => p.Id == id).ToList(); // Should not be more than one, since ids are generated, but I'll output a list just in case.
        if (deletedPatients == null) return false;
        else 
        {
            patients.RemoveAll(p => p.Id == id);
            return true;
        }
    }
}
