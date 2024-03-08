using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Operations_
{
    public class ATM
    {
        private List<User> users;
        private string usersFilePath = @"../../../users.json";
        private static string logFilePath;

        public ATM()
        {
            users = LoadUsers();
        }

        private List<User> LoadUsers()
        {
            if (!File.Exists(usersFilePath))
            {
                File.WriteAllText(usersFilePath, "[]");
                return new List<User>();
            }

            string json = File.ReadAllText(usersFilePath);
            return JsonConvert.DeserializeObject<List<User>>(json);
        }

        private void SaveUsers()
        {
            string json = JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(usersFilePath, json);
        }

        public void Register(string name, string lastName, string personalNumber)
        {
            Random rnd = new Random();
            string password = rnd.Next(1000, 9999).ToString();

            User newUser = new User(name, lastName, personalNumber, password);
            newUser.Id = users.Count + 1;
            users.Add(newUser);
            SaveUsers();

            Console.WriteLine($"Registered successfully! Your ID is: {newUser.Id} and Password is: {password}");
        }

        public User Login(string personalNumber, string password)
        {
            foreach (User user in users)
            {
                if (user.PersonalNumber == personalNumber && user.Password == password)
                {
                    Console.WriteLine($"Welcome, {user.Name} {user.LastName}!");
                    return user;
                }
            }
            throw new Exception("Invalid personal number or password.");
        }

        public void CheckBalance(User user)
        {
            Console.WriteLine($"Your current balance is: {user.Balance}");
            LogTransaction(user, $"Checked the balance");
        }

        public void Deposit(User user, double amount)
        {
            user.Balance += amount;
            Console.WriteLine($"Deposited {amount}. Your current balance is: {user.Balance}");
            LogTransaction(user, $"Deposited {amount}");
        }

        public void Withdraw(User user, double amount)
        {
            if (user.Balance >= amount)
            {
                user.Balance -= amount;
                Console.WriteLine($"Withdrawn {amount}. Your current balance is: {user.Balance}");
                LogTransaction(user, $"Withdrawn {amount}");
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }

       

        private void LogTransaction(User user, string action)
        {
            string logFilePath = @"../../../transaction_log.json";
            List<string> logs = new List<string>();

            if (File.Exists(logFilePath))
            {
                string json = File.ReadAllText(logFilePath);
                logs = JsonConvert.DeserializeObject<List<string>>(json);
            }

            string log = $"User {user.Name} {user.LastName} - {action} on: {DateTime.Now}";
            logs.Add(log);

            string updatedLogs = JsonConvert.SerializeObject(logs, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(logFilePath, updatedLogs);
        }

        public static void ViewTransactionHistory()
        {
            if (File.Exists(logFilePath))
            {
                string transactionLog = File.ReadAllText(logFilePath);
                Console.WriteLine("Transaction History:");
                Console.WriteLine(transactionLog);
            }
            else
            {
                Console.WriteLine("Transaction history is empty.");
            }
        }

    }
}
