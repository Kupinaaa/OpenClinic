using System;
using System.Security.Cryptography;
using Api.HospitalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.HospitalSystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Appointment>()
            .HasOne(e => e.Physician)
            .WithMany(e => e.Appointments)
            .HasForeignKey(e => e.PhysicianId)
            .IsRequired();
        
        modelBuilder.Entity<Appointment>()
            .HasOne(e => e.Patient)
            .WithMany(e => e.Appointments)
            .HasForeignKey(e => e.PatientId)
            .IsRequired();

        modelBuilder.Entity<Bill>()
            .HasOne(e => e.AppointmentNav)
            .WithOne(e => e.Bill)
            .HasForeignKey<Bill>(e => e.AppointmentId)
            .IsRequired(false);

        modelBuilder.Entity<Appointment>()
            .HasMany(e => e.AppointmentTreatments)
            .WithOne(e => e.Appointment)
            .HasForeignKey(e => e.AppointmentId)
            .IsRequired();
        
        modelBuilder.Entity<AppointmentTreatment>()
            .HasOne(e => e.Treatment)
            .WithMany()
            .HasForeignKey(e => e.TreatmentId)
            .IsRequired();
        
        modelBuilder.Entity<Patient>()
            .HasOne(e => e.InsurancePlan)
            .WithMany()
            .HasForeignKey(e => e.InsurancePlanId)
            .IsRequired(false);
        
        modelBuilder.Entity<Patient>()
            .HasMany(e => e.Appointments)
            .WithOne(e => e.Patient)
            .HasForeignKey(e => e.PatientId)
            .IsRequired();

        modelBuilder.Entity<Physician>()
            .HasMany(e => e.Appointments)
            .WithOne(e => e.Physician)
            .HasForeignKey(e => e.PhysicianId)
            .IsRequired();
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Physician> Physicians { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<AppointmentTreatment> AppointmentTreatments { get; set; }
    public DbSet<InsurancePlan> InsurancePlans { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Treatment> Treatments { get; set; }
}
