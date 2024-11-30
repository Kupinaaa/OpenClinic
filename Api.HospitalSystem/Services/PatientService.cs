using System;
using Api.HospitalSystem.Controllers;
using Api.HospitalSystem.Data;
using Api.HospitalSystem.Dtos;
using Api.HospitalSystem.Dtos.PatientDtos;
using Api.HospitalSystem.Interfaces;
using Api.HospitalSystem.Mappers;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Services;

public class PatientService: IPatientService
{
    private readonly IPatientRepository _patientRepository;
    public PatientService(IPatientRepository repo)
    {
        _patientRepository = repo;
    }
    public async Task<PatientDto> CreatePatient(PatientCreateRequestDto patientCreateDto)
    {
        Patient createPatient = patientCreateDto.ToPatient();
        Patient createdPatient = await _patientRepository.CreateAsync(createPatient);
        PatientDto createdPatientDto = createdPatient.ToPatientDto();

        return createdPatientDto;
    }

    public async Task<PatientDto?> DeletePatient(int id)
    {
        Patient? deletedPatient = await _patientRepository.DeleteAsync(id);
        if (deletedPatient == null) return null;
        PatientDto deletedPatientDto = deletedPatient.ToPatientDto();

        return deletedPatientDto;
    }

    public async Task<List<PatientDto>> GetAllPatients()
    {
        List<Patient> patients = await _patientRepository.GetAllAsync();
        List<PatientDto> patientDtos = patients.Select(p => p.ToPatientDto()).ToList();

        return patientDtos;
    }

    public async Task<PatientDto?> GetPatientById(int id)
    {
        Patient? patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null) return null;
        PatientDto patientDto = patient.ToPatientDto();

        return patientDto;
    }

    public Task<List<DateTime>> TimeSlots(int id, DateTime date)
    {
        throw new NotImplementedException();
    }

    public async Task<PatientDto?> UpdatePatient(int id, PatientUpdateRequestDto patientUpdateDto)
    {
        Patient? updatePatient = patientUpdateDto.ToPatient();

        updatePatient = await _patientRepository.UpdateAsync(id, updatePatient);
        if (updatePatient == null) return null;

        PatientDto updatedPatientDto = updatePatient.ToPatientDto();
        return updatedPatientDto;
    }
}
