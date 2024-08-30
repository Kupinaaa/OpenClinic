using System;
using Library.HospitalSystem;
using Library.HospitalSystem.Helpers;

namespace App.HospitalSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var PatientHelper = new PatientHelper();
            bool cont = true;
            while (cont) {
                Console.WriteLine("1. Add a patient");
                Console.WriteLine("2. List all patients");
                Console.WriteLine("3. Find patient by Id");
                Console.WriteLine("Q. Quit ");
                Console.WriteLine("Choose an option:");

                string option = Console.ReadLine() ?? "Q";

                switch(option) {
                    case "1":
                        PatientHelper.CreatePatient();
                        break;
                    case "2":
                        PatientHelper.ListAll();
                        break;
                    case "3":
                        PatientHelper.DisplayPatientById();
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