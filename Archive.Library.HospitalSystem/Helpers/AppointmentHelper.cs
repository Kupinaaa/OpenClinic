using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Library.HospitalSystem.Models;
using Library.HospitalSystem.Services;

namespace Library.HospitalSystem.Helpers;

public class AppointmentHelper
{
    private AppointmentService appointmentService = AppointmentService.Current;
    public void CreateAppointment(uint? Id = null) // Get next ID from the service
    {
        bool cont = true;
        Appointment addAppointment = new Appointment();

        bool foundId = false;
        if (Id != null) 
        {
            foundId = appointmentService.TryFindAppointmentByID((uint)Id, out addAppointment);
        }

        do {
            uint patientId, physicianId;

            Console.WriteLine("Enter the title of your appointment:");
            addAppointment.Title = Console.ReadLine() ?? "";

            Console.WriteLine("Shortly describe the appointment:");
            addAppointment.Description = Console.ReadLine() ?? "";

            Console.WriteLine("Enter the patient id:");
            while (!uint.TryParse(Console.ReadLine() ?? "0", out patientId)) 
            {
                Console.WriteLine("Please try again.");
            }

            Console.WriteLine("Enter the physician id:");
            while (!uint.TryParse(Console.ReadLine() ?? "0", out physicianId)) 
            {
                Console.WriteLine("Please try again.");
            }

            DateTime start, end;

            Console.WriteLine("Enter the start date-time of your appointment:");
            while (!DateTime.TryParse(Console.ReadLine() ?? "", new CultureInfo("en-US"), out start))
            {
                Console.WriteLine("Please try again and use the correct format for example MM/DD/YYYY HH:MM AM/PM");
            }
            Console.WriteLine("Your inputed start date-time is: " + start);

            Console.WriteLine("Enter the end date-time of your appointment:");
            while (!DateTime.TryParse(Console.ReadLine() ?? "", new CultureInfo("en-US"), out end)) 
            {
                Console.WriteLine("Please try again and use the correct format for example MM/DD/YYYY HH:MM AM/PM");
            }
            Console.WriteLine("Your inputed end date time is: " + end);

            string message;
            bool updTimeCheck = appointmentService.PatientCheckAppointmentAvailability(patientId, start, end, out message, Id) && 
            appointmentService.PhysicianCheckAppointmentAvailability(physicianId, start, end, out message, Id);

            if (foundId && updTimeCheck || !foundId) 
            {
                addAppointment.Id = Id ?? AppointmentService.NextId;
                addAppointment.PatientId = patientId;
                addAppointment.PhysicianId = physicianId;
                addAppointment.DateTimeStart = start;
                addAppointment.DateTimeEnd = end;

                if (foundId) 
                {
                    cont = false;
                }
                else 
                {
                    cont = !appointmentService.TryAddAppointment(addAppointment, out message, Id);
                }
            }
            Console.WriteLine(message);
        } while (cont);
    }

    public void ListAllAppointments() 
    {
        Console.WriteLine("List of all Appointments:");
        appointmentService.ListAllAppointments();
    }

    public void DisplayAppointmentById() 
    {
        uint id;
        do {
            Console.WriteLine("Enter the id of your Appointment:");
        } while (!uint.TryParse(Console.ReadLine(), out id));

        if (appointmentService.TryFindAppointmentByID(id, out Appointment? appointment)) 
        {
            Console.WriteLine(appointment);
        } 
        else 
        {
            Console.WriteLine("Appointment not found!");
        }
    }

    public void DisplayAppointmentsByPatientId() 
    {
        uint id;
        do {
            Console.WriteLine("Enter the id of your patient:");
        } while (!uint.TryParse(Console.ReadLine(), out id));

        if (appointmentService.TryFindAppointmentsByPatientID(id, out List<Appointment>? appointments)) 
        {
            appointments.ForEach(Console.WriteLine);
        } 
        else 
        {
            Console.WriteLine("Appointment not found!");
        }
    }
    public void DisplayAppointmentsByPhysicianId() 
    {
        uint id;
        do {
            Console.WriteLine("Enter the id of your physician:");
        } while (!uint.TryParse(Console.ReadLine(), out id));

        if (appointmentService.TryFindAppointmentsByPhysicianID(id, out List<Appointment>? appointments)) 
        {
            appointments.ForEach(Console.WriteLine);
        } 
        else 
        {
            Console.WriteLine("Appointment not found!");
        }
    }

    public void DeleteAppointmentById() {
        uint Id;

        do {
            Console.WriteLine("Enter the id of the Appointment you would like to delete:");
        } while (!uint.TryParse(Console.ReadLine() ?? "0", out Id));

        if (appointmentService.TryDeleteAppointmentByID(Id, out Appointment? appointment)) 
        {
            Console.WriteLine($"Sucessfuly deleted Appointment:\n{appointment}");
        } 
        else 
        {
            Console.WriteLine("No Appointments were deleted.");
        }
    }

    public void UpdateAppointmentById() {
        uint Id;

        do {
            Console.WriteLine("Enter the id of the Appointment you would like to update:");
        } while (!uint.TryParse(Console.ReadLine() ?? "0", out Id));

        if (appointmentService.TryFindAppointmentByID(Id, out Appointment? appointment)) 
        {
            Console.WriteLine("The Appointment you are trying to change is:");
            Console.WriteLine(appointment);

            CreateAppointment(Id); // UPD overload
        } 
        else 
        {
            Console.WriteLine("Appointment not found.");
        }
    }
}
