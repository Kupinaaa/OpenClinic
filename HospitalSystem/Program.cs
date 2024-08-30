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
                Console.WriteLine("Q. Quit ");
                Console.WriteLine("Choose an option:");

                string option = Console.ReadLine() ?? "Q";

                switch(option) {
                    case "1":
                        PatientHelper.CreatePatient();
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