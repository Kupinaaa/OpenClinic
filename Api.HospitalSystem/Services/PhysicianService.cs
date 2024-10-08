using System;
using Api.HospitalSystem.Dtos.PhysicianDtos;
using Api.HospitalSystem.Interfaces;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Services;

public class PhysicianService : IPhysicianService
{
    public Task<Physician> CreateAsync(Physician createPhysician)
    {
        throw new NotImplementedException();
    }

    public Task<PhysicianDto> CreateAsync(PhysicianCreateRequestDto createPhysician)
    {
        throw new NotImplementedException();
    }

    public Task<Physician?> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Physician>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Physician?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Physician?> UpdateAsync(int id, Physician updatePhysician)
    {
        throw new NotImplementedException();
    }

    public Task<PhysicianDto?> UpdateAsync(int id, PhysicianUpdateRequestDto updatePhysicianDto)
    {
        throw new NotImplementedException();
    }

    Task<PhysicianDto?> IPhysicianService.DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task<List<PhysicianDto>> IPhysicianService.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    Task<PhysicianDto?> IPhysicianService.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
