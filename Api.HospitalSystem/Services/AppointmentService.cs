using System;
using Api.HospitalSystem.Interfaces;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Services;

public class AppointmentService : IAppointmentService
{
    public Task<Appointment> CreateAppointment(Appointment createAppointment)
    {
        throw new NotImplementedException();
    }

    public Task<Appointment?> DeleteAppointment(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Appointment>> GetAllAppointments()
    {
        throw new NotImplementedException();
    }

    public Task<Appointment?> GetAppointmentById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Appointment?> UpdateAppointment(int id, Appointment updateAppointment)
    {
        throw new NotImplementedException();
    }
}
