using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemModelsLibrary
{
    public class Doctor : User
    {
        private int experience;
        private string speciality;

        public int Experience { get => experience; set => experience = value; }
        public string Speciality { get => speciality; set => speciality = value; }

        public Doctor()
        {
            Type = "Doctor";
        }
        public override string ToString()
        {
            var output = base.ToString();
            output += "Speciality: " + speciality + "\n";
            output += "Experience: " + experience + " years\n";
            return output;
        }
        public override void TakeDetails()
        {
            base.TakeDetails();
            Console.WriteLine("Please Enter Your Speciality:");
            speciality = Console.ReadLine();
            Console.WriteLine("Please Enter Your Years of Experience:");
            experience = Convert.ToInt32(Console.ReadLine());
        }
    }
}
