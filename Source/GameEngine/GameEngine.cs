using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

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
                players.Add(new Player(Color.Red, name) { });

            else if (players.Count == 1)
                players.Add(new Player(Color.Blue, name));

            else if (players.Count == 2)
                players.Add(new Player(Color.Yellow, name));

            else if (players.Count == 3)
                players.Add(new Player(Color.Green, name));

            foreach (var player in players)
            {
                SetOuterPathStart(player);

            }
        }

        public void SetOuterPathStart(Player player)
        {

            if (player.Color == Color.Red)
            {
                foreach (var gp in player.GamePieces)
                {
                    gp.position.BoardPosition = 0;

                }
            }
            else if (player.Color == Color.Blue)
            {
                foreach (var gp in player.GamePieces)
                {
                    gp.position.BoardPosition = 10;

                }
            }
            else if (player.Color == Color.Yellow)
            {
                foreach (var gp in player.GamePieces)
                {
                    gp.position.BoardPosition = 20;

                }
            }
            else if (player.Color == Color.Green)
            {
                foreach (var gp in player.GamePieces)
                {
                    gp.position.BoardPosition = 30;

                }
            }

        }

        public List<GamePiece> GetValidPiecesToMove(Player player, int dice)
        {
            List<GamePiece> validToMovePieces = new List<GamePiece>();


            if (dice == 1 || dice == 6)
            {

                validToMovePieces.AddRange(player.GamePieces);

            }
            else
            {
                validToMovePieces.AddRange(player.GamePieces.Where(x => x.position.positionType != PositionType.StartingPosition &&
                                                                    x.position.positionType != PositionType.FinishPosition).ToList());
            }

            return validToMovePieces;

        }

       
        public int RollDice()
        {
            Random rnd = new Random();
            return rnd.Next(1, 7);
        }

        public void MoveGamePiece(GamePiece gamePiece, int dice, Player player)
        {

            gamePiece.position.BoardPosition += dice;

            if (gamePiece.position.BoardPosition > 40)
            {
                gamePiece.position.BoardPosition -= 40;
            }

            gamePiece.StepCounter += dice;
            gamePiece = MoveGamePieceToInnerPath(gamePiece);

            if (gamePiece.position.positionType == PositionType.InnerPath)
            {
                if (gamePiece.StepCounter > 5)
                {
                    gamePiece.StepCounter -= 5;
                }
                else if (gamePiece.StepCounter == 5)
                {
                    gamePiece.position.positionType = PositionType.FinishPosition;
                    player.Score += 1;
                }
                else
                {
                    gamePiece.StepCounter += dice;
                }
            }
            CheckGamePieceCollision(gamePiece.position.BoardPosition, gamePiece);
        }

        public void CheckGamePieceCollision(int currentPosition, GamePiece ourGamePiece)
        {
            
            foreach (Player player in players)
            {
                foreach (GamePiece gamePiece in player.GamePieces)
                {
                    if (gamePiece.position.BoardPosition == currentPosition)
                    {
                        var gamePieceOnPosition = gamePiece;

                        if (gamePieceOnPosition != ourGamePiece)
                        {
                            PushGamePiece(gamePieceOnPosition, player);
                        }
                    }
                }
            }
        }

        public void PushGamePiece(GamePiece gamePiece, Player player)
        {
            gamePiece.position.positionType = PositionType.StartingPosition;

            if (player.Color == Color.Red)
            {
                gamePiece.position.BoardPosition = 0;
            }
            else if (player.Color == Color.Blue)
            {
                gamePiece.position.BoardPosition = 10;
            }
            else if (player.Color == Color.Yellow)
            {
                gamePiece.position.BoardPosition = 20;
            }
            else if (player.Color == Color.Green)
            {
                gamePiece.position.BoardPosition = 30;
            }
        }

        public GamePiece MoveGamePieceToInnerPath(GamePiece gamePiece)
        {
            if (gamePiece.StepCounter >= 40)
            {
                gamePiece.position.positionType = PositionType.InnerPath;

                var innerPathSteps = gamePiece.StepCounter - 40;

                gamePiece.StepCounter += innerPathSteps;
                gamePiece.position.BoardPosition = innerPathSteps;

               

            }
            return gamePiece;
        }


        public List<Player> ChooseStartingPlayer()
        {
            var dice = 0;
            while (players.Count != playerOrder.Count)
            {
                foreach (Player player in players)
                {
                    dice = RollDice();
                    player.StartingDice = dice;
                    //Console.WriteLine($"{player.Name} rolled {dice}.");
                }

                playerOrder = players.OrderByDescending(p => p.StartingDice).ToList();
            }
            return playerOrder;
        }

        public void MoveGamePieceToBoard(GamePiece gp, int dice, Player player)
        {
            gp.position.positionType = PositionType.OuterPath;
            MoveGamePiece(gp, dice, player);
        }

    }
}
