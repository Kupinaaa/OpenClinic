using System;
using System.Security.Cryptography;
using Api.HospitalSystem.Dtos;
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
            AdressLine = patient.AdressLine,
            DOB = patient.DOB,
            Gender = patient.Gender,
            Race = new List<Race>(patient.Race)
        };
    }
}
