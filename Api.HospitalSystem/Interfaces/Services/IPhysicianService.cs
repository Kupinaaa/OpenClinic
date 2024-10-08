using System;
using Api.HospitalSystem.Dtos.PhysicianDtos;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Interfaces;

public interface IPhysicianService
{
    Task<List<Physician>> GetAllAsync();
    Task<Physician?> GetByIdAsync(int id);
    Task<Physician> CreateAsync(Physician createPhysician);
    Task<Physician?> UpdateAsync(int id, Physician updatePhysician);
    Task<Physician?> DeleteAsync(int id);
}
