using System;
using Api.HospitalSystem.Dtos.AppointmentDtos;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Mappers;

public static class AppointmentMapper
{
    public static AppointmentResponseDto ToAppointmentResponseDto(this Appointment appointment)
    {
        return new AppointmentResponseDto
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
}
