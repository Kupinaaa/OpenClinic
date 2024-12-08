using System;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Interfaces.Repositories;

public interface ITreatmentRepository
{
    Task<List<Treatment>> GetTreatmentOptions();
    Task<Treatment> CreateTreatementOption(Treatment treatmentCreateRequest);
    Task<Treatment?> DeleteTreatmentOption(int id);
    Task<Treatment?> UpdateTreatmentOption(int id, Treatment treatmentUpdateRequest);
    Task<List<Treatment>> GetByIds(List<int> ids);
}
