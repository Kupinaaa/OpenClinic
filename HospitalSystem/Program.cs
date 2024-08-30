﻿using System;
using Library.HospitalSystem;
using Library.HospitalSystem.Helpers;

namespace App.HospitalSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var PatientHelper = new PatientHelper();
            var PhysicianHelper = new PhysicianHelper();
            bool cont = true;
            while (cont) {
                Console.WriteLine("1. Add a patient");
                Console.WriteLine("2. List all patients");
                Console.WriteLine("3. Find patient by Id");
                Console.WriteLine("4. Search for patient");
                Console.WriteLine("5. Delete patient by Id");
                Console.WriteLine("6. Update patient by Id");
                Console.WriteLine("7. Add a physician");
                Console.WriteLine("8. List all physicians");
                Console.WriteLine("9. Find physician by Id");
                Console.WriteLine("10. Search for physician");
                Console.WriteLine("11. Delete physician by Id");
                Console.WriteLine("12. Update physician by Id");
                Console.WriteLine("Q. Quit ");
                Console.WriteLine("Choose an option:");

                string option = Console.ReadLine() ?? "Q";

                switch(option) {
                    case "1":
                        PatientHelper.CreatePatient();
                        break;
                    case "2":
                        PatientHelper.ListAllPatients();
                        break;
                    case "3":
                        PatientHelper.DisplayPatientById();
                        break;
                    case "4":
                        PatientHelper.SearchPatientByQuery();
                        break;
                    case "5":
                        PatientHelper.DeletePatientById();
                        break;
                    case "6":
                        PatientHelper.UpdatePatientById();
                        break;
                    case "7":
                        PhysicianHelper.CreatePhysician();
                        break;
                    case "8":
                        PhysicianHelper.ListAllPhysicians();
                        break;
                    case "9":
                        PhysicianHelper.DisplayPhysicianById();
                        break;
                    case "10":
                        PhysicianHelper.SearchPhysicianByQuery();
                        break;
                    case "11":
                        PhysicianHelper.DeletePhysicianById();
                        break;
                    case "12":
                        PhysicianHelper.UpdatePhysicianById();
                        break;
                    case "q":
                    case "Q":
                        cont = false;
                        break;
                    default:
                        Console.WriteLine("Unknown option, please try again!");
                        break;
                }
            }
        }
    }
}