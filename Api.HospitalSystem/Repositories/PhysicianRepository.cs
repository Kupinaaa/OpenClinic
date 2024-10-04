using System;
using Api.HospitalSystem.Data;
using Api.HospitalSystem.Dtos.PhysicianDtos;
using Api.HospitalSystem.Interfaces;
using Api.HospitalSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Api.HospitalSystem.Repositories;

public class PhysicianRepository : IPhysicianRepository
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

    public async Task<Physician?> UpdateAsync(int id, UpdatePhysicianRequestDto updatePhysicianDto)
    {
        var updatePhysician = await _context.Physicians.FirstOrDefaultAsync(p => p.Id == id);
        if (updatePhysician == null) return null;

        updatePhysician.Name = updatePhysicianDto.Name;
        updatePhysician.GraduationDate = updatePhysicianDto.GraduationDate;
        updatePhysician.LisenceNumber = updatePhysicianDto.LisenceNumber;
        updatePhysician.Specializations = updatePhysicianDto.Specializations;

        await _context.SaveChangesAsync();
        
        return updatePhysician;
    }
}   