using Api.HospitalSystem.Dtos.PhysicianDtos;
using Api.HospitalSystem.Interfaces;
using Api.HospitalSystem.Mappers;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Services;

public class PhysicianService: IPhysicianService
{
    private readonly IPhysicianRepository _physicianRepository;
    public PhysicianService(IPhysicianRepository physicianRepository)
    {
        _physicianRepository = physicianRepository;
    }

    public async Task<PhysicianDto> CreateAsync(PhysicianCreateRequestDto createPhysicianDto)
    {
        Physician createPhysician = createPhysicianDto.ToPhysician();
        createPhysician = await _physicianRepository.CreateAsync(createPhysician);
        PhysicianDto createdPhysicianDto = createPhysician.ToPhysicianDto();

        return createdPhysicianDto;
    }

    public async Task<PhysicianDto?> DeleteAsync(int id)
    {
        Physician? deletedPhysician = await _physicianRepository.DeleteAsync(id);
        if (deletedPhysician == null) return null;
        PhysicianDto deletedPhysicianDto = deletedPhysician.ToPhysicianDto();
        
        return deletedPhysicianDto;
    }

    public async Task<List<PhysicianDto>> GetAllAsync()
    {
        List<Physician> physicians = await _physicianRepository.GetAllAsync();
        List<PhysicianDto> physicianDtos = physicians.Select(p => p.ToPhysicianDto()).ToList();

        return physicianDtos;
    }

    public async Task<PhysicianDto?> GetByIdAsync(int id)
    {
        Physician? physician = await _physicianRepository.GetByIdAsync(id);
        if (physician == null) return null;
        PhysicianDto physicianDto = physician.ToPhysicianDto();

        return physicianDto;
    }

    public async Task<PhysicianDto?> UpdateAsync(int id, PhysicianUpdateRequestDto updatePhysicianDto)
    {
        Physician? updatePhysician = updatePhysicianDto.ToPhysician();
        updatePhysician = await _physicianRepository.UpdateAsync(id, updatePhysician);
        if (updatePhysician == null) return null;
        PhysicianDto updatedPhysicianDto = updatePhysician.ToPhysicianDto();

        return updatedPhysicianDto;
    }
}
