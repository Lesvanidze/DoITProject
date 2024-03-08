using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Number_guessing_game
{
    [TestFixture]
    public class GameTests
    {
        private Game game;

        [SetUp]
        public void Setup()
        {
            game = new Game();
        }

        [Test]
        public void LoadPlayers_FileExists_LoadsPlayers()
        {
            // Arrange
            File.WriteAllText("player_scores.csv", "Nini,100\n");

            // Act
            game.Start();

            // Assert
            //Assert.AreEqual(1, game.Players.Count);
            //Assert.AreEqual("Nini", game.Players[0].Name);
            //Assert.AreEqual(100, game.Players[0].Points);
        }

        [Test]
        public void SavePlayerScore_NewPlayer_SavesPlayer()
        {
            // Arrange
            game.Players = new List<Player>();
            string playerName = "Nini";
            int points = 50;

            // Act
            _ = game.Save;
        }
    }
}
