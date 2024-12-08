using System;
using Api.HospitalSystem.Dtos.TreatmentDtos;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Interfaces.Services;

public interface ITreatmentService
{
    Task<List<TreatmentDto>> GetTreatmentOptions();
    Task<TreatmentDto> CreateTreatementOption(TreatmentCreateRequestDto treatmentCreateRequest);
    Task<TreatmentDto?> DeleteTreatmentOption(int id);
    Task<TreatmentDto?> UpdateTreatmentOption(int id, TreatmentCreateRequestDto treatmentUpdateRequest);
    Task<List<Treatment>> GetTreatmentsByIds(List<int> Ids);
}
