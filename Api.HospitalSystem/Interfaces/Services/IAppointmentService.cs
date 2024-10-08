using System;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Interfaces;

public interface IAppointmentService
{
    Task<List<Appointment>> GetAllAppointments();
    Task<Appointment?> GetAppointmentById(int id);
    Task<Appointment> CreateAppointment(Appointment createAppointment);
    Task<Appointment?> UpdateAppointment(int id, Appointment updateAppointment);
    Task<Appointment?> DeleteAppointment(int id);
}
