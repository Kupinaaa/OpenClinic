using System;
using System.Runtime.CompilerServices;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Interfaces;

public interface IAppointmentRepository
{
    Task<List<Appointment>> GetAll();
    Task<Appointment?> GetById(int id);
    Task<Appointment> Create(Appointment createAppointment);
    Task<Appointment?> Update(int id, Appointment updateAppointment);
    Task<Appointment?> Delete(int id);
    Task<List<Appointment>> GetByPatientAndPhysicianId(int patientId, int physicianId);
    Task<List<Appointment>> GetByPatientId(int patientId);
    Task<List<Appointment>> GetByPhysicianId(int physicianId);
    Task<List<Appointment>> GetUpcomingByPatientId(int patientId, DateTime now);
    Task<List<Appointment>> GetUpcomingByPhysicianId(int physicianId, DateTime now);
    void SaveChangesAsync();
}
