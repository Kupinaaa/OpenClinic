using System;
using Api.HospitalSystem.Dtos.AppointmentDtos;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Interfaces;

public interface IAppointmentService
{
    Task<List<AppointmentDto>> GetAllAppointments();
    Task<AppointmentDto?> GetAppointmentById(int id);
    Task<AppointmentDto?> CreateAppointment(AppointmentCreateRequestDto createAppointment);
    Task<AppointmentDto?> UpdateAppointment(int id, AppointmentUpdateRequestDto updateAppointment);
    Task<AppointmentDto?> DeleteAppointment(int id);
    Task<bool> CheckAppointmentTime(Appointment appointemnt, int? updateAppointmentId);
}