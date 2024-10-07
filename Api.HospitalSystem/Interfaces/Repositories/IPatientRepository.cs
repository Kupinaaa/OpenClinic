using System;
using Api.HospitalSystem.Dtos.PatientDtos;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Interfaces;

public interface IPatientRepository
{
    Task<List<Patient>> GetAllAsync();
    Task<Patient?> GetByIdAsync(int id);
    Task<Patient> CreateAsync(Patient createPatient);
    Task<Patient?> UpdateAsync(int id, UpdatePatientRequestDto updatePatient);
    Task<Patient?> DeleteAsync(int id);

}
