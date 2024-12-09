using System;
using Api.HospitalSystem.Dtos.InsurancePlanDtos;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Interfaces.Services;

public interface IInsurancePlanService
{
    Task<List<InsurancePlanDto>> GetInsurancePlanOptions();
    Task<InsurancePlanDto?> GetById(int id);
    Task<InsurancePlanDto> CreateTreatementOption(InsurancePlanCreateRequestDto InsurancePlanCreateRequest);
    Task<InsurancePlanDto?> DeleteInsurancePlanOption(int id);
    Task<InsurancePlanDto?> UpdateInsurancePlanOption(int id, InsurancePlanCreateRequestDto InsurancePlanUpdateRequest);
}
