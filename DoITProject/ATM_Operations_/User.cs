using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Operations_
{


    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }

        public User()
        {
            Id = 0;
            Name = "";
            LastName = "";
            PersonalNumber = "";
            Password = "";
            Balance = 0.0;
        }

        public User(string name, string lastName, string personalNumber, string password)
        {
            Name = name;
            LastName = lastName;
            PersonalNumber = personalNumber;
            Password = password;
            Balance = 0.0;
        }
    }
}
