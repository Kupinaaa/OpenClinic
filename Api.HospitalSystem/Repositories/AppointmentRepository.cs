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
    public async Task<Appointment> CreateAsync(Appointment createAppointment)
    {
        await _context.Appointments.AddAsync(createAppointment);
        await _context.SaveChangesAsync();

        return createAppointment;
    }

    public async Task<Appointment?> DeleteAsync(int id)
    {
        var deleteAppointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        if (deleteAppointment == null) return null;

        _context.Appointments.Remove(deleteAppointment);
        return deleteAppointment;
    }

    public async Task<List<Appointment>> GetAllAsync()
    {
        return await _context.Appointments.ToListAsync();
    }

    public async Task<Appointment?> GetByIdAsync(int id)
    {
        return await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Appointment?> UpdateAsync(int id)
    {
        throw new NotImplementedException();
    }
}
