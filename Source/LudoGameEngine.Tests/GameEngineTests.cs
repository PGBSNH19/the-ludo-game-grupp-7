using System;
using System.Collections.Generic;
using Xunit;
using LudoGameEngine;
using System.Linq;

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
            Game dice = new Game();
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
        public void ChooseStartingPlayer_PopulatePlayerOrderList_Sucess ()
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
