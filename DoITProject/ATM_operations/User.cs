using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_operations
{
    public class User
    {
       
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }

        public User(string firstName, string lastName, string personalNumber, string password, decimal balance)
        {
            FirstName = firstName;
            LastName = lastName;
            PersonalNumber = personalNumber;
            Password = password;
            Balance = balance;
        }

 
    }
}
