using System;
using System.Collections.Generic;
using Xunit;
using LudoGameEngine;
using System.Linq;
using LudoGame;
using LudoGameEngine.Database;

namespace LudoGameEngine.Tests
{
    public class GameEngineTests
    {
        //[Fact]
        //public void GamePiece_MoveGamePiece_Move3Positions()
        //{
        //    //arrange
        //    //GamePiece gp = new GamePiece();
        //    //int dice = 3;
        //    //act
        //    //assert



        //}
        [Fact]
        public void Dice_RollDice_ValidInt_Ture_IntBtw1and6()
        {
            //arrange
            GameEngine dice = new GameEngine();
            var die = dice.RollDice();
            //act
            //Assert
            Assert.InRange(die, 1, 6);

        }

        [Fact]
        public void CreatePlayer_GenerateGamePieces_AddToList()
        {
            //arrange
            Player a = new Player(Color.Red, "Per");

            //act

            //assert
            Assert.Equal(4, a.GamePieces.Count);
            Assert.NotEmpty(a.GamePieces);

        }


        [Fact]
        public void ChooseStartingPlayer_PopulatePlayerOrderList_Sucess()
        {
            //arrange
            GameEngine gameEngine = new GameEngine();
            Game game = new Game();

            gameEngine.AddPlayer("Olle");
            gameEngine.AddPlayer("Per");
            gameEngine.AddPlayer("Lasse");
            gameEngine.AddPlayer("Nisse");


            //act
            gameEngine.ChooseStartingPlayer();
            var expectedList = game.Players.OrderByDescending(x => x.StartingDice).ToList();

            //assert
            Assert.NotEqual(game.Players, gameEngine.PlayerOrder);
            Assert.True(expectedList.SequenceEqual(gameEngine.PlayerOrder));
        }

        [Fact]
        public void SetOuterPathStart_SetValidStartPosition_()
        {
            //arrange
            GameEngine gameEngine = new GameEngine();
            gameEngine.CreateNewGame();


            //act
            gameEngine.AddPlayer("Ralph");
            gameEngine.AddPlayer("Dolph Lundgren");

            //assert
            Assert.Equal(0, gameEngine.Game.Players[0].GamePieces[0].BoardPosition);
            Assert.Equal(10, gameEngine.Game.Players[1].GamePieces[0].BoardPosition);

        }

        [Fact]

        public void FinishGame_APlayerScore4Points_GameIsFinishedTrue()
        {
            GameEngine gameEngine = new GameEngine();
            GameData gameData = new GameData();
            Menu menu = new Menu();

            gameEngine.CreateNewGame();
            gameEngine.AddPlayer("Kalle");
            gameEngine.AddPlayer("Palle");

            gameEngine.Game.Players[0].Score = 4;


            gameEngine.CheckWin(gameEngine.Game.Players[0]);

            Assert.True(gameEngine.Game.HasFinished);
        }

        [Fact]
        public void MoveGamePiece_PositionBeyondFourty_ResetBoardPosition()
        {
            //arrange
            GameEngine gameEngine = new GameEngine();
            Game game = new Game();
            Player player = new Player(Color.Green, "Lasse");
            player.GamePieces[0].PositionType = PositionType.OuterPath;
            player.GamePieces[0].BoardPosition = 39;
            var dice = 6;

            //act
            gameEngine.MoveGamePiece(player.GamePieces[0], dice, player);

            //assert
            Assert.Equal(5, player.GamePieces[0].BoardPosition);
        }

        [Fact]
        public void PushGamePiece_GamePiecesOnSamePosition_GamePieceGetsRepositionedToStartingPosition()
        {
            //arrange
            GameEngine gameEngine = new GameEngine();
            Game game = new Game();
            game.Players.Add(new Player(Color.Blue ,"Kålle"));
            game.Players.Add(new Player(Color.Green ,"Ada"));
            game.Players[0].GamePieces[0].PositionType = PositionType.OuterPath;
            game.Players[0].GamePieces[0].BoardPosition = 38;
            game.Players[1].GamePieces[0].PositionType = PositionType.OuterPath;
            game.Players[1].GamePieces[0].BoardPosition = 39;

            //act
            gameEngine.MoveGamePiece(game.Players[0].GamePieces[0], 1, game.Players[0]);

            //assert
            Assert.Equal(30, game.Players[1].GamePieces[0].BoardPosition);
            Assert.NotEqual(39, game.Players[1].GamePieces[0].BoardPosition);
        }

