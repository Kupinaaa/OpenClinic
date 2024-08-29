using System;
using Library.HospitalSystem.Models;

namespace Library.HospitalSystem.Services;

public class PatientService
{
    private List<Patient> patients;

    private static PatientService? _instance;
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
    }
}
