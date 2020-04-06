using LudoGameEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGame
{
    public class Menu
    {
        GameEngine GameEngine;

        public Menu()
        {
            GameEngine = new GameEngine();
        }

        public void MenuHeader()
        {
            Console.Title = "LudoGame";
            Console.ForegroundColor = ConsoleColor.Yellow;
            var header = new[]
            {
                @" _    _      _                            _          _               _",
                @"| |  | |    | |                          | |        | |             | |",
                @"| |  | | ___| | ___ ___  _ __ ___   ___  | |_ ___   | |    _   _  __| | ___",
                @"| |/\| |/ _ \ |/ __/ _ \| '_ ` _ \ / _ \ | __/ _ \  | |   | | | |/ _` |/ _ \ ",
                @"\  /\  /  __/ | (_| (_) | | | | | |  __/ | || (_) | | |___| |_| | (_| | (_) |",
                @" \/  \/ \___|_|\___\___/|_| |_| |_|\___|  \__\___/  \_____/\__,_|\__,_|\___/",

        };

            foreach (var line in header)
            {
                Console.WriteLine(line);
            }
        }

        public void MenuOptions()
        {

            Console.WriteLine("Options:");
            Console.WriteLine("(1) Start new game (2) Continue old game (3) Check game history");

            var optionChoosen = Console.ReadKey().Key;

            switch (optionChoosen)
            {
                case ConsoleKey.D1:
                    StartLobby();
                    break;

                case ConsoleKey.D2:
                    //GameEngine.LoadGame();
                    break;

                case ConsoleKey.D3:
                    //GameEngine.CheckGameHistory();
                    break;

                default:
                    break;
            }

        }
        public void StartLobby()
        {
            Console.WriteLine("How many players?");
            int playerAmount = int.Parse(Console.ReadLine());
            for (int i = 0; i < playerAmount; i++)
            {
                Console.WriteLine($"{Enum.GetName(typeof(Color), i)} player, choose your name: ");
                var name = Console.ReadLine();
                GameEngine.AddPlayer(name);
            }
            Console.WriteLine("All players are reday, press any key to start game");
            Console.ReadKey();
            GameEngine.StartNewGame();

        }


    }
}
