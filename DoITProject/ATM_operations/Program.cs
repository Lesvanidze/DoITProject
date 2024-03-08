using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;
using System.Xml;
using ATM_operations;
using Newtonsoft.Json;

namespace ATMOperations
{
   

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Register a new user
                Console.WriteLine("Enter your first name:");
                string firstName = Console.ReadLine();

                Console.WriteLine("Enter your last name:");
                string lastName = Console.ReadLine();

                Console.WriteLine("Enter your personal number:");
                string personalNumber = Console.ReadLine();

                ATM.RegisterUser(firstName, lastName, personalNumber);

                // Perform operations
                Console.WriteLine("Choose operation:\n1. Make Deposit\n2. Check Balance\n3. Cash Withdraw");
                string operation = Console.ReadLine();

                switch (operation)
                {
                    case "1":
                        Console.WriteLine("Enter the amount to deposit:");
                        decimal depositAmount = Convert.ToDecimal(Console.ReadLine());
                        ATM.Deposit(personalNumber, depositAmount);
                        break;
                    case "2":
                        decimal balance = ATM.CheckBalance(personalNumber);
                        Console.WriteLine($"Your current balance: {balance} GEL");
                        break;
                    case "3":
                        Console.WriteLine("Enter the amount to withdraw:");
                        decimal withdrawAmount = Convert.ToDecimal(Console.ReadLine());
                        ATM.Withdraw(personalNumber, withdrawAmount);
                        break;
                    default:
                        Console.WriteLine("Invalid operation selected.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            static bool ValidatePersonalNumber(string personalNumber)
            {
                return personalNumber.Length == 11 && long.TryParse(personalNumber, out _);
            }

        }
    }
}
