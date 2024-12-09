using System;
using Api.HospitalSystem.Dtos.TreatmentDtos;
using Api.HospitalSystem.Interfaces.Repositories;
using Api.HospitalSystem.Interfaces.Services;
using Api.HospitalSystem.Mappers;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Services;

public class TreatmentService : ITreatmentService
{
    private readonly ITreatmentRepository _repo;
    public TreatmentService(ITreatmentRepository repo)
    {
        _repo = repo;
    }
    public async Task<TreatmentDto> CreateTreatementOption(TreatmentCreateRequestDto treatmentCreateRequest)
    {
        Treatment treatmentCreate = treatmentCreateRequest.ToTreatment();
        treatmentCreate = await _repo.CreateTreatementOption(treatmentCreate);
        return treatmentCreate.ToTreatmentDto();
    }

    public async Task <TreatmentDto?> GetById(int id)
    {
        Treatment? treatment = await _repo.GetById(id);
        if (treatment == null) return null;
        return treatment.ToTreatmentDto();
    }

    public async Task<TreatmentDto?> DeleteTreatmentOption(int id)
    {
        Treatment? deletedTreatment = await _repo.DeleteTreatmentOption(id);
        return deletedTreatment?.ToTreatmentDto();
    }

    public async Task<List<TreatmentDto>> GetTreatmentOptions()
    {
        List<Treatment> treatments = await _repo.GetTreatmentOptions();
        List<TreatmentDto> treatmentDtos = treatments.Select(t => t.ToTreatmentDto()).ToList();
        return treatmentDtos;
    }

    public async Task<List<Treatment>> GetTreatmentsByIds(List<int> Ids)
    {
        return await _repo.GetByIds(Ids);
    }

    public async Task<TreatmentDto?> UpdateTreatmentOption(int id, TreatmentCreateRequestDto treatmentUpdateRequest)
    {
        Treatment? updatedTreatment = await _repo.UpdateTreatmentOption(id, treatmentUpdateRequest.ToTreatment());
        return updatedTreatment?.ToTreatmentDto();
    }
}
