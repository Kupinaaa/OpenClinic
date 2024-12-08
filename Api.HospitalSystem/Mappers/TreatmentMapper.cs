using System;
using Api.HospitalSystem.Dtos.TreatmentDtos;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Mappers;

public static class TreatmentMapper
{
    public static TreatmentDto ToTreatmentDto(this Treatment treatment)
    {
        TreatmentDto treatmentDto = new TreatmentDto
        {
            Id = treatment.Id,
            Name = treatment.Name,
            Price = treatment.Price,
        };

        return treatmentDto;
    }
    public static Treatment ToTreatment(this TreatmentDto treatmentDto)
    {
        Treatment treatment = new Treatment
        {
            Id = treatmentDto.Id,
            Name = treatmentDto.Name,
            Price = treatmentDto.Price,
        };

        return treatment;
    } 

    public static Treatment ToTreatment(this TreatmentCreateRequestDto treatmentDto)
    {
        Treatment treatment = new Treatment
        {
            Name = treatmentDto.Name,
            Price = treatmentDto.Price,
        };

        return treatment;
    } 

    public static AppointmentTreatmentDto ToAppointmentTreatmentDto(this AppointmentTreatment appointmentTreatment)
    {
        return new AppointmentTreatmentDto 
        {
            Id = appointmentTreatment.Id,
            TreatmentId = appointmentTreatment.TreatmentId,
            AppointmentId = appointmentTreatment.AppointmentId,
            Treatment = appointmentTreatment.Treatment.ToTreatmentDto(),
        };
    }
}
