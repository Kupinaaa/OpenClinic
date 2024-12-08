using System;
using Api.HospitalSystem.Dtos.InsurancePlanDtos;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Mappers;

public static class InsurancePlanMapper
{
    public static InsurancePlanDto ToInsurancePlanDto(this InsurancePlan insurancePlan)
    {
        InsurancePlanDto InsurancePlanDto = new InsurancePlanDto
        {
            Id = insurancePlan.Id,
            Name = insurancePlan.Name,
            CoinsurancePercent = insurancePlan.CoinsurancePercent,
            Copay = insurancePlan.Copay,
            Deductable = insurancePlan.Deductable,
            OOPM = insurancePlan.OOPM
        };

        return InsurancePlanDto;
    }
    public static InsurancePlan ToInsurancePlan(this InsurancePlanDto insurancePlanDto)
    {
        InsurancePlan InsurancePlan = new InsurancePlan
        {
            Id = insurancePlanDto.Id,
            Name = insurancePlanDto.Name,
            CoinsurancePercent = insurancePlanDto.CoinsurancePercent,
            Copay = insurancePlanDto.Copay,
            Deductable = insurancePlanDto.Deductable,
            OOPM = insurancePlanDto.OOPM
        };

        return InsurancePlan;
    } 

    public static InsurancePlan ToInsurancePlan(this InsurancePlanCreateRequestDto insurancePlanDto)
    {
        InsurancePlan InsurancePlan = new InsurancePlan
        {
            Name = insurancePlanDto.Name,
            CoinsurancePercent = insurancePlanDto.CoinsurancePercent,
            Copay = insurancePlanDto.Copay,
            Deductable = insurancePlanDto.Deductable,
            OOPM = insurancePlanDto.OOPM
        };

        return InsurancePlan;
    } 
}
