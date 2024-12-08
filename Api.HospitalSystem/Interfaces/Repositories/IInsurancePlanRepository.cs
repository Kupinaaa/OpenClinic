using System;
using Api.HospitalSystem.Dtos.InsurancePlanDtos;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Interfaces.Services;

public interface IInsurancePlanRepository
{
    Task<List<InsurancePlan>> GetInsurancePlans();
    Task<InsurancePlan> CreateInsurancePlan(InsurancePlan insurancePlanCreateRequest);
    Task<InsurancePlan?> DeleteInsurancePlan(int id);
    Task<InsurancePlan?> UpdateInsurancePlan(int id, InsurancePlan insurancePlanUpdateRequest);
}
