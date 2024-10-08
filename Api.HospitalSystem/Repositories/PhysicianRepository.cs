using Api.HospitalSystem.Data;
using Api.HospitalSystem.Interfaces;
using Api.HospitalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.HospitalSystem.Repositories;

public class PhysicianRepository: IPhysicianRepository
{
    private readonly ApplicationDbContext _context;
    public PhysicianRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Physician> CreateAsync(Physician createPhysician)
    {
        await _context.Physicians.AddAsync(createPhysician);
        await _context.SaveChangesAsync();

        return createPhysician;
    }

    public async Task<Physician?> DeleteAsync(int id)
    {
        var physicianToDelete = await _context.Physicians.FirstOrDefaultAsync(p => p.Id == id);
        if (physicianToDelete == null) return null;

        _context.Physicians.Remove(physicianToDelete);
        await _context.SaveChangesAsync();

        return physicianToDelete;
    }

    public async Task<List<Physician>> GetAllAsync()
    {
        return await _context.Physicians.ToListAsync();
    }

    public async Task<Physician?> GetByIdAsync(int id)
    {
        return await _context.Physicians.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Physician?> UpdateAsync(int id, Physician updatePhysician)
    {
        var updatePhysicianDb = await _context.Physicians.FirstOrDefaultAsync(p => p.Id == id);
        if (updatePhysicianDb == null) return null;

        updatePhysicianDb.Name = updatePhysician.Name;
        updatePhysicianDb.GraduationDate = updatePhysician.GraduationDate;
        updatePhysicianDb.LisenceNumber = updatePhysician.LisenceNumber;
        updatePhysicianDb.Specializations = updatePhysician.Specializations;

        await _context.SaveChangesAsync();
        
        return updatePhysician;
    }
}   