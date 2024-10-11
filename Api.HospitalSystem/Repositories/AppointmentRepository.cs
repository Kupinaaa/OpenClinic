using System;
using Api.HospitalSystem.Data;
using Api.HospitalSystem.Interfaces;
using Api.HospitalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.HospitalSystem.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ApplicationDbContext _context;
    public AppointmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Appointment> Create(Appointment createAppointment)
    {
        await _context.Appointments.AddAsync(createAppointment);
        await _context.SaveChangesAsync();

        return createAppointment;
    }

    public async Task<Appointment?> Delete(int id)
    {
        var deleteAppointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        if (deleteAppointment == null) return null;

        _context.Appointments.Remove(deleteAppointment);
        await _context.SaveChangesAsync();

        return deleteAppointment;
    }

    public async Task<List<Appointment>> GetAll()
    {
        return await _context.Appointments.ToListAsync();
    }

    public async Task<Appointment?> GetById(int id)
    {
        return await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Appointment>> GetByPatientOrPhysicianId(int patientId, int physicianId)
    {
        var appointments = _context.Appointments.Where(a => a.PatientId == patientId || a.PhysicianId == physicianId);
        return await appointments.ToListAsync();
    }

    public async Task<Appointment?> Update(int id, Appointment updateBody)
    {
        var updateAppointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        if (updateAppointment == null) return null;

        updateAppointment.Title = updateBody.Title;
        updateAppointment.DateTimeStart = updateBody.DateTimeStart;
        updateAppointment.DateTimeEnd = updateBody.DateTimeEnd;
        updateAppointment.Description = updateBody.Description;
        updateAppointment.PatientId = updateBody.PatientId;
        updateAppointment.PhysicianId = updateBody.PhysicianId;

        await _context.SaveChangesAsync();

        return updateAppointment;
    }
}
