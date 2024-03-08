using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace HangingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            HangmanGame hangmanGame = new HangmanGame();
            hangmanGame.StartGame();
        }
    }
}
