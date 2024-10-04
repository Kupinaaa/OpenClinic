using System;
using Api.HospitalSystem.Dtos.PatientDtos;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Interfaces;

public interface IPatientRepository
{
    Task<List<Patient>> GetAllAsync();
    Task<Patient?> GetByIdAsync(int id);
    Task<Patient> CreateAsync(CreatePatientRequestDto patientDto);
    Task<Patient?> UpdateAsync(int id, UpdatePatientRequestDto patientDto);
    Task<Patient> DeleteAsync(int id);

}
