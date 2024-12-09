using System;
using Api.HospitalSystem.Data;
using Api.HospitalSystem.Interfaces.Repositories;
using Api.HospitalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.HospitalSystem.Repositories;

public class TreatmentRepository : ITreatmentRepository
{
    private readonly ApplicationDbContext _context;
    public TreatmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Treatment> CreateTreatementOption(Treatment treatmentCreateRequest)
    {
        await _context.Treatments.AddAsync(treatmentCreateRequest);
        await _context.SaveChangesAsync();
        return treatmentCreateRequest;
    }

    public async Task<Treatment?> GetById(int id)
    {
        return await _context.Treatments.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Treatment?> DeleteTreatmentOption(int id)
    {
        var treatmentToDelete = await _context.Treatments.FirstOrDefaultAsync(t => t.Id == id);
        if (treatmentToDelete == null) return null;

        _context.Treatments.Remove(treatmentToDelete);
        await _context.SaveChangesAsync();
        return treatmentToDelete;
    }

    public async Task<List<Treatment>> GetByIds(List<int> ids)
    {
        return await _context.Treatments.Where(t => ids.Contains(t.Id)).ToListAsync();
    }

    public async Task<List<Treatment>> GetTreatmentOptions()
    {
        return await _context.Treatments.ToListAsync();
    }

    public async Task<Treatment?> UpdateTreatmentOption(int id, Treatment treatmentUpdateRequest)
    {
        var treatmentToUpdate = await _context.Treatments.FirstOrDefaultAsync(t => t.Id == id);
        if (treatmentToUpdate == null) return null;

        treatmentToUpdate.Name = treatmentUpdateRequest.Name;
        treatmentToUpdate.Price = treatmentUpdateRequest.Price;

        await _context.SaveChangesAsync();
        return treatmentToUpdate;
    }
}
