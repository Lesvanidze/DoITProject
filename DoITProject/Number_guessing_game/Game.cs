using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Number_guessing_game
{
    public class Game
    {
        private readonly string csvFilePath = @"../../../player_scores.csv";
        private List<Player> players;


        public object Players { get; internal set; }
        public object Save { get; internal set; }

        public Game()
        {
            LoadPlayers();
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the Number Guessing Game!");
            string playerName = GetUniquePlayerName();

            if (!File.Exists(csvFilePath))
            {
                File.Create(csvFilePath).Close(); // Create the file if it doesn't exist
            }

            while (true)
            {
                PlayGame(playerName);

                Console.WriteLine("Do you want to play again? (Y/N)");
                if (Console.ReadKey().Key != ConsoleKey.Y)
                    break;
                DisplayTopPlayers();
            }

            ChooseDifficulty(out int maxNumber, out int multiplier);

            Random random = new Random();
            int secretNumber = random.Next(1, maxNumber + 1);

            int points = 0;
            int attempts = 0;

            while (attempts < 10)
            {
                Console.Write($"Guess the number (1-{maxNumber}): ");
                if (!int.TryParse(Console.ReadLine(), out int guess))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                attempts++;

                if (guess == secretNumber)
                {
                    Console.WriteLine($"Congratulations, {playerName}! You guessed the number in {attempts} attempts.");
                    points = CalculatePoints(attempts, multiplier);
                    break;
                }
                else
                {
                    Console.WriteLine(guess < secretNumber ? "Too low. Try again." : "Too high. Try again.");
                }
            }

            SavePlayerScore(playerName, points);
            DisplayTopPlayers();
        }

        private void LoadPlayers()
        {
            players = new List<Player>();

            if (!File.Exists(csvFilePath))
            {
                File.Create(csvFilePath).Close();
            }

            string[] lines = File.ReadAllLines(csvFilePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                players.Add(new Player { Name = parts[0], Points = int.Parse(parts[1]) });
            }
        }

        private string GetUniquePlayerName()
        {
            string playerName;
            do
            {
                Console.Write("Enter your name: ");
                playerName = Console.ReadLine();

                if (players.Any(p => p.Name == playerName))
                {
                    Console.WriteLine($"The name '{playerName}' is already taken. Please choose another name.");
                }
            } while (players.Any(p => p.Name == playerName));
            return playerName;
        }


        private void PlayGame(string playerName)
        {
            ChooseDifficulty(out int maxNumber, out int multiplier);

            Random random = new Random();
            int secretNumber = random.Next(1, maxNumber + 1);

            int points = 0;
            int attempts = 0;

            while (attempts < 10)
            {
                Console.Write($"Guess the number (1-{maxNumber}): ");
                if (!int.TryParse(Console.ReadLine(), out int guess))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                attempts++;

                if (guess == secretNumber)
                {
                    Console.WriteLine($"Congratulations, {playerName}! You guessed the number in {attempts} attempts.");
                    points = CalculatePoints(attempts, multiplier);
                    break;
                }
                else
                {
                    Console.WriteLine(guess < secretNumber ? "Too low. Try again." : "Too high. Try again.");
                }
            }

            SavePlayerScore(playerName, points);
        }


        private void ChooseDifficulty(out int maxNumber, out int multiplier)
        {
            Console.WriteLine("Choose difficulty:");
            Console.WriteLine("1. Easy (1-15)");
            Console.WriteLine("2. Medium (1-25)");
            Console.WriteLine("3. Hard (1-50)");

            int choice;
            do
            {
                Console.Write("Enter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                {
                    Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                }
            } while (choice < 1 || choice > 3);

            switch (choice)
            {
                case 1:
                    maxNumber = 15;
                    multiplier = 1;
                    break;
                case 2:
                    maxNumber = 25;
                    multiplier = 2;
                    break;
                case 3:
                    maxNumber = 50;
                    multiplier = 3;
                    break;
                default:
                    maxNumber = 15;
                    multiplier = 1;
                    break;
            }
        }

        private int CalculatePoints(int attempts, int multiplier)
        {
            int points = 10 - attempts + 1;
            return Math.Max(points * multiplier, 0);
        }

        private void SavePlayerScore(string playerName, int points)
        {
            using (StreamWriter writer = File.AppendText(csvFilePath))
            {
                writer.WriteLine($"{playerName},{points}");
            }
        }

        private void DisplayTopPlayers()
        {
            Console.WriteLine("Top 10 Players:");
            var topPlayers = players.OrderByDescending(p => p.Points).Take(10);
            foreach (var player in topPlayers)
            {
                Console.WriteLine($"{player.Name} - {player.Points} points");
            }
        }
    }
}
