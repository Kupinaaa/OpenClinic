using System;
using Api.HospitalSystem.Dtos;
using Api.HospitalSystem.Dtos.PatientDtos;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Interfaces;

public interface IPatientService
{
    Task<List<PatientDto>> GetAllPatients();
    Task<PatientDto?> GetPatientById(int id);
    Task<PatientDto> CreatePatient(PatientCreateRequestDto patientCreateDto);
    Task<PatientDto?> UpdatePatient(int id, PatientUpdateRequestDto patientUpdateDto);
    Task<PatientDto?> DeletePatient(int id);
    Task<List<DateTime>> TimeSlots(int id, DateTime date);
}
