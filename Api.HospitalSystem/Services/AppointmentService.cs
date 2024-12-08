using System;
using System.ComponentModel.DataAnnotations;
using Api.HospitalSystem.Data;
using Api.HospitalSystem.Dtos;
using Api.HospitalSystem.Dtos.AppointmentDtos;
using Api.HospitalSystem.Dtos.PhysicianDtos;
using Api.HospitalSystem.Interfaces;
using Api.HospitalSystem.Interfaces.Services;
using Api.HospitalSystem.Mappers;
using Api.HospitalSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Api.HospitalSystem.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientService _patientService;
    private readonly IPhysicianService _physicianService;
    private readonly ITreatmentService _treatmentService;
    private readonly TimeSpan START_TIME = new TimeSpan(8, 0, 0);
    private readonly TimeSpan END_TIME = new TimeSpan(17, 0, 0);
    public AppointmentService(IAppointmentRepository appointmentRepository, IPatientService patientService, IPhysicianService physicianService, ITreatmentService treatmentService)
    {
        _appointmentRepository = appointmentRepository;
        _patientService = patientService;
        _physicianService = physicianService;
        _treatmentService = treatmentService;
    }

    public async Task<bool> CheckAppointmentTime(Appointment appointment, int? updateAppiontmentId = null)
    {
        PatientDto? patient = await _patientService.GetPatientById(appointment.PatientId);
        PhysicianDto? physician = await _physicianService.GetByIdAsync(appointment.PhysicianId);

        if(patient == null || physician == null) return false; // Patient or Physician do not exist

        if (appointment.DateTimeStart.Date != appointment.DateTimeEnd.Date) return false; // Not on the same day
        if (appointment.DateTimeStart.DayOfWeek > DayOfWeek.Friday) return false; // Not M through F
        if (appointment.DateTimeStart >= appointment.DateTimeEnd) return false; // Start is after End
        if (appointment.DateTimeStart.TimeOfDay < START_TIME || appointment.DateTimeEnd.TimeOfDay > END_TIME) return false; // Not in working hours, make variables


        List<Appointment> patientPhysicianAppointments = await _appointmentRepository.GetByPatientAndPhysicianId(appointment.PatientId, appointment.PhysicianId);

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

        List<Treatment> treatments = await _treatmentService.GetTreatmentsByIds(createAppointmentDto.TreatmentOptionIds);
        createAppointment.AppointmentTreatments = treatments.Select(t => new AppointmentTreatment{Treatment = t}).ToList();

        Patient? patient = await _patientService.GetPatientDataById(createAppointmentDto.PatientId);
        if (patient == null) return null;

        createAppointment.Bill = new Bill
        {
            Amount = createAppointment.AppointmentTreatments.Select(t => t.Treatment.Price).Sum(),
        };

        InsurancePlan? insurance = createAppointment.Patient.InsurancePlan;
        if (insurance == null) createAppointment.Bill.OutOfPocket = createAppointment.Bill.Amount;
        else 
        {
            double leftToCover = createAppointment.Bill.Amount;
            double outOfPocket = 0;

            double deductableLeft = Math.Max(0, insurance.Deductable - patient.TotalPayThisYear);
            double deductableCover = Math.Min(deductableLeft, leftToCover);
            leftToCover -= deductableCover;
            outOfPocket += deductableCover;

            if (leftToCover > 0)
            {
                double coPay = Math.Min(leftToCover, insurance.Copay);
                leftToCover -= coPay;
                outOfPocket += coPay;
            }

            if (leftToCover > 0)
            {
                double coPay = Math.Min(insurance.OOPM, leftToCover * insurance.CoinsurancePercent);
                leftToCover = 0;
                outOfPocket += coPay;
            } 
            createAppointment.Bill.OutOfPocket = outOfPocket;
        }


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

    public async Task<List<AppointmentWithNavDto>> GetAllAppointments()
    {
        List<Appointment> appointments = await _appointmentRepository.GetAll();
        List<AppointmentWithNavDto> appointmentDtos = appointments.Select(a => a.ToAppointmentWithNavDto()).ToList();
        return appointmentDtos;
    }

    public async Task<AppointmentWithNavDto?> GetAppointmentById(int id)
    {
        Appointment? appointment = await _appointmentRepository.GetById(id);
        if (appointment == null) return null;

        AppointmentWithNavDto appointmentDto = appointment.ToAppointmentWithNavDto();
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
        List<Appointment> physicianAppointments = await _appointmentRepository.GetByPhysicianId(physicianId);
        List<AppointmentDto> physicianAppointmentDtos = physicianAppointments.Select(a => a.ToAppointmentDto()).ToList();
        return physicianAppointmentDtos;
    }

    public async Task<List<AppointmentDto>> GetPatientAppointments(int patientId)
    {
        List<Appointment> patientAppointments = await _appointmentRepository.GetByPatientId(patientId);
        List<AppointmentDto> patientAppointmentDtos = patientAppointments.Select(a => a.ToAppointmentDto()).ToList();
        return patientAppointmentDtos;
    }

    public async Task<List<AppointmentDto>> GetUpcomingPhysicianAppointments(int physicianId, DateTime now)
    {
        List<Appointment> physicianAppointments = await _appointmentRepository.GetUpcomingByPhysicianId(physicianId, now);
        List<AppointmentDto> physicanAppointmentDtos = physicianAppointments.Select(a => a.ToAppointmentDto()).ToList();
        return physicanAppointmentDtos;
    }
    public async Task<List<AppointmentDto>> GetUpcomingPatientAppointments(int patientId, DateTime now)
    {
        List<Appointment> patientAppointments = await _appointmentRepository.GetUpcomingByPatientId(patientId, now);
        List<AppointmentDto> patientAppointmentDtos = patientAppointments.Select(a => a.ToAppointmentDto()).ToList();
        return patientAppointmentDtos;
    } 

    public async Task<List<DateTime>> GetPhysicianAvailability(int physicianId, DateTime day, int? updateId)
    {
        List<Appointment> physicianAppointments = await _appointmentRepository.GetUpcomingByPhysicianId(physicianId, day);
        List<TimeSpan> physicianAvailabilityTimeSpans = new List<TimeSpan>();
        List<DateTime> physicianAvailability = new List<DateTime>();

        TimeSpan iteratorTime = START_TIME;
        TimeSpan stepTime = new TimeSpan(0, 30, 0);

        for (; iteratorTime < END_TIME; iteratorTime = iteratorTime.Add(stepTime))
        {
            physicianAvailabilityTimeSpans.Add(iteratorTime);
        }

        physicianAppointments.ForEach(appointment => {
            if(appointment.Id == updateId) {
                return;
            }
            for (iteratorTime = new TimeSpan(appointment.DateTimeStart.Hour, appointment.DateTimeStart.Minute >= 30 ? 30 : 0, 0); iteratorTime <= appointment.DateTimeEnd.TimeOfDay; iteratorTime = iteratorTime.Add(stepTime))
            {
                physicianAvailabilityTimeSpans.Remove(iteratorTime);
            }
        });

        physicianAvailabilityTimeSpans.ForEach(timeSpan => {
            day = day.Date + timeSpan;
            physicianAvailability.Add(day);
        });

        return physicianAvailability;
    }
}