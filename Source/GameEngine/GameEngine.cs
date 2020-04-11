using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using LudoGameEngine.Database;

namespace LudoGameEngine
{
    public class GameEngine
    {
        public GameData GameData = new GameData();
        public Game Game { get; set; }

        public List<Player> PlayerOrder = new List<Player>();

        public void CreateNewGame()
        {
            var game = GameData.NewGame();
            Game = game;
        }

        public void InitPlayersNPieces(Game game)
        {
            GameData.InitilizePLayersAndPieces(game);

        }

        public void LoadGame()
        {
            var game = GameData.LoadGame();
            Game = game;
        }

        public void UpdateGame(Game game)
        {
            GameData.UpdateGame(game);
        }

        public void SetGamePiecePlayerID(Player player)
        {
            foreach (GamePiece gamePiece in player.GamePieces)
            {
                gamePiece.PlayerID = player.ID;
            }
        }
        public void AddPlayer(string name)
        {
            if (Game.Players.Count == 0)
            {
                Game.Players.Add(new Player(Color.Red, name) { ID = 1 });
                SetGamePiecePlayerID(Game.Players[0]);
            }

            else if (Game.Players.Count == 1)
            {
                Game.Players.Add(new Player(Color.Blue, name) { ID = 2 });
                SetGamePiecePlayerID(Game.Players[1]);
            }

            else if (Game.Players.Count == 2)
            {
                Game.Players.Add(new Player(Color.Yellow, name) { ID = 3 });
                SetGamePiecePlayerID(Game.Players[2]);
            }

            else if (Game.Players.Count == 3)
            {
                Game.Players.Add(new Player(Color.Green, name) { ID = 4 });
                SetGamePiecePlayerID(Game.Players[3]);
            }

            foreach (var player in Game.Players)
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

                validToMovePieces.AddRange(player.GamePieces.Where(x => x.PositionType != PositionType.FinishPosition).ToList());

            }
            else
            {
                validToMovePieces.AddRange(player.GamePieces.Where(x => x.PositionType != PositionType.StartingPosition &&
                                                                    x.PositionType != PositionType.FinishPosition).ToList());
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

            if (gamePiece.PositionType == PositionType.InnerPath)
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
                    gamePiece.PositionType = PositionType.FinishPosition;
                    player.Score += 1;
                }
            }
            CheckGamePieceCollision(gamePiece.BoardPosition, gamePiece);
        }

        public void CheckGamePieceCollision(int currentPosition, GamePiece ourGamePiece)
        {
            
            foreach (Player player in Game.Players)
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
            gamePiece.PositionType = PositionType.StartingPosition;

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
                gamePiece.PositionType = PositionType.InnerPath;

                gamePiece.StepCounter = gamePiece.StepCounter - 40;

                gamePiece.BoardPosition = gamePiece.StepCounter;

            }
            return gamePiece;
        }


        public List<Player> ChooseStartingPlayer()
        {
            var dice = 0;
            while (Game.Players.Count != PlayerOrder.Count)
            {
                foreach (Player player in Game.Players)
                {
                    dice = RollDice();
                    player.StartingDice = dice;
                }

                PlayerOrder = Game.Players.OrderByDescending(p => p.StartingDice).ToList();
            }
            return PlayerOrder;
        }

        public void MoveGamePieceToBoard(GamePiece gp, int dice, Player player)
        {
            gp.PositionType = PositionType.OuterPath;
            MoveGamePiece(gp, dice, player);
        }

        public Player CheckWin(Player player)
        {
            if (player.Score == 4)
            {
                Game.HasFinished = true;
                return player;
            }
            else
                return null;
        }
    }
}
