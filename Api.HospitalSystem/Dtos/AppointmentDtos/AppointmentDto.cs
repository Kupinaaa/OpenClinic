using System;
using Api.HospitalSystem.Dtos.BillDtos;
using Api.HospitalSystem.Dtos.PhysicianDtos;
using Api.HospitalSystem.Dtos.TreatmentDtos;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Dtos.AppointmentDtos;

/*
public class Appointment
{
    public int Id { get; set; } = 0;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DateTimeStart { get; set; } = DateTime.MinValue;
    public DateTime DateTimeEnd { get; set; } = DateTime.MinValue;
    public int PhysicianId { get; set; } = 0;
    public int PatientId { get; set; } = 0;
    public int BillId { get; set; } = 0;

    // Navigation
    public Patient Patient { get; set; } = null!;
    public Physician Physician { get; set; } = null!;
    public List<AppointmentTreatment> AppointmentTreatments { get; set; } = new List<AppointmentTreatment>();
    public Bill? Bill { get; set; } = null;

    // TODO: Add Nurse, Testing Center, Test results

    public override string ToString()
    {
        return $"{Id} {Title} Start: {DateTimeStart} End: {DateTimeEnd} Physician Id: {PhysicianId} Patient Id: {PatientId}";
    }
}
*/

public class AppointmentDto
{
    public int Id { get; set; } = 0;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DateTimeStart { get; set; } = DateTime.MinValue;
    public DateTime DateTimeEnd { get; set; } = DateTime.MinValue;
    public int PhysicianId { get; set; } = 0;
    public int PatientId { get; set; } = 0;
    public int BillId { get; set; } = 0;

    public PatientDto PatientNav { get; set; } = null!;
    public PhysicianDto PhysicianNav { get; set; } = null!;
    public List<AppointmentTreatmentDto> AppointmentTreatmentsNav { get; set; } = new List<AppointmentTreatmentDto>();
    public BillDto? BillNav { get; set; } = null;
}
