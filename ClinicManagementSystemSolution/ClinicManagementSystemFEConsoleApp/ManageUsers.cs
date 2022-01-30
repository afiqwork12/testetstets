using ClinicManagementSystemModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemFEConsoleApp
{
    public class ManageUsers
    {
        public List<User> users;
        public User currentUser;
        public ManageUsers()
        {
        }
        public ManageUsers(User user, List<User> users)
        {
            currentUser = user;
            this.users = users;
        }
        public User LoginUser()
        {
            do
            {
                string username = "";
                Console.WriteLine("Please enter your username");
                do
                {
                    username = Console.ReadLine();
                    if (username == "")
                    {
                        Console.WriteLine("Username cannot be blank");
                    }                    
                } while (username == "");
                string password = "";
                Console.WriteLine("Please enter your password");
                do
                {
                    password = Console.ReadLine();
                    if (password == "")
                    {
                        Console.WriteLine("Password cannot be blank");
                    }
                } while (password == "");
                currentUser = users.SingleOrDefault(u => username == u.Name + u.Id && u.Password == password);
                if (currentUser == null)
                {
                    Console.WriteLine("Please enter the correct username and password");
                }
            } while (currentUser == null);
            Console.Clear();
            
            return currentUser;
        }
    }
}
