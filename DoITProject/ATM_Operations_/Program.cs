using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using ATM_Operations_;
using Newtonsoft.Json;

namespace ATMOperations
{

    class Program
    {
        static void Main(string[] args)
        {
            ATM atm = new ATM();

            try
            {
                Console.WriteLine("Are you registered? (yes/no): ");
                string response = Console.ReadLine().ToLower();

                if (response == "no")
                {
                    Console.WriteLine("Please register:");
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Last Name: ");
                    string lastName = Console.ReadLine();
                    Console.Write("Personal Number: ");
                    string personalNumber = Console.ReadLine();

                    atm.Register(name, lastName, personalNumber);
                }
                else if (response == "yes")
                {
                    Console.Write("Enter Personal Number: ");
                    string personalNumber = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    string password = Console.ReadLine();

                    User currentUser = atm.Login(personalNumber, password);

                    Console.WriteLine("Choose operation:");
                    Console.WriteLine("1. Check Balance");
                    Console.WriteLine("2. Deposit");
                    Console.WriteLine("3. Withdraw");
                    Console.WriteLine("4. View Transaction History");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            atm.CheckBalance(currentUser);
                            break;
                        case 2:
                            Console.Write("Enter amount to deposit: ");
                            double depositAmount = Convert.ToDouble(Console.ReadLine());
                            atm.Deposit(currentUser, depositAmount);
                            break;
                        case 3:
                            Console.Write("Enter amount to withdraw: ");
                            double withdrawAmount = Convert.ToDouble(Console.ReadLine());
                            atm.Withdraw(currentUser, withdrawAmount);
                            break;
                        case 4:
                            ATM.ViewTransactionHistory();
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            Console.ReadKey();
                            Console.ReadKey();
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid response.");
                    Console.ReadKey();
                    Console.ReadKey();
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.ReadKey();
                Console.ReadKey();
                Console.ReadKey();
            }
        }
    }
}
