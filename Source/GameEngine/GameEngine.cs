using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LudoGameEngine
{
    public class GameEngine
    {
        private int turn = 0;
        //private List<Player> players;
        //public List<Player> Players
        //{
        //    get { return players; }
        //    set { players = value; }
        //}
        public List<Player> players = new List<Player>();

        public List<Player> playerOrder = new List<Player>();


        public void AddPlayer(string name)
        {
            if (players.Count == 0)
                players.Add(new Player(Color.Red, name));

            else if (players.Count == 1)
                players.Add(new Player(Color.Blue, name));

            else if (players.Count == 2)
                players.Add(new Player(Color.Yellow, name));

            else if (players.Count == 3)
                players.Add(new Player(Color.Green, name));
        }

        public void StartNewGame()
        {
            ChooseStartingPlayer();

            while(true)
            {
                for (int i = 0; i < playerOrder.Count; i++)
                {
                    PlayerTurn(playerOrder[i]);
                }
                
            }
        }

        public void PlayerTurn(Player player)
        {
            Console.WriteLine($"{player.Name}! Press any key to roll the dice!");
            Console.ReadKey();
            var dice = RollDice();

            var selectedPiece = SelectGamePiece(player);

        }

        public GamePiece SelectGamePiece(Player player)
        {

            Console.WriteLine("Choose the gamepiece you want to move: ");

            for (int i = 0; i < player.GamePieces.Count; i++)
            {
                var gamePieceID = player.GamePieces[i].GamePieceID;
                var postition = player.GamePieces[i].position.BoardPosition;
                var positionType = player.GamePieces[i].position.positionType;
                Console.WriteLine($"GamePiece nr: {gamePieceID} at boardposition{positionType} {postition}");

            }

            var selectedGamePieceID = int.Parse(Console.ReadLine());

            var selectedGamePiece = player.GamePieces.Where(x => x.GamePieceID == selectedGamePieceID).FirstOrDefault();

            return selectedGamePiece;
        }

       

        public int RollDice()
        {
            Random rnd = new Random();
            return rnd.Next(1, 7);
        }

        public GamePiece MoveGamePiece(GamePiece gamePiece, int dice)
        {

            gamePiece.position.BoardPosition = gamePiece.position.BoardPosition + dice;

            gamePiece.StepCounter = gamePiece.StepCounter + dice;
            gamePiece = StepCounter(gamePiece, dice);

            return gamePiece;
        }

        public GamePiece StepCounter(GamePiece gamePiece, int dice)
        {
            if(gamePiece.StepCounter >= 40)
            {
                gamePiece.position.positionType = PositionType.InnerPath;

                var innerPathSteps = gamePiece.StepCounter - 40;

                gamePiece.StepCounter = gamePiece.StepCounter + innerPathSteps;
                gamePiece.position.BoardPosition = gamePiece.position.BoardPosition = innerPathSteps;

                if(gamePiece.StepCounter > 5)
                {
                    gamePiece.StepCounter -= 5;
                }
                else if(gamePiece.StepCounter == 5)
                {
                    players[0].Score += 1;
                }
                else
                {
                    gamePiece.StepCounter += dice;
                }

            }
                return gamePiece;
        }


        public void ChooseStartingPlayer()
        {
            var dice = 0;
            while (players.Count != playerOrder.Count)
            {
                foreach (Player player in players)
                {
                    dice = RollDice();
                    player.StartingDice = dice;
                    Console.WriteLine($"{player.Name} rolled {dice}.");
                }

                playerOrder = players.OrderByDescending(p => p.StartingDice).ToList();
                Console.WriteLine($"{playerOrder[0].Name} is the starting player!");
            }
        }

        public void MoveGamePieceToBoard()
        {

            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"{Enum.GetName(typeof(Color), i)} player rolls the dice.");
                var dice = RollDice();
                Console.WriteLine($"The dice rolled {dice}.");

                if (dice == 6 || dice == 1)
                {

                }
                else
                {
                    Console.WriteLine();
                }
            }

        }

     
    }
}
