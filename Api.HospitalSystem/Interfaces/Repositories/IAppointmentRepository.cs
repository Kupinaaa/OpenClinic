using System;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Interfaces;

public interface IAppointmentRepository
{
    Task<List<Appointment>> GetAll();
    Task<Appointment?> GetById(int id);
    Task<Appointment> Create(Appointment createAppointment);
    Task<Appointment?> Update(int id, Appointment updateAppointment);
    Task<Appointment?> Delete(int id);
    Task<List<Appointment>> GetByPatientOrPhysicianId(int patientId, int physicianId);
}
