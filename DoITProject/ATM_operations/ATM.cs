using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_operations
{
    public static class ATM
    {
        private static List<User> users = new List<User>();
        private static string logFilePath = "C:\\Users\\Levan\\Desktop\\C#\\DoITProject\\DoITProject\\ATM_operations\\ATMLog.json";

        static ATM()
        {
            if (!File.Exists(logFilePath))
            {
                File.WriteAllText(logFilePath, "");
            }
        }

        public static void RegisterUser(string firstName, string lastName, string personalNumber)
        {
            if (IsUserRegistered(personalNumber))
            {
                throw new InvalidOperationException("User already registered.");
            }

            string password = GeneratePassword();
            User newUser = new User(firstName, lastName, personalNumber, password, 0);
            users.Add(newUser);
            LogTransaction($"{firstName} {lastName} registered with personal number: {personalNumber}");
            SaveChanges();
            Console.WriteLine($"User registered successfully. Your password is: {password}");
        }

        public static void Deposit(string personalNumber, decimal amount)
        {
            User user = GetUser(personalNumber);
            user.Balance += amount;
            LogTransaction($"{user.FirstName} {user.LastName} deposited {amount} GEL. Current balance: {user.Balance} GEL");
            SaveChanges();
        }

        public static void Withdraw(string personalNumber, decimal amount)
        {
            User user = GetUser(personalNumber);
            if (user.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient balance.");
            }

            user.Balance -= amount;
            LogTransaction($"{user.FirstName} {user.LastName} withdrew {amount} GEL. Current balance: {user.Balance} GEL");
            SaveChanges();
        }

        public static decimal CheckBalance(string personalNumber)
        {
            User user = GetUser(personalNumber);
            return (decimal)user.Balance;
        }

        public static void ViewTransactionHistory()
        {
            string logContent = File.ReadAllText(logFilePath);
            Console.WriteLine(logContent);
        }

        private static bool IsUserRegistered(string personalNumber)
        {
            foreach (User user in users)
            {
                if (user.PersonalNumber == personalNumber)
                {
                    return true;
                }
            }
            return false;
        }

        private static User GetUser(string personalNumber)
        {
            foreach (User user in users)
            {
                if (user.PersonalNumber == personalNumber)
                {
                    return user;
                }
            }
            throw new InvalidOperationException("User not found.");
        }

        private static void LogTransaction(string transaction)
        {
            string log = $"{DateTime.Now}: {transaction}\n";
            File.AppendAllText(logFilePath, log);
        }

        private static void SaveChanges()
        {
            string jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText("users.json", jsonData);
        }

        private static string GeneratePassword()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString();
        }
    }
}
