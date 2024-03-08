using HangingGame;
using System.Xml.Linq;

public class HangmanGame
{
    private readonly List<string> words = new List<string>
        {
            "apple", "banana", "orange", "grape", "kiwi",
            "strawberry", "pineapple", "blueberry", "peach", "watermelon"
        };

    private readonly string xmlFilePath = @"../../../players.xml";
    private List<Player> players;

    public HangmanGame()
    {
        LoadPlayers();
    }

    public void StartGame()
    {
        Console.WriteLine("Welcome to Hanging Game!");

        string playerName = GetPlayerName();

        Player currentPlayer = GetOrCreatePlayer(playerName);

        int score = PlayGame();
        Console.WriteLine($"Your score is: {score}");

        currentPlayer.Score = score;
        UpdatePlayerData(currentPlayer);

        DisplayTopPlayers();
    }

    private void LoadPlayers()
    {
        if (!File.Exists(xmlFilePath))
        {
            players = new List<Player>();
            return;
        }

        XElement xml = XElement.Load(xmlFilePath);
        players = xml.Elements("Player")
                    .Select(p => new Player
                    {
                        Name = p.Element("Name").Value,
                        Score = int.Parse(p.Element("Score").Value)
                    })
                    .OrderByDescending(p => p.Score)
                    .ToList();
    }

    private void UpdatePlayerData(Player player)
    {
        if (!players.Any(p => p.Name == player.Name))
        {
            players.Add(player);
        }
        else
        {
            Player existingPlayer = players.First(p => p.Name == player.Name);
            existingPlayer.Score = Math.Max(existingPlayer.Score, player.Score);
        }

        players = players.OrderByDescending(p => p.Score).Take(50).ToList();

        XElement xml = new XElement("Players",
                        players.Select(p => new XElement("Player",
                            new XElement("Name", p.Name),
                            new XElement("Score", p.Score)
                        )));

        xml.Save(xmlFilePath);
    }

    private void DisplayTopPlayers()
    {
        Console.WriteLine("\nTop 10 Players:");
        for (int i = 0; i < Math.Min(10, players.Count); i++)
        {
            Console.WriteLine($"{i + 1}. {players[i].Name} - {players[i].Score} points");
        }
    }

    private string GetPlayerName()
    {
        string playerName;
        do
        {
            Console.Write("Please enter your name: ");
            playerName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(playerName))
            {
                Console.WriteLine("Name cannot be empty or whitespace.");
            }
            else if (players.Any(p => p.Name == playerName))
            {
                Console.WriteLine("This name already exists. If you are playing for the first time, please use a different name.");
            }
        } while (string.IsNullOrWhiteSpace(playerName) || players.Any(p => p.Name == playerName));

        return playerName;
    }

    private Player GetOrCreatePlayer(string playerName)
    {
        Player player = players.FirstOrDefault(p => p.Name == playerName);
        if (player == null)
        {
            player = new Player { Name = playerName, Score = 0 };
        }

        return player;
    }

    private int PlayGame()
    {
        string chosenWord = words[new Random().Next(words.Count)];
        string hiddenWord = new string('-', chosenWord.Length);
        int attemptsLeft = 6;
        HashSet<char> guessedLetters = new HashSet<char>();

        while (attemptsLeft > 0)
        {
            Console.WriteLine($"Word: {hiddenWord}");
            Console.WriteLine($"Attempts left: {attemptsLeft}");

            Console.Write("Guess a letter or the whole word: ");
            string input = Console.ReadLine().ToLower();

            if (input.Length == 1)
            {
                char guessedLetter;
                if (char.TryParse(input, out guessedLetter) && char.IsLetter(guessedLetter))
                {
                    if (!guessedLetters.Contains(guessedLetter))
                    {
                        guessedLetters.Add(guessedLetter);
                        if (chosenWord.Contains(guessedLetter))
                        {
                            for (int i = 0; i < chosenWord.Length; i++)
                            {
                                if (chosenWord[i] == guessedLetter)
                                {
                                    hiddenWord = hiddenWord.Substring(0, i) + guessedLetter + hiddenWord.Substring(i + 1);
                                }
                            }
                        }
                        else
                        {
                            attemptsLeft--;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You have already guessed this letter.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid letter.");
                }
            }
            else if (input.Length == chosenWord.Length && input == chosenWord)
            {
                return CalculateScore(7);
            }
            else
            {
                Console.WriteLine("Incorrect word guess.");
            }

            if (hiddenWord == chosenWord)
            {
                return CalculateScore(7 - attemptsLeft);
            }
        }

        return 0; // No points earned
    }

    private int CalculateScore(int attemptsLeft)
    {
        return 8 - attemptsLeft;
    }
}