using LudoGameEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGame
{
    public class Menu
    {
        GameEngine _gameEngine;
        public Menu(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        public void MenuHeader()
        {
            Console.Title = "LudoGame";
            Console.ForegroundColor = ConsoleColor.Yellow;
            var header = new[]
            {
             @"_    _      _                            _          _               _
| |  | |    | |                          | |        | |             | |
| |  | | ___| | ___ ___  _ __ ___   ___  | |_ ___   | |    _   _  __| | ___
| |/\| |/ _ \ |/ __/ _ \| '_ ` _ \ / _ \ | __/ _ \  | |   | | | |/ _` |/ _ \ 
\  /\  /  __/ | (_| (_) | | | | | |  __/ | || (_) | | |___| |_| | (_| | (_) |
 \/  \/ \___|_|\___\___/|_| |_| |_|\___|  \__\___/  \_____/\__,_|\__,_|\___/

                                                                             __|\__|",
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

            //switch (optionChoosen)
            //{
            //    case ConsoleKey.D1:
            //        _gameEngine.StartNewGame();
            //        break;

            //    case ConsoleKey.D2:
            //        _gameEngine.LoadGame();
            //        break;

            //    case ConsoleKey.D3:
            //        _gameEngine.CheckGameHistory();
            //        break;

            //    default:
            //        break;
            //}

        }


    }
}
