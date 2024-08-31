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

    public bool PatientCheckAppointmentAvailability(uint patientId, DateTime start, DateTime end, uint? updId = null) 
    {
        return start.Hour > 8 && end.Hour < 17 && start.Date == end.Date
               && !appointments.Any(a => a.Id != updId && a.PatientId == patientId && end >= a.DateTimeStart || start < a.DateTimeEnd);
    }

    public bool PhysicianCheckAppointmentAvailability(uint physicianId, DateTime start, DateTime end, uint? updId = null) 
    {
        return start.Hour >= 8 && end.Hour <= 17 && start.Date == end.Date 
               && !appointments.Any(a => a.Id != updId && a.PhysicianId == physicianId && end >= a.DateTimeStart || start < a.DateTimeEnd);
    }
    
    public bool TryAddAppointment(Appointment appointment, out string message, uint? updId = null)
    {
        message = string.Empty;
        if (appointment.DateTimeStart.Date != appointment.DateTimeStart.Date) 
        {
            message = "The start and end dates of the appointment have to be on the same day";
            return false;
        }

        if (!PatientCheckAppointmentAvailability(appointment.PatientId, appointment.DateTimeStart, appointment.DateTimeEnd, updId))
        {
            message = $"Check patient #{appointment.PatientId} availability";
            return false;
        }

        if(!PhysicianCheckAppointmentAvailability(appointment.PhysicianId, appointment.DateTimeStart, appointment.DateTimeEnd, updId))
        {
            message = $"Check physician #{appointment.PhysicianId} availability";
            return false;
        }

        appointments.Add(appointment);
        message = $"Appointment #{appointment.Id} {(updId != null ? "updated" : "created")} succesfully";
        return true;
    }

    public void ListAllAppointments() 
    {
        appointments.ForEach(Console.WriteLine);
    }

    public bool TryFindAppointmentByID(uint Id, [NotNullWhen(true)] out Appointment? appointment)
    {
        appointment = appointments.FirstOrDefault(a => a.Id == Id);
        if (appointment != null) return true;
        return false;
    }

    public bool TryFindAppointmentsByPatientID(uint Id, [NotNullWhen(true)] out List<Appointment>? appointmentsFound)
    {
        appointmentsFound = appointments.FindAll(a => a.PatientId == Id).ToList();
        if (appointmentsFound != null) return true;
        return false;
    }

    public bool TryFindAppointmentsByPhysicianID(uint Id, [NotNullWhen(true)] out List<Appointment>? appointmentsFound)
    {
        appointmentsFound = appointments.FindAll(a => a.PhysicianId == Id).ToList();
        if (appointmentsFound != null) return true;
        return false;
    }

    public bool TryDeleteAppointmentByID(uint Id, [NotNullWhen(true)] out Appointment? appointmentToDelete)
    {
        appointmentToDelete = appointments.FirstOrDefault(a => a.Id == Id);

        if (appointmentToDelete == null) return false;

        appointments.RemoveAll(a => a.Id == Id);
        return true;
    }
}
