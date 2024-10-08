using System;
using Api.HospitalSystem.Dtos.AppointmentDtos;
using Api.HospitalSystem.Interfaces;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Services;

public class AppointmentService : IAppointmentService
{
    public Task<AppointmentDto> CreateAppointment(AppointmentCreateRequestDto createAppointment)
    {
        throw new NotImplementedException();
    }

    public Task<AppointmentDto?> DeleteAppointment(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<AppointmentDto>> GetAllAppointments()
    {
        throw new NotImplementedException();
    }

    public Task<AppointmentDto?> GetAppointmentById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<AppointmentDto?> UpdateAppointment(int id, AppointmentUpdateRequestDto updateAppointment)
    {
        throw new NotImplementedException();
    }
}
