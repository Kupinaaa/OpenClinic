using System;
using Api.HospitalSystem.Data;
using Api.HospitalSystem.Interfaces.Services;
using Api.HospitalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.HospitalSystem.Repositories;

public class InsurancePlanRepository : IInsurancePlanRepository
{
    private readonly ApplicationDbContext _context;
    public InsurancePlanRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<InsurancePlan> CreateInsurancePlan(InsurancePlan insurancePlanCreateRequest)
    {
        await _context.AddAsync(insurancePlanCreateRequest);
        await _context.SaveChangesAsync();

        return insurancePlanCreateRequest;
    }

    public async Task<InsurancePlan?> DeleteInsurancePlan(int id)
    {
        var insurancePlanDelete = await _context.InsurancePlans.FirstOrDefaultAsync(i => i.Id == id);
        if (insurancePlanDelete == null) return null;

        _context.Remove(insurancePlanDelete);
        await _context.SaveChangesAsync();
        return insurancePlanDelete;
    }

    public async Task<List<InsurancePlan>> GetInsurancePlans()
    {
        return await _context.InsurancePlans.ToListAsync();
    }

    public async Task<InsurancePlan?> UpdateInsurancePlan(int id, InsurancePlan insurancePlanUpdateRequest)
    {
        var insurancePlanUpdate = await _context.InsurancePlans.FirstOrDefaultAsync(i => i.Id == id);
        if (insurancePlanUpdate == null) return null;

        insurancePlanUpdate.Name = insurancePlanUpdateRequest.Name;
        insurancePlanUpdate.CoinsurancePercent = insurancePlanUpdateRequest.CoinsurancePercent;
        insurancePlanUpdate.Copay = insurancePlanUpdateRequest.Copay;
        insurancePlanUpdate.Deductable = insurancePlanUpdateRequest.Deductable;
        insurancePlanUpdate.OOPM = insurancePlanUpdateRequest.OOPM;

        await _context.SaveChangesAsync();
        return insurancePlanUpdate;
    }
}
