using System;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Interfaces;

public interface IAppointmentRepository
{
    Task<List<Appointment>> GetAllAsync();
    Task<Appointment?> GetByIdAsync(int id);
    Task<Appointment> CreateAsync(Appointment createAppointment);
    Task<Appointment?> UpdateAsync(int id, Appointment updateAppointment);
    Task<Appointment?> DeleteAsync(int id);
}
