using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Library.HospitalSystem.Models;

namespace Library.HospitalSystem.Services;

public class AppointmentService
{
    private List<Appointment> appointments;
    private static AppointmentService? _instance; // Singleton pattern
    private AppointmentService()
    {
        appointments = new List<Appointment>();
        _nextId = 0;
    }
    public static AppointmentService Current 
    {
        get
        {
            if (_instance == null) _instance = new AppointmentService();
            return _instance;
        }
    }

    static private uint _nextId;
    static public uint NextId
    {
        get
        {
            return ++_nextId;
        }
    }

    public bool PatientCheckAppointmentAvailability(uint patientId, DateTime start, DateTime end, out string message, uint? updId = null) 
    {
        message = "Ok";
        bool isOnTheSameDate = start.Date == end.Date;
        bool startIsBeforeEnd = start < end;
        bool isInWorkingHours = start.Hour >= 8 && (end.Hour < 17 || (end.Hour == 17 && end.Minute == 0));
        bool isNotConflictingWithOtherPatientAppointmnets = !appointments.Any(a => a.PatientId == patientId && a.Id != updId &&
        ((end >= a.DateTimeStart && end <= a.DateTimeEnd) || // Check whether the end of the new appointment is inside of a different appointment's time range
        (start >= a.DateTimeStart && start <= a.DateTimeEnd) || // Check whether the start of the new appointment is inside of a different appiontment's time range
        (start <= a.DateTimeStart && end >= a.DateTimeEnd))); // Check whether there exist any appointments that would be inside of the new appointment's time range

        Console.WriteLine(isNotConflictingWithOtherPatientAppointmnets);

        if(!isOnTheSameDate) message = "Not on the same date";
        if (!startIsBeforeEnd) message = "The start of the appointment is before it's end";
        if (!isInWorkingHours) message = "The time slot you are trying to schedule is outside of working hours (8am - 5pm)";
        if (!isNotConflictingWithOtherPatientAppointmnets) message = $"Check conflicting appointments of Patient #{patientId}";

        return isOnTheSameDate && startIsBeforeEnd && isInWorkingHours && isNotConflictingWithOtherPatientAppointmnets;
    }

    public bool PhysicianCheckAppointmentAvailability(uint physicianId, DateTime start, DateTime end, out string message, uint? updId = null ) 
    {
        message = "Ok";
        bool isOnTheSameDate = start.Date == end.Date;
        bool startIsBeforeEnd = start < end;
        bool isInWorkingHours = start.Hour >= 8 && (end.Hour < 17 || (end.Hour == 17 && end.Minute == 0));
        bool isNotConflictingWithOtherPhysicianAppointmnets = !appointments.Any(a => a.PhysicianId == physicianId && a.Id != updId &&
        ((end >= a.DateTimeStart && end <= a.DateTimeEnd) || // Check whether the end of the new appointment is inside of a different appointment's time range
        (start >= a.DateTimeStart && start <= a.DateTimeEnd) || // Check whether the start of the new appointment is inside of a different appiontment's time range
        (start <= a.DateTimeStart && end >= a.DateTimeEnd))); // Check whether there exist any appointments that would be inside of the new appointment's time range

        if(!isOnTheSameDate) message = "Not on the same date";
        if (!startIsBeforeEnd) message = "The start of the appointment is before it's end";
        if (!isInWorkingHours) message = "The time slot you are trying to schedule is outside of working hours (8am - 5pm)";
        if (!isNotConflictingWithOtherPhysicianAppointmnets) message = $"Check conflicting appointments of physician #{physicianId}";

        return isOnTheSameDate && startIsBeforeEnd && isInWorkingHours && isNotConflictingWithOtherPhysicianAppointmnets;
    }
    
    public bool TryAddAppointment(Appointment appointment, out string message, uint? updId = null)
    {
        if (appointment.DateTimeStart.Date != appointment.DateTimeStart.Date) 
        {
            message = "The start and end dates of the appointment have to be on the same day";
            return false;
        }

        if (!PatientCheckAppointmentAvailability(appointment.PatientId, appointment.DateTimeStart, appointment.DateTimeEnd, out message, updId))
        {
            return false;
        }

        if(!PhysicianCheckAppointmentAvailability(appointment.PhysicianId, appointment.DateTimeStart, appointment.DateTimeEnd, out message, updId))
        {
            return false;
        }

        if (updId == null) appointments.Add(appointment);
        message = $"Appointment #{appointment.Id} {(updId != null ? "updated" : "created")} succesfully";
        return true;
    }

    public void ListAllAppointments() 
    {
        appointments.ForEach(Console.WriteLine);
    }

    public bool TryFindAppointmentByID(uint Id, out Appointment appointment)
    {
        appointment = appointments.FirstOrDefault(a => a.Id == Id) ?? new Appointment();
        if (appointment != null) return true;
        return false;
    }

    public bool TryFindAppointmentsByPatientID(uint Id, [NotNullWhen(true)] out List<Appointment> appointmentsFound) // Not null when true does not work for some reason
    {
        appointmentsFound = appointments.FindAll(a => a.PatientId == Id).ToList();
        if (appointmentsFound != null) return true;
        return false;
    }

    public bool TryFindAppointmentsByPhysicianID(uint Id, [NotNullWhen(true)] out List<Appointment> appointmentsFound)
    {
        appointmentsFound = appointments.FindAll(a => a.PhysicianId == Id).ToList();
        if (appointmentsFound != null) return true;
        return false;
    }

    public bool TryDeleteAppointmentByID(uint Id, [NotNullWhen(true)] out Appointment appointmentToDelete)
    {
        appointmentToDelete = appointments.FirstOrDefault(a => a.Id == Id) ?? new Appointment();

        if (appointmentToDelete == null) return false;

        appointments.RemoveAll(a => a.Id == Id);
        return true;
    }
}
