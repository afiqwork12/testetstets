using ClinicManagementSystemModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemFEConsoleApp
{
    public class ManageAppointments
    {
        public List<Appointment> appointments;
        public List<User> users;
        public User currentUser;
        public ManageAppointments()
        {
        }
        public ManageAppointments(User user, List<User> users, List<Appointment> appointments)
        {
            currentUser = user;
            this.users = users;
            this.appointments = appointments;
        }
        public void MakeAppointment()
        {
            Console.WriteLine("Please select a doctor from the list below");
            Appointment newAppointment = new Appointment
            {
                Id = appointments.Count < 0 ? 1 : appointments.Max(x => x.Id) + 1
            };
            newAppointment.TakeDetails(currentUser, users.Where(u => u.Type == "Doctor").ToList(), appointments);
            appointments.Add(newAppointment);
            Console.WriteLine("Appointment Made. Displaying Appointment Details.");
            PrintAppointment(newAppointment);
        }
        public void PayForAppointment()
        {
            var temp = appointments.Where(x => x.PatientID == currentUser.Id && x.Date < DateTime.Now && x.Status == "Pending Payment").ToList();
            if (temp.Count > 0)
            {
                Console.WriteLine("Please select an unpaid appointment");
                PrintAppointmentsFromList(temp);
                var check = true;
                Console.WriteLine("Please enter the appointment ID");
                int id;
                while (check)
                {
                    while (!int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.WriteLine("Invalid input. Try again.");
                    }
                    if (temp.Find(x => x.Id == id) == null)
                    {
                        Console.WriteLine("Please select from the above list.");
                    }
                    else
                    {
                        check = false;
                        Appointment apt = appointments.SingleOrDefault(a => a.Id == id);
                        if (apt != null)
                        {
                            int index = appointments.IndexOf(apt);
                            if (index > 0)
                            {

                                appointments[index].Status = "Paid";
                                Console.WriteLine("Payment Updated. Displaying Appointment Details");
                                PrintAppointment(appointments[index]);
                            }
                            else
                            {
                                Console.WriteLine("Opps");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Opps");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no appointments to make payment on");
            }
        }
        public void AddRemarks()
        {
            var temp = appointments.Where(x => x.DoctorID == currentUser.Id && x.Date >= DateTime.Now).ToList();
            if (temp.Count > 0)
            {
                Console.WriteLine("Please select an appointment for adding remarks to");
                PrintAppointmentsFromList(temp);
                Console.WriteLine("Please enter the appointment ID");
                int id;
                var check = true;
                while (check)
                {
                    while (!int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.WriteLine("Invalid input. Try again.");
                    }
                    if (temp.Find(x => x.Id == id) == null)
                    {
                        Console.WriteLine("Please select from the above list.");
                    }
                    else
                    {
                        check = false;
                        Console.WriteLine("Please add a remark");
                        string details = Console.ReadLine();
                        Appointment ta = appointments.SingleOrDefault(a => a.Id == id);
                        if (ta != null)
                        {
                            int idx = appointments.IndexOf(ta);
                            appointments[idx].Details += "\nDoctor Notes: " + details;
                            Console.WriteLine("Remarks added. Displaying Appointment Details.");
                            PrintAppointment(appointments[idx]);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no appointments to raise payments on");
            }
        }
        public void RaisePayment()
        {
            var temp = appointments.Where(x => x.DoctorID == currentUser.Id && x.Date < DateTime.Now && x.Status == "Opened").ToList();
            if (temp.Count > 0)
            {
                Console.WriteLine("Please select an appointment for raising payment request");
                PrintAppointmentsFromList(temp);
                Console.WriteLine("Please enter the appointment ID");
                int id;
                var check = true;
                while (check)
                {
                    while (!int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.WriteLine("Invalid input. Try again.");
                    }
                    if (temp.Find(x => x.Id == id) == null)
                    {
                        Console.WriteLine("Please select from the above list.");
                    }
                    else
                    {
                        check = false;
                        Console.WriteLine("Please enter any message to be saved");
                        string details = Console.ReadLine();
                        Console.WriteLine("Please enter amount to be collected");
                        double price;
                        while (!double.TryParse(Console.ReadLine(), out price))
                        {
                            Console.WriteLine("Invalid input. Try again.");
                        }
                        Appointment ta = appointments.SingleOrDefault(a => a.Id == id);
                        if (ta != null)
                        {
                            int idx = appointments.IndexOf(ta);
                            appointments[idx].Details += "\nDoctor Notes: " + details;
                            appointments[idx].Price = price;
                            appointments[idx].Status = (price == 0 ? "Paid" : "Pending Payment");
                            Console.WriteLine("Payment Raised. Displaying Appointment Details.");
                            PrintAppointment(appointments[idx]);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no appointments to raise payments on");
            }
        }

        private void PrintAppointmentsFromList(List<Appointment> temp)
        {
            foreach (var item in temp)
            {
                PrintAppointment(item);
            }
        }

        
        public void PrintUpcomingAppointments()
        {
            List<Appointment> temp;
            Console.WriteLine("Displaying Upcoming Appointments");
            if (currentUser.Type == "Patient")
            {
                temp = appointments.Where(x => x.PatientID == currentUser.Id && x.Date >= DateTime.Now).ToList();
            }
            else
            {
                temp = appointments.Where(x => x.DoctorID == currentUser.Id && x.Date >= DateTime.Now).ToList();
            }
            if (temp.Count > 0)
            {
                PrintAppointmentsFromList(temp);
            }
            else
            {
                Console.WriteLine("There are no upcoming appointments");
            }
        }
        public void PrintPastAppointments()
        {
            List<Appointment> temp;
            Console.WriteLine("Displaying Past Appointments");
            if (currentUser.Type == "Patient")
            {
                temp = appointments.Where(x => x.PatientID == currentUser.Id && x.Date < DateTime.Now).ToList();
            }
            else
            {
                temp = appointments.Where(x => x.DoctorID == currentUser.Id && x.Date < DateTime.Now).ToList();
            }
            if (temp.Count > 0)
            {
                PrintAppointmentsFromList(temp);
            }
            else
            {
                Console.WriteLine("There are no past appointments");
            }

        }
        public void PrintAllAppointments()
        {
            if (appointments.Count > 0)
            {
                for (int i = 0; i < appointments.Count; i++)
                {
                    PrintAppointment(appointments[i]);
                }
            }
            else
            {
                Console.WriteLine("No Appointments created yet");
            }
        }
        public void PrintAppointment(Appointment appointment)
        {
            Console.WriteLine("************************");
            Console.WriteLine(appointment);
            Console.WriteLine("************************");
        }
        public Appointment GetAppointmentByID(int ID)
        {
            return appointments.Find(a => a.Id == ID);
        }
        public int GetIdFromUser()
        {
            Console.WriteLine("Please enter the appointment ID:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid entry. Please try again.");
            }
            return id;
        }
    }
}
