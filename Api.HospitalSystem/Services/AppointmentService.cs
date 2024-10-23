using System;
using Api.HospitalSystem.Data;
using Api.HospitalSystem.Dtos;
using Api.HospitalSystem.Dtos.AppointmentDtos;
using Api.HospitalSystem.Dtos.PhysicianDtos;
using Api.HospitalSystem.Interfaces;
using Api.HospitalSystem.Mappers;
using Api.HospitalSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Api.HospitalSystem.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientService _patientService;
    private readonly IPhysicianService _physicianService;
    public AppointmentService(IAppointmentRepository appointmentRepository, IPatientService patientService, IPhysicianService physicianService)
    {
        _appointmentRepository = appointmentRepository;
        _patientService = patientService;
        _physicianService = physicianService;
    }

    public async Task<bool> CheckAppointmentTime(Appointment appointment, int? updateAppiontmentId = null)
    {
        PatientDto? patient = await _patientService.GetPatientById(appointment.PatientId);
        PhysicianDto? physician = await _physicianService.GetByIdAsync(appointment.PhysicianId);

        if(patient == null || physician == null) return false; // Patient or Physician do not exist

        if (appointment.DateTimeStart.Date != appointment.DateTimeEnd.Date) return false; // Not on the same day
        if (appointment.DateTimeStart.DayOfWeek > DayOfWeek.Friday) return false; // Not M through F
        if (appointment.DateTimeStart >= appointment.DateTimeEnd) return false; // Start is after End
        if (appointment.DateTimeStart.TimeOfDay < new TimeSpan(8, 0, 0) || appointment.DateTimeEnd.TimeOfDay > new TimeSpan(17, 0, 0)) return false; // Not in working hours, make variables


        List<Appointment> patientPhysicianAppointments = await _appointmentRepository.GetByPatientOrPhysicianId(appointment.PatientId, appointment.PhysicianId);

        bool appointmentOverlaps = patientPhysicianAppointments.Any(checkAppointment => checkAppointment.Id != updateAppiontmentId && 
        ((checkAppointment.DateTimeStart <= appointment.DateTimeStart && appointment.DateTimeStart <= checkAppointment.DateTimeEnd) ||
        (checkAppointment.DateTimeStart <= appointment.DateTimeEnd && appointment.DateTimeEnd <= checkAppointment.DateTimeEnd) ||
        (appointment.DateTimeStart <= checkAppointment.DateTimeStart && checkAppointment.DateTimeStart <= appointment.DateTimeEnd) ||
        (appointment.DateTimeStart <= checkAppointment.DateTimeEnd && checkAppointment.DateTimeEnd <= appointment.DateTimeEnd)));

        // Appointment Overlaps with existing appointment of physician or patient that are not updateAppointmentId
        if (appointmentOverlaps == true) return false;

        return true;
    }

    public async Task<AppointmentDto?> CreateAppointment(AppointmentCreateRequestDto createAppointmentDto)
    {
        Appointment createAppointment = createAppointmentDto.ToAppointment();

        if(await CheckAppointmentTime(createAppointment) == false) return null;

        Appointment createdAppointment = await _appointmentRepository.Create(createAppointment);
        AppointmentDto createdAppointmentDto = createdAppointment.ToAppointmentDto();

        return createdAppointmentDto;
    }

    public async Task<AppointmentDto?> DeleteAppointment(int id)
    {
        Appointment? appointmentToDelete = await _appointmentRepository.Delete(id);
        if (appointmentToDelete == null) return null;

        AppointmentDto deletedAppointment = appointmentToDelete.ToAppointmentDto();
        return deletedAppointment;
    }

    public async Task<List<AppointmentDto>> GetAllAppointments()
    {
        List<Appointment> appointments = await _appointmentRepository.GetAll();
        List<AppointmentDto> appointmentDtos = appointments.Select(a => a.ToAppointmentDto()).ToList();
        return appointmentDtos;
    }

    public async Task<AppointmentDto?> GetAppointmentById(int id)
    {
        Appointment? appointment = await _appointmentRepository.GetById(id);
        if (appointment == null) return null;

        AppointmentDto appointmentDto = appointment.ToAppointmentDto();
        return appointmentDto;
    }

    public async Task<AppointmentDto?> UpdateAppointment(int id, AppointmentUpdateRequestDto updateAppointmentDto)
    {
        Appointment? appointmentToUpdate = await _appointmentRepository.GetById(id);
        if (appointmentToUpdate == null) return null;

        Appointment? updateAppointment = updateAppointmentDto.ToAppointment();

        if (await CheckAppointmentTime(updateAppointment, id) == false) return null;

        updateAppointment = await _appointmentRepository.Update(id, updateAppointment);
        if (updateAppointment == null) return null;

        AppointmentDto updatedAppointment = updateAppointment.ToAppointmentDto();

        return updatedAppointment;
    }

    public async Task<List<AppointmentDto>> GetPhysicianAppointments(int physicianId)
    {
        List<Appointment> physicianAppointments = await _appointmentRepository.GetByPatientOrPhysicianId(null, physicianId);
        List<AppointmentDto> physicianAppointmentDtos = physicianAppointments.Select(a => a.ToAppointmentDto()).ToList();
        return physicianAppointmentDtos;
    }
}