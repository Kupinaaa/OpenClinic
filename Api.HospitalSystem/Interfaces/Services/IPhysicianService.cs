using System;
using Api.HospitalSystem.Dtos.PhysicianDtos;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Interfaces;

public interface IPhysicianService
{
    Task<List<PhysicianDto>> GetAllAsync();
    Task<PhysicianDto?> GetByIdAsync(int id);
    Task<PhysicianDto> CreateAsync(PhysicianCreateRequestDto createPhysician);
    Task<PhysicianDto?> UpdateAsync(int id, PhysicianUpdateRequestDto updatePhysicianDto);
    Task<PhysicianDto?> DeleteAsync(int id);
}
