using System.Xml.Serialization;
using Word_game;

class Game
{
    private const string filePath = @"../../../playerData.xml";
    private string playerName;

    public void Start()
    {
        Console.WriteLine("Welcome to the Word Game!");

        // Check if the player name is already recorded
        if (IsPlayerNameExists())
        {
            Console.WriteLine($"Welcome back, {playerName}!");

            // Prompt user to continue or start a new game
            Console.WriteLine("Do you want to continue your previous game? (Y/N)");
            char choice = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();

            if (choice == 'N')
            {
                playerName = GetUniquePlayerName();
            }
        }
        else
        {
            playerName = GetUniquePlayerName();
        }

        // Start or continue the game
        PlayGame();
    }

    private bool IsPlayerNameExists()
    {
        Console.WriteLine("Please enter your name:");
        string name = Console.ReadLine().Trim();

        // Check if the player name exists in the player data file
        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<UserData>));
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                List<UserData> userList = (List<UserData>)serializer.Deserialize(fs);
                foreach (var user in userList)
                {
                    if (user.Name == name)
                    {
                        playerName = name;
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private void PlayGame()
    {
        // Implement game logic here
    }

    private string GetUniquePlayerName()
    {
        string name;
        bool isUnique = false;

        do
        {
            Console.WriteLine("Please enter your name:");
            name = Console.ReadLine().Trim();

            if (!IsPlayerNameExists())
            {
                isUnique = true;
            }
            else
            {
                Console.WriteLine("This name already exists. Please enter a different name.");
            }

        } while (!isUnique);

        return name;
    }
}
