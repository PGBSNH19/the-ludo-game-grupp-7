using LudoGameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
                    LoadGame();
                    break;

                case ConsoleKey.D3:
                    //GameEngine.CheckGameHistory();
                    break;

                default:
                    break;
            }

        }

        private void LoadGame()
        {
            Console.WriteLine("Checking for current game");
            Thread.Sleep(400);
            Console.Write(" . ");
            Thread.Sleep(400);
            Console.Write(" . ");
            Thread.Sleep(400);
            Console.Write(" . ");
            Thread.Sleep(400);
            Console.Write(" . ");
            Thread.Sleep(400);
            Console.Write(" . ");

            try
            {
                GameEngine.LoadGame();
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong");
            }

            if (GameEngine.Game != null)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("No game found");
            }

            GameRun(GameEngine.Game.Players);

            GameEngine.GameData.FinishGame(GameEngine.Game);

            Console.WriteLine("Game done!");
        }

        public void StartLobby()
        {
            GameEngine.CreateNewGame();
            Console.WriteLine("Type in how many players and press enter");

            string userInput = Console.ReadLine();
            int playerAmount = CheckUserInput(userInput);

            while (playerAmount < 2 || playerAmount > 4)
            {
                Console.WriteLine("You have to be at least 2 players and maximum 4 players.");
                Console.WriteLine("How many players?");
                userInput = Console.ReadLine();
                playerAmount = CheckUserInput(userInput);
            }

            for (int i = 0; i < playerAmount; i++)
            {
                Console.WriteLine($"{Enum.GetName(typeof(Color), i)} player, choose your name: ");
                var name = Console.ReadLine();
                GameEngine.AddPlayer(name);
            }


            Console.WriteLine("All players are ready, press any key to start game");
            Console.ReadKey();
            var playerOrder = GameEngine.ChooseStartingPlayer();
            Console.WriteLine($"{playerOrder[0].Name} rolled the highest and is the starting player!");

            GameEngine.GameData.InitilizePLayersAndPieces(GameEngine.Game);

            GameRun(playerOrder);

            GameEngine.GameData.FinishGame(GameEngine.Game);

            Console.WriteLine("Game done!");
        }
        public void GameRun(List<Player> playerOrder)
        {
            Player winPlayer = null;
            while (winPlayer == null)
            {
                for (int i = 0; i < playerOrder.Count; i++)
                {
                    //if its my turn
                    if(playerOrder[0].MyTurn == true)
                    {

                    }
                    playerOrder[i].MyTurn = true;
                    winPlayer = PlayerTurn(playerOrder[i]);
                    playerOrder[i].MyTurn = false;
                    if (winPlayer != null)
                        break;
                }
            }
            Console.WriteLine($"Congratulations, {winPlayer.Name}! You won!");
        }

        public Player PlayerTurn(Player player)
        {
            Console.WriteLine($"{player.Name}, press any key to roll the dice.");
            Console.ReadKey();
            var dice = GameEngine.RollDice();
            Console.WriteLine($"The dice rolled {dice}.");
            Thread.Sleep(500);

            var validToMovePieces = GameEngine.GetValidPiecesToMove(player, dice);
            foreach (var gp in validToMovePieces)
            {
                Console.WriteLine($"GamePiece nr: {gp.PlayerGamePiece} is on the" +
                    $" {gp.PositionType} at position: {gp.BoardPosition} and has taken {gp.StepCounter} steps");
            }

            if (validToMovePieces.Count == 0)
            {
                Console.WriteLine("Sorry, you have no valid gamepieces to move, try again next turn \n");
                return null;
            }


        TryAgain:
            Console.WriteLine("Type in GamePiece number and press enter");

            int selectedPlayerGamePiece;
            string userInput = Console.ReadLine();
            selectedPlayerGamePiece = CheckUserInput(userInput);

            GamePiece selectedGamePiece = null;

            for (int i = 0; i < validToMovePieces.Count + 1; i++)
            {
                try
                {
                    if (selectedPlayerGamePiece == validToMovePieces[i].PlayerGamePiece)
                    {
                        selectedGamePiece = validToMovePieces.Where(x => x.PlayerGamePiece == selectedPlayerGamePiece).FirstOrDefault();
                        break;
                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("That gamepiece does not exist or is not valid to move right now \n");
                    goto TryAgain;
                }
            }


            if (selectedGamePiece.PositionType == PositionType.StartingPosition)
            {
                GameEngine.MoveGamePieceToBoard(selectedGamePiece, dice, player);
            }
            else
            {
                GameEngine.MoveGamePiece(selectedGamePiece, dice, player);
            }

            player = GameEngine.CheckWin(player);
            if (player != null)
                return player;

            Console.WriteLine($"Your piece is now at position: {selectedGamePiece.BoardPosition} on the {selectedGamePiece.PositionType}" +
                $" and has taken: {selectedGamePiece.StepCounter} steps \n");

            
            GameEngine.GameData.UpdateGame(GameEngine.Game);

            return null;
        }


        public static int CheckUserInput(string input)
        {
            int output;

            while (!int.TryParse(input, out output))
            {
                Console.WriteLine("Please enter a number.");
                input = Console.ReadLine();
            }
            return output;
        }
    }
}
