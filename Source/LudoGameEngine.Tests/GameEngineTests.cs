using System;
using System.Collections.Generic;
using Xunit;
using LudoGameEngine;
using System.Linq;
using LudoGame;

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

            gameEngine.AddPlayer("Olle");
            gameEngine.AddPlayer("Per");
            gameEngine.AddPlayer("Lasse");
            gameEngine.AddPlayer("Nisse");


            //act
            gameEngine.ChooseStartingPlayer();
            var expectedList = gameEngine.players.OrderByDescending(x => x.StartingDice).ToList();

            //assert
            Assert.NotEqual(gameEngine.players, gameEngine.playerOrder);
            Assert.True(expectedList.SequenceEqual(gameEngine.playerOrder));
        }

        [Fact]
        public void SetOuterPathStart_SetValidStartPosition()
        {
            //arrange
            GameEngine gameEngine = new GameEngine();


            //act
            gameEngine.AddPlayer("Ralph");
            gameEngine.AddPlayer("Dolph Lundgren");

            //assert
            Assert.Equal(0, gameEngine.players[0].GamePieces[0].position.BoardPosition);
            Assert.Equal(10, gameEngine.players[1].GamePieces[0].position.BoardPosition);

        }


        [Fact]
        public void MoveGamePiece_PositionBeyondFourty_ResetBoardPosition()
        {
            //arrange
            GameEngine gameEngine = new GameEngine();
            Player player = new Player(Color.Green, "Lasse");
            player.GamePieces[0].position.positionType = PositionType.OuterPath;
            player.GamePieces[0].position.BoardPosition = 39;
            var dice = 6;

            //act
            gameEngine.MoveGamePiece(player.GamePieces[0], dice, player);

            //assert
            Assert.Equal(5, player.GamePieces[0].position.BoardPosition);
        }

        [Fact]
        public void PushGamePiece_GamePiecesOnSamePosition_GamePieceGetsRepositionedToStartingPosition()
        {
            //arrange
            GameEngine gameEngine = new GameEngine();
            gameEngine.players.Add(new Player(Color.Blue ,"Kålle"));
            gameEngine.players.Add(new Player(Color.Green ,"Ada"));
            gameEngine.players[0].GamePieces[0].position.positionType = PositionType.OuterPath;
            gameEngine.players[0].GamePieces[0].position.BoardPosition = 38;
            gameEngine.players[1].GamePieces[0].position.positionType = PositionType.OuterPath;
            gameEngine.players[1].GamePieces[0].position.BoardPosition = 39;

            //act
            gameEngine.MoveGamePiece(gameEngine.players[0].GamePieces[0], 1, gameEngine.players[0]);

            //assert
            Assert.Equal(30, gameEngine.players[1].GamePieces[0].position.BoardPosition);
            Assert.NotEqual(39, gameEngine.players[1].GamePieces[0].position.BoardPosition);
        }

        [Fact]
        public void PushGamePiece_TwoSameColoredPiecesOnSamePosition_NoPush()
        {
            
            //arrange
            GameEngine gameEngine = new GameEngine();
            gameEngine.players.Add(new Player(Color.Red, "Nils"));
            gameEngine.players[0].GamePieces[0].position.positionType = PositionType.OuterPath;
            gameEngine.players[0].GamePieces[0].position.BoardPosition = 38;
            gameEngine.players[0].GamePieces[1].position.positionType = PositionType.OuterPath;
            gameEngine.players[0].GamePieces[1].position.BoardPosition = 37;

            //act
            gameEngine.MoveGamePiece(gameEngine.players[0].GamePieces[1], 1, gameEngine.players[0]);

            //assert
            Assert.Equal(38, gameEngine.players[0].GamePieces[1].position.BoardPosition);
        }


        [Fact]
        public void MoveGamePiece_GamePieceMoves40Steps_GamePieceMovesToInnerPath()
        {
            //arrange
            GameEngine gameEngine = new GameEngine();
            gameEngine.players.Add(new Player(Color.Red, "Nils"));
            gameEngine.players[0].GamePieces[0].position.positionType = PositionType.OuterPath;
            gameEngine.players[0].GamePieces[0].position.BoardPosition = 38;
            gameEngine.players[0].GamePieces[0].StepCounter = 38;

            //act
            gameEngine.MoveGamePiece(gameEngine.players[0].GamePieces[0], 4, gameEngine.players[0]);

            //assert
            Assert.Equal(PositionType.InnerPath, gameEngine.players[0].GamePieces[0].position.positionType);
            Assert.Equal(2, gameEngine.players[0].GamePieces[0].position.BoardPosition);
            Assert.Equal(2, gameEngine.players[0].GamePieces[0].StepCounter);

        }

        [Fact]
        public void InnerPathMove_MoveToFar_GamePieceStepBack()
        {
            //arrange
            GameEngine gameEngine = new GameEngine();
            gameEngine.players.Add(new Player(Color.Red, "Nils"));
            gameEngine.players[0].GamePieces[0].position.positionType = PositionType.InnerPath;
            gameEngine.players[0].GamePieces[0].position.BoardPosition = 2;
            gameEngine.players[0].GamePieces[0].StepCounter = 2;

            //act
            gameEngine.MoveGamePiece(gameEngine.players[0].GamePieces[0], 4, gameEngine.players[0]);

            //assert
            Assert.Equal(4, gameEngine.players[0].GamePieces[0].position.BoardPosition);
        }

        [Fact]
        public void MoveLastPeiceToFinishPosition_PlayerScore4Points_PlayerWins()
        {
            //arrange
            GameEngine gameEngine = new GameEngine();
            Menu menu = new Menu();
            gameEngine.players.Add(new Player(Color.Red, "Nils"));
            gameEngine.players[0].GamePieces[0].position.positionType = PositionType.InnerPath;
            gameEngine.players[0].GamePieces[0].position.BoardPosition = 2;
            gameEngine.players[0].GamePieces[0].StepCounter = 2;
            gameEngine.players[0].Score = 3;

            //act
            gameEngine.MoveGamePiece(gameEngine.players[0].GamePieces[0], 3, gameEngine.players[0]);
            var winnerPlayer = gameEngine.CheckWin(gameEngine.players[0]);

            //assert
            Assert.Equal(4, gameEngine.players[0].Score);
            Assert.Equal(winnerPlayer, gameEngine.players[0]);

        }
        [Fact]
        public void MoveGamePiece_AfterPieceGetsToFinish_PieceGetsRemoved()
        {
            //arrange
            GameEngine gameEngine = new GameEngine();
            Menu menu = new Menu();
            gameEngine.players.Add(new Player(Color.Red, "Nils"));
            gameEngine.players[0].GamePieces[0].position.positionType = PositionType.InnerPath;
            gameEngine.players[0].GamePieces[0].position.BoardPosition = 2;
            gameEngine.players[0].GamePieces[0].StepCounter = 2;
            gameEngine.players[0].GamePieces[1].position.positionType = PositionType.InnerPath;
            gameEngine.players[0].GamePieces[1].position.BoardPosition = 1;
            gameEngine.players[0].GamePieces[2].position.positionType = PositionType.OuterPath;
            gameEngine.players[0].GamePieces[2].position.BoardPosition = 2;
            gameEngine.players[0].GamePieces[3].position.positionType = PositionType.StartingPosition;
            gameEngine.players[0].GamePieces[3].position.BoardPosition = 1;

            //act
            gameEngine.MoveGamePiece(gameEngine.players[0].GamePieces[0], 3, gameEngine.players[0]);
            var validpiecestomove = gameEngine.GetValidPiecesToMove(gameEngine.players[0], 1);

            //assert
            Assert.Equal(PositionType.FinishPosition, gameEngine.players[0].GamePieces[0].position.positionType);
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
