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
            

            TryPlayerAmountInputAgain:

            Console.WriteLine("Type in how many players and press enter");

            int playerAmount;
            bool succeeded;
            succeeded = int.TryParse(Console.ReadLine(), out playerAmount);

            if (succeeded == false)
            {
                Console.WriteLine("Your input was not correct, please try again");
                goto TryPlayerAmountInputAgain;
            }

            while (playerAmount < 2 || playerAmount > 4)
            {
                Console.WriteLine("You have to be at least 2 players and maximum 4 players.");
                Console.WriteLine("How many players?");
                playerAmount = int.Parse(Console.ReadLine());
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

            GameRun(playerOrder);

            Console.WriteLine("Game done!");
        }
        private void GameRun(List<Player> playerOrder)
        {
            Player winPlayer = null;
            while (winPlayer == null)
            {
                for (int i = 0; i < playerOrder.Count; i++)
                {
                    winPlayer = PlayerTurn(playerOrder[i]);
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
                Console.WriteLine($"GamePiece nr: {gp.GamePieceID} is at boardposition:" +
                    $" {gp.position.positionType} and is at {gp.position.BoardPosition}");
            }

            if (validToMovePieces.Count == 0)
            {
                Console.WriteLine("Sorry, you have no valid gamepieces to move, try again next turn");
                return null;
            }

            
            TryInputAgain:
            Console.WriteLine("Type in GamePiece number and press enter");

            int selectedGamePieceID;
            bool succeeded;
            succeeded = int.TryParse(Console.ReadLine(), out selectedGamePieceID);
            
            if (succeeded == false)
            {
                Console.WriteLine("Your input was not correct, please try again");
                goto TryInputAgain;   
            }

            GamePiece selectedGamePiece = null;

            for (int i = 0; i < validToMovePieces.Count +1 ; i++)
            {
                try
                {
                if(selectedGamePieceID == validToMovePieces[i].GamePieceID)
                {
                    selectedGamePiece = validToMovePieces.Where(x => x.GamePieceID == selectedGamePieceID).FirstOrDefault();
                    break;
                }      

                }
                catch (Exception)
                {
                    Console.WriteLine("That gamepiece does not exist or is not valid to move right now");
                    goto TryInputAgain;
                }
            }
            
            
            

            if (selectedGamePiece.position.positionType == PositionType.StartingPosition)
            {
                GameEngine.MoveGamePieceToBoard(selectedGamePiece, dice, player);
            }
            else
            {
                GameEngine.MoveGamePiece(selectedGamePiece, dice, player);
            }

            //check if player won with loop of players and their score
            if (player.Score == 4)
            {
                return player;
            }


            Console.WriteLine($"Your piece is now at: {selectedGamePiece.position.BoardPosition} on the {selectedGamePiece.position.positionType}" +
                $" and has taken: {selectedGamePiece.StepCounter} steps");

            return null;
        }

        public bool CheckUserInput(string input)
        {
            bool result;
            var charInput = Convert.ToChar(input);
            result = Char.IsDigit(charInput);

            return result;
        }
    }
}
