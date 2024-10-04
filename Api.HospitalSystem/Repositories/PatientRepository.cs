using System;
using Api.HospitalSystem.Data;
using Api.HospitalSystem.Dtos.PatientDtos;
using Api.HospitalSystem.Interfaces;
using Api.HospitalSystem.Mappers;
using Api.HospitalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.HospitalSystem.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;
    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Patient> CreateAsync(Patient createPatient)
    {
        await _context.Patients.AddAsync(createPatient);
        await _context.SaveChangesAsync();

        return createPatient;
    }

    public async Task<Patient?> DeleteAsync(int id)
    {
        var patientModel = await GetByIdAsync(id);
        if (patientModel == null) return null;

        _context.Patients.Remove(patientModel);
        await _context.SaveChangesAsync();

        return patientModel;
    }

    public async Task<List<Patient>> GetAllAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient?> GetByIdAsync(int id)
    {
        return await _context.Patients.FirstOrDefaultAsync(p => id == p.Id);
    }

    public async Task<Patient?> UpdateAsync(int id, UpdatePatientRequestDto updatePatient)
    {
        var patientModel = await GetByIdAsync(id);
        if (patientModel == null) return null;

        patientModel.AddressLine = updatePatient.AddressLine;
        patientModel.DOB = updatePatient.DOB;
        patientModel.Gender = updatePatient.Gender;
        patientModel.Name = updatePatient.Name;
        patientModel.Race = updatePatient.Race;

        await _context.SaveChangesAsync();
        return patientModel;
    }
}
