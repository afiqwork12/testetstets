using ClinicManagementSystemModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemFEConsoleApp
{
    class Program
    {
        static User currentUser;
        static List<User> users = new List<User>();
        static List<Appointment> appointments = new List<Appointment>();
        static void GenerateAppointments()
        {
            appointments.Add(new Appointment() { Id = 1, PatientID = 101, DoctorID = 201, Details = "Follow Up", Date = new DateTime(2022, 2, 16, 14, 00, 0), Price = -1, Status = "Opened" });
            appointments.Add(new Appointment() { Id = 2, PatientID = 101, DoctorID = 201, Details = "Blood Test", Date = new DateTime(2022, 1, 20, 12, 00, 0), Price = -1, Status = "Opened" });
            appointments.Add(new Appointment() { Id = 3, PatientID = 101, DoctorID = 201, Details = "Diarrhea", Date = new DateTime(2022, 5, 20, 12, 00, 0), Price = -1, Status = "Opened" });
            appointments.Add(new Appointment() { Id = 4, PatientID = 101, DoctorID = 201, Details = "Headache", Date = new DateTime(2022, 1, 10, 14, 00, 0), Price = 444.36, Status = "Pending Payment" });
            appointments.Add(new Appointment() { Id = 5, PatientID = 101, DoctorID = 201, Details = "Vomitting", Date = new DateTime(2022, 1, 11, 14, 00, 0), Price = 52.2, Status = "Pending Payment" });
            appointments.Add(new Appointment() { Id = 6, PatientID = 101, DoctorID = 201, Details = "Health Check Up", Date = new DateTime(2022, 1, 12, 14, 00, 0), Price = 62.2, Status = "Paid" });
            appointments.Add(new Appointment() { Id = 7, PatientID = 102, DoctorID = 201, Details = "Blood Test", Date = new DateTime(2021, 12, 15, 11, 00, 0), Price = 20.0, Status = "Paid" });
            appointments.Add(new Appointment() { Id = 8, PatientID = 102, DoctorID = 201, Details = "Health Check Up", Date = new DateTime(2020, 5, 4, 13, 00, 0), Price = 123.6, Status = "Paid" });
        }
        static void GenerateUsers()
        {
            users.Add(new Patient() { Id = 101, Name = "John", Password = "123456", Age = 25, Remarks = "Lower Back Pain", Status = "Pain Manageable" });
            users.Add(new Patient() { Id = 102, Name = "Mary", Password = "123456", Age = 36, Remarks = "Swollen Appendix", Status = "In alot of pain" });
            users.Add(new Doctor() { Id = 201, Name = "James", Password = "123456", Age = 33, Speciality = "Orthopedics", Experience = 4 });
            users.Add(new Doctor() { Id = 202, Name = "Sally", Password = "123456", Age = 38, Speciality = "Surgery", Experience = 10 });
            users.Add(new Doctor() { Id = 203, Name = "Mark", Password = "123456", Age = 28, Speciality = "Dermatology", Experience = 5 });
            users.Add(new Doctor() { Id = 204, Name = "Jane", Password = "123456", Age = 29, Speciality = "Family Medicine", Experience = 6 });
        }
        static void ManageAppointmentsPatientSide()
        {
            int choice;
            ManageAppointments ma = new ManageAppointments(currentUser, users, appointments);
            do
            {
                WelcomeMsg();
                Console.WriteLine("Choose from the options");
                Console.WriteLine("1: View Upcoming Appointments");
                Console.WriteLine("2: View Past Appointment");
                Console.WriteLine("3: Pay for Appointments");
                Console.WriteLine("4: Make an Appointment");
                Console.WriteLine("0: Log Off");
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Please enter a number");
                }
                switch (choice)
                {
                    case 1:
                        ma.PrintUpcomingAppointments();
                        break;
                    case 2:
                        ma.PrintPastAppointments();
                        break;
                    case 3:
                        ma.PayForAppointment();
                        break;
                    case 4:
                        ma.MakeAppointment();
                        break;
                    case 0:
                        Console.WriteLine("Bye Bye");
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
                ClearScreen();
            } while (choice != 0);
            appointments = ma.appointments;
        }

        private static void ClearScreen()
        {
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
            Console.Clear();
        }

        static void ManageAppointmentsDoctorSide()
        {
            int choice;
            ManageAppointments ma = new ManageAppointments(currentUser, users, appointments);
            do
            {
                WelcomeMsg();
                Console.WriteLine("Choose from the options");
                Console.WriteLine("1: View Upcoming Appointments");
                Console.WriteLine("2: View Past Appointment");
                Console.WriteLine("3: Raise Payment Request");
                Console.WriteLine("4: Add Remarks to Upcoming Appointment");
                Console.WriteLine("0: Log Off");
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Please enter a number");
                }
                switch (choice)
                {
                    case 1:
                        ma.PrintUpcomingAppointments();
                        break;
                    case 2:
                        ma.PrintPastAppointments();
                        break;
                    case 3:
                        ma.RaisePayment();
                        break;
                    case 4:
                        ma.AddRemarks();
                        break;
                    case 0:
                        Console.WriteLine("Bye Bye");
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
                ClearScreen();
            } while (choice != 0);
            appointments = ma.appointments;
        }

        private static void WelcomeMsg()
        {
            if (currentUser.Type == "Patient")
            {
                Console.WriteLine("Welcome, " + currentUser.Name);
            }
            else
            {
                Console.WriteLine("Welcome, Dr." + currentUser.Name);
            }
        }

        static void Main(string[] args)
        {
            Console.Title = "Clinic Management System";
            GenerateAppointments();
            GenerateUsers();
            bool check = true;
            while (check)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Clinic");
                ManageUsers manageUsers = new ManageUsers(currentUser, users);
                currentUser = manageUsers.LoginUser();
                users = manageUsers.users;
                if (currentUser.Type == "Patient")
                {
                    ManageAppointmentsPatientSide();
                }
                else
                {
                    ManageAppointmentsDoctorSide();
                }
                currentUser = null;
            }
            Console.ReadLine();
        }
    }
}
