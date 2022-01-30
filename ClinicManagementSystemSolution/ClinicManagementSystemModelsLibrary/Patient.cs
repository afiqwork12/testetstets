using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemModelsLibrary
{
    public class Patient : User
    {
        private string remarks;
        private string status;

        public string Remarks { get => remarks; set => remarks = value; }
        public string Status { get => status; set => status = value; }

        public Patient()
        {
            Type = "Patient";
        }
        public override string ToString()
        {
            var output = base.ToString();
            output += "Remarks: " + remarks + "\n";
            output += "Status: " + status + "\n";
            return output;
        }
        public override void TakeDetails()
        {
            base.TakeDetails();
            Console.WriteLine("Please Enter Your Remarks:");
            remarks = Console.ReadLine();
            Console.WriteLine("Please Enter Your Status:");
            status = Console.ReadLine();
        }
    }
}
