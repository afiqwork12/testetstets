using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemModelsLibrary
{
    public class Appointment : IComparable
    {
        private int id;
        private int patientID;
        private int doctorID;
        private string details;
        private DateTime date;
        private string status;//Open, Pending Payment, Paid, Closed
        private double price;
        

        public int Id { get => id; set => id = value; }
        public int PatientID { get => patientID; set => patientID = value; }
        public int DoctorID { get => doctorID; set => doctorID = value; }
        public string Details { get => details; set => details = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Status { get => status; set => status = value; }
        public double Price { get => price; set => price = value; }

        public Appointment()
        {
            status = "Opened";
        }
        public override string ToString()
        {
            return
                "Appointment ID: " + id +
                "\nPatient ID: " + patientID +
                "\nDoctor ID: " + doctorID +
                "\nPatient Notes: " + details +
                "\nDate: " + date.ToString("dd/MM/yyyy") +
                "\nTime: " + date.ToString("hh:mm tt") +
                "\nPrice: " + (price < 0.0 ? "To be decided": "$" + price) +
                "\nPayment Status: " + status;
        }
        public void TakeDetails(User user, List<User> listOfDoctors, List<Appointment> appointments)
        {
            patientID = user.Id;
            Console.WriteLine("| {0,-3} | {1,-10} | {2,-15} | {3,-20} |", "Id", "Name", "Speciality", "Years of Experience");
            foreach (var item in listOfDoctors)
            {
                Console.WriteLine("| {0,-3} | {1,-10} | {2,-15} | {3,-20} |", item.Id, item.Name, ((Doctor)item).Speciality, ((Doctor)item).Experience);
            }
            Console.WriteLine("Enter Doctor ID:");
            var check = true;
            while (check)
            {
                doctorID = GetIntInput();
                if (listOfDoctors.SingleOrDefault(d => d.Id == doctorID) == null)
                {
                    Console.WriteLine("Please select from the list of doctors above");
                }
                else
                {
                    check = false;
                }
            }
            Console.WriteLine("Enter Appointment Details:");
            do
            {
                details = Console.ReadLine();
                if (details == "")
                {
                    Console.WriteLine("Details cannot be blank");
                }
                else
                {
                    break;
                }
            } while (true);
            
            price = -1;
            Console.WriteLine("Enter Date (e.g. dd/MM/yyyy):");
            check = true;
            DateTime appDate = DateTime.Now.AddDays(-1.0);
            while (check)
            {
                while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, DateTimeStyles.None, out appDate))
                {
                    Console.WriteLine("Invalid Value. Try again.");
                }
                if (appDate < DateTime.Now)
                {
                    Console.WriteLine("Please enter a date after today.");
                }
                else
                {
                    var docApp = appointments.Where(x => x.doctorID == doctorID && x.date.Date == appDate.Date).ToList();
                    DateTime chosenTimeslot = GetTimeSlot(appDate, docApp);
                    if (chosenTimeslot < DateTime.Now)
                    {
                        Console.WriteLine("Please select another date");
                    }
                    else
                    {
                        date = chosenTimeslot;
                        check = false;
                    }
                }
            }
            status = "Opened";
        }

        private static DateTime GetTimeSlot(DateTime appDate, List<Appointment> docApp)
        {
            List<DateTime> timeslots = new List<DateTime>();
            for (int i = 9; i <= 17; i++)
            {
                string hour = (i > 12 ? i - 12 : i).ToString("D2") + ":00:00";
                timeslots.Add(DateTime.ParseExact(appDate.ToString("dd/MM/yyyy") + " " + hour + (i < 12 ? " AM" : " PM"), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture));
            }
            timeslots = timeslots.Where(x => !docApp.Select(y => y.date).Contains(x)).ToList();
            if (timeslots.Count > 0)
            {
                Console.WriteLine("Select a timeslot (choose from the numbers listed e.g. 0 to " + (timeslots.Count - 1));
                int count = 0;
                foreach (var item in timeslots)
                {
                    Console.WriteLine(count + " - " + item.ToString("hh:mm:ss tt"));
                    count++;
                }
                int option;
                do
                {
                    while (!int.TryParse(Console.ReadLine(), out option))
                    {
                        Console.WriteLine("Please enter a number from the selection above");
                    }
                    if (option >= 0 && option < timeslots.Count)
                    {
                        break;
                    }
                    Console.WriteLine("Please select from timeslot above");
                } while (true);
                var chosenTimeslot = timeslots[option];
                return chosenTimeslot;
            }
            else
            {
                return DateTime.Now.AddDays(-1.0);
            }
        }

        private static int GetIntInput()
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Invalid Value. Try again.");
            }
            return input;
        }

        public int CompareTo(object obj)
        {
            return date.CompareTo(((Appointment)obj).date);
        }
    }
}