        [Fact]
        public void PushGamePiece_TwoSameColoredPiecesOnSamePosition_NoPush()
        {
            
            //arrange
            GameEngine gameEngine = new GameEngine();
            Game game = new Game();
            game.Players.Add(new Player(Color.Red, "Nils"));
            game.Players[0].GamePieces[0].PositionType = PositionType.OuterPath;
            game.Players[0].GamePieces[0].BoardPosition = 38;
            game.Players[0].GamePieces[1].PositionType = PositionType.OuterPath;
            game.Players[0].GamePieces[1].BoardPosition = 37;

            //act
            gameEngine.MoveGamePiece(game.Players[0].GamePieces[1], 1, game.Players[0]);

            //assert
            Assert.Equal(38, game.Players[0].GamePieces[1].BoardPosition);
        }


        [Fact]
        public void MoveGamePiece_GamePieceMoves40Steps_GamePieceMovesToInnerPath()
        {
            //arrange
            GameEngine gameEngine = new GameEngine();
            Game game = new Game();
            game.Players.Add(new Player(Color.Red, "Nils"));
            game.Players[0].GamePieces[0].PositionType = PositionType.OuterPath;
            game.Players[0].GamePieces[0].BoardPosition = 38;
            game.Players[0].GamePieces[0].StepCounter = 38;

            //act
            gameEngine.MoveGamePiece(game.Players[0].GamePieces[0], 4, game.Players[0]);

            //assert
            Assert.Equal(PositionType.InnerPath, game.Players[0].GamePieces[0].PositionType);
            Assert.Equal(2, game.Players[0].GamePieces[0].BoardPosition);
            Assert.Equal(2, game.Players[0].GamePieces[0].StepCounter);

        }

        [Fact]
        public void InnerPathMove_MoveToFar_GamePieceStepBack()
        {
            //arrange
            GameEngine gameEngine = new GameEngine();
            Game game = new Game();
            game.Players.Add(new Player(Color.Red, "Nils"));
            game.Players[0].GamePieces[0].PositionType = PositionType.InnerPath;
            game.Players[0].GamePieces[0].BoardPosition = 2;
            game.Players[0].GamePieces[0].StepCounter = 2;

            //act
            gameEngine.MoveGamePiece(game.Players[0].GamePieces[0], 4, game.Players[0]);

            //assert
            Assert.Equal(4, game.Players[0].GamePieces[0].BoardPosition);
        }

        [Fact]
        public void MoveLastPeiceToFinishPosition_PlayerScore4Points_PlayerWins()
        {
            //arrange
            GameEngine gameEngine = new GameEngine();
            Game game = new Game();
            Menu menu = new Menu();
            game.Players.Add(new Player(Color.Red, "Nils"));
            game.Players[0].GamePieces[0].PositionType = PositionType.InnerPath;
            game.Players[0].GamePieces[0].BoardPosition = 2;
            game.Players[0].GamePieces[0].StepCounter = 2;
            game.Players[0].Score = 3;

            //act
            gameEngine.MoveGamePiece(game.Players[0].GamePieces[0], 3, game.Players[0]);
            var winnerPlayer = gameEngine.CheckWin(game.Players[0]);

            //assert
            Assert.Equal(4, game.Players[0].Score);
            Assert.Equal(winnerPlayer, game.Players[0]);

        }
        [Fact]
        public void MoveGamePiece_AfterPieceGetsToFinish_PieceGetsRemoved()
        {
            //arrange
            GameEngine gameEngine = new GameEngine();
            Game game = new Game();
            Menu menu = new Menu();
            game.Players.Add(new Player(Color.Red, "Nils"));
            game.Players[0].GamePieces[0].PositionType = PositionType.InnerPath;
            game.Players[0].GamePieces[0].BoardPosition = 2;
            game.Players[0].GamePieces[0].StepCounter = 2;
            game.Players[0].GamePieces[1].PositionType = PositionType.InnerPath;
            game.Players[0].GamePieces[1].BoardPosition = 1;
            game.Players[0].GamePieces[2].PositionType = PositionType.OuterPath;
            game.Players[0].GamePieces[2].BoardPosition = 2;
            game.Players[0].GamePieces[3].PositionType = PositionType.StartingPosition;
            game.Players[0].GamePieces[3].BoardPosition = 1;

            //act
            gameEngine.MoveGamePiece(game.Players[0].GamePieces[0], 3, game.Players[0]);
            var validpiecestomove = gameEngine.GetValidPiecesToMove(game.Players[0], 1);

            //assert
            Assert.Equal(PositionType.FinishPosition, game.Players[0].GamePieces[0].PositionType);
            Assert.Equal(3, validpiecestomove.Count);
        }

        //[Fact]
        //public void SelectGamePiece_LetPlayerDecidePiece_SelectedPieceReturned()
        //{
        //    //arrange
        //    GameEngine gameEngine = new GameEngine();
        //    Player player = new Player(Color.Blue, "Nisse");

        //    //act
        //    gameEngine.SelectGamePiece(player);

        //    //
        //}
    }
}
