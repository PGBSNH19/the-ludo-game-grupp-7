using System;
using System.Collections.Generic;
using Xunit;
using LudoGameEngine;

namespace LudoGameEngine.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void GamePiece_MoveGamePiece_Move3Positions()
        {
            //arrange
            //GamePiece gp = new GamePiece();
            //int dice = 3;
            //act
            //assert



        }
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
            Player a = new Player(Color.red, "Per");

            //act

            //assert
            Assert.Equal(4, a.GamePieces.Count);
            Assert.NotEmpty(a.GamePieces);

        }

       


    }
}
