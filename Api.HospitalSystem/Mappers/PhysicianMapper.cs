using Api.HospitalSystem.Dtos.PhysicianDtos;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Mappers;

public static class PhysicianMapper
{
    public static PhysicianDto ToPhysicianDto(this Physician physician)
    {
        return new PhysicianDto
        {
            Id = physician.Id,
            Name = physician.Name,
            GraduationDate = physician.GraduationDate,
            LisenceNumber = physician.LisenceNumber,
            Specializations = physician.Specializations
        };
    }

    public static Physician ToPhysician(this PhysicianUpdateRequestDto physicianDto)
    {
        return new Physician
        {
            Name = physicianDto.Name,
            GraduationDate = physicianDto.GraduationDate,
            LisenceNumber = physicianDto.LisenceNumber,
            Specializations = physicianDto.Specializations
        };
    }

    public static Physician ToPhysician(this PhysicianCreateRequestDto physicianDto)
    {
        return new Physician
        {
            Name = physicianDto.Name,
            GraduationDate = physicianDto.GraduationDate,
            LisenceNumber = physicianDto.LisenceNumber,
            Specializations = physicianDto.Specializations
        };
    }
}
