using System;
using System.Security.Cryptography;
using Api.HospitalSystem.Dtos;
using Api.HospitalSystem.Dtos.PatientDtos;
using Api.HospitalSystem.Enums;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Mappers;

public static class PatientMapper
{
    public static PatientDto ToPatientDto(this Patient patient)
    {
        return new PatientDto
        {
            Id = patient.Id,
            Name = patient.Name,
            AddressLine = patient.AddressLine,
            DOB = patient.DOB,
            Gender = patient.Gender,
            Race = new List<Race>(patient.Race)
        };
    }

    public static Patient ToPatient(this CreatePatientRequestDto patientDto)
    {
        return new Patient
        {
            Name = patientDto.Name,
            AddressLine = patientDto.AddressLine,
            DOB = patientDto.DOB,
            Gender = patientDto.Gender,
            Race = new List<Race>(patientDto.Race)
        };
    }

    public static Patient ToPatient(this UpdatePatientRequestDto patientDto)
    {
       return new Patient
        {
            Name = patientDto.Name,
            AddressLine = patientDto.AddressLine,
            DOB = patientDto.DOB,
            Gender = patientDto.Gender,
            Race = new List<Race>(patientDto.Race)
        };;
    }
}
