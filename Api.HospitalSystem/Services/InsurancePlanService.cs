using System;
using Api.HospitalSystem.Dtos.InsurancePlanDtos;
using Api.HospitalSystem.Interfaces.Services;
using Api.HospitalSystem.Mappers;
using Api.HospitalSystem.Models;
using Microsoft.Identity.Client;

namespace Api.HospitalSystem.Services;

public class InsurancePlanService : IInsurancePlanService
{
    readonly private IInsurancePlanRepository _repo;
    public InsurancePlanService(IInsurancePlanRepository repo)
    {
        _repo = repo;
    }
    public async Task<InsurancePlanDto> CreateTreatementOption(InsurancePlanCreateRequestDto insurancePlanCreateRequest)
    {
        InsurancePlan insurancePlanCreate = insurancePlanCreateRequest.ToInsurancePlan();
        insurancePlanCreate = await _repo.CreateInsurancePlan(insurancePlanCreate);
        return insurancePlanCreate.ToInsurancePlanDto();
    }

    public async Task<InsurancePlanDto?> DeleteInsurancePlanOption(int id)
    {
        return (await _repo.DeleteInsurancePlan(id))?.ToInsurancePlanDto();
    }

    public async Task<List<InsurancePlanDto>> GetInsurancePlanOptions()
    {
        return (await _repo.GetInsurancePlans()).Select(i => i.ToInsurancePlanDto()).ToList();
    }

    public async Task<InsurancePlanDto?> UpdateInsurancePlanOption(int id, InsurancePlanCreateRequestDto insurancePlanUpdateRequest)
    {
        InsurancePlan? insurancePlanUpdate = insurancePlanUpdateRequest.ToInsurancePlan();
        insurancePlanUpdate = await _repo.UpdateInsurancePlan(id, insurancePlanUpdate); 
        if (insurancePlanUpdate == null) return null;
        return insurancePlanUpdate.ToInsurancePlanDto();
    }
}
