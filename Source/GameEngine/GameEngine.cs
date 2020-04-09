﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

namespace LudoGameEngine
{
    public class GameEngine
    {
        Game game = new Game();

        public List<Player> playerOrder = new List<Player>();


        public void AddPlayer(string name)
        {
            if (game.Players.Count == 0)
                game.Players.Add(new Player(Color.Red, name) { });

            else if (game.Players.Count == 1)
                game.Players.Add(new Player(Color.Blue, name));

            else if (game.Players.Count == 2)
                game.Players.Add(new Player(Color.Yellow, name));

            else if (game.Players.Count == 3)
                game.Players.Add(new Player(Color.Green, name));

            foreach (var player in game.Players)
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
                    gp.BoardPosition = 0;

                }
            }
            else if (player.Color == Color.Blue)
            {
                foreach (var gp in player.GamePieces)
                {
                    gp.BoardPosition = 10;

                }
            }
            else if (player.Color == Color.Yellow)
            {
                foreach (var gp in player.GamePieces)
                {
                    gp.BoardPosition = 20;

                }
            }
            else if (player.Color == Color.Green)
            {
                foreach (var gp in player.GamePieces)
                {
                    gp.BoardPosition = 30;

                }
            }

        }

        public List<GamePiece> GetValidPiecesToMove(Player player, int dice)
        {
            List<GamePiece> validToMovePieces = new List<GamePiece>();


            if (dice == 1 || dice == 6)
            {

                validToMovePieces.AddRange(player.GamePieces.Where(x => x.positionType != PositionType.FinishPosition).ToList());

            }
            else
            {
                validToMovePieces.AddRange(player.GamePieces.Where(x => x.positionType != PositionType.StartingPosition &&
                                                                    x.positionType != PositionType.FinishPosition).ToList());
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

            gamePiece.BoardPosition += dice;

            if (gamePiece.BoardPosition > 40)
            {
                gamePiece.BoardPosition -= 40;
            }

            gamePiece.StepCounter += dice;
            gamePiece = MoveGamePieceToInnerPath(gamePiece);

            if (gamePiece.positionType == PositionType.InnerPath)
            {
                if (gamePiece.StepCounter > 5)
                {
                    int result = gamePiece.StepCounter - 5;
                    result = 5 - result;
                    gamePiece.StepCounter = result;
                    gamePiece.BoardPosition = result;
                }
                else if (gamePiece.StepCounter == 5)
                {
                    gamePiece.positionType = PositionType.FinishPosition;
                    player.Score += 1;
                }
            }
            CheckGamePieceCollision(gamePiece.BoardPosition, gamePiece);
        }

        public void CheckGamePieceCollision(int currentPosition, GamePiece ourGamePiece)
        {
            
            foreach (Player player in game.Players)
            {
                foreach (GamePiece gamePiece in player.GamePieces)
                {
                    if (gamePiece.BoardPosition == currentPosition)
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
            gamePiece.positionType = PositionType.StartingPosition;

            if (player.Color == Color.Red)
            {
                gamePiece.BoardPosition = 0;
            }
            else if (player.Color == Color.Blue)
            {
                gamePiece.BoardPosition = 10;
            }
            else if (player.Color == Color.Yellow)
            {
                gamePiece.BoardPosition = 20;
            }
            else if (player.Color == Color.Green)
            {
                gamePiece.BoardPosition = 30;
            }
        }

        public GamePiece MoveGamePieceToInnerPath(GamePiece gamePiece)
        {
            if (gamePiece.StepCounter >= 40)
            {
                gamePiece.positionType = PositionType.InnerPath;

                gamePiece.StepCounter = gamePiece.StepCounter - 40;

                gamePiece.BoardPosition = gamePiece.StepCounter;

            }
            return gamePiece;
        }


        public List<Player> ChooseStartingPlayer()
        {
            var dice = 0;
            while (game.Players.Count != playerOrder.Count)
            {
                foreach (Player player in game.Players)
                {
                    dice = RollDice();
                    player.StartingDice = dice;
                }

                playerOrder = game.Players.OrderByDescending(p => p.StartingDice).ToList();
            }
            return playerOrder;
        }

        public void MoveGamePieceToBoard(GamePiece gp, int dice, Player player)
        {
            gp.positionType = PositionType.OuterPath;
            MoveGamePiece(gp, dice, player);
        }

        public Player CheckWin(Player player)
        {
            if (player.Score == 4)
                return player;
            else
                return null;
        }
    }
}
