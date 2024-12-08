using System;
using Api.HospitalSystem.Dtos.AppointmentDtos;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Mappers;

public static class AppointmentMapper
{
    public static AppointmentDto ToAppointmentDto(this Appointment appointment)
    {
        return new AppointmentDto
        {
            Id = appointment.Id,
            Title = appointment.Title,
            DateTimeStart = appointment.DateTimeStart,
            DateTimeEnd = appointment.DateTimeEnd,
            Description = appointment.Description,
            PatientId = appointment.PatientId,
            PhysicianId = appointment.PhysicianId
        };
    }

    public static AppointmentWithNavDto ToAppointmentWithNavDto(this Appointment appointment)
    {
        return new AppointmentWithNavDto
        {
            Id = appointment.Id,
            Title = appointment.Title,
            DateTimeStart = appointment.DateTimeStart,
            DateTimeEnd = appointment.DateTimeEnd,
            Description = appointment.Description,
            PatientId = appointment.PatientId,
            PhysicianId = appointment.PhysicianId,
            PhysicianNav = appointment.Physician.ToPhysicianDto(),
            PatientNav = appointment.Patient.ToPatientDto(),
            AppointmentTreatmentNav = appointment.AppointmentTreatments.Select(t => t.ToAppointmentTreatmentDto()).ToList()
        };
    }

    public static Appointment ToAppointment(this AppointmentCreateRequestDto appointmentDto)
    {
        return new Appointment
        {
            Title = appointmentDto.Title,
            DateTimeStart = appointmentDto.DateTimeStart,
            DateTimeEnd = appointmentDto.DateTimeEnd,
            Description = appointmentDto.Description,
            PatientId = appointmentDto.PatientId,
            PhysicianId = appointmentDto.PhysicianId
        };
    }

    public static Appointment ToAppointment(this AppointmentUpdateRequestDto appointmentDto)
    {
        return new Appointment
        {
            Title = appointmentDto.Title,
            DateTimeStart = appointmentDto.DateTimeStart,
            DateTimeEnd = appointmentDto.DateTimeEnd,
            Description = appointmentDto.Description,
            PatientId = appointmentDto.PatientId,
            PhysicianId = appointmentDto.PhysicianId
        };
    }
}
