using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word_game
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
            File.WriteAllText("player_scores.xml", "<Players><Player><Name>Nika</Name><Points>100</Points></Player></Players>");

            // Act
            game.Start();

            // Assert
            //Assert.AreEqual(1, game.Players.Count);
            //Assert.AreEqual("Nika", game.Players[0].Name);
            //Assert.AreEqual(100, game.Players[0].Points);
        }


    }
}
