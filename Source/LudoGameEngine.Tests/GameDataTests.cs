using LudoGameEngine.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;


namespace LudoGameEngine.Tests
{
    public class GameDataTest
    {

        [Fact]
        public void StoreNewGame()
        {
            //arrange
            LudoContext context = new LudoContext();
            Game game = new Game();
            context = new LudoContext();
            game.HasFinished = true;
            game.WinnerPlayerName = "Osborn";

            //act
            context.Game.Add(game);
            context.SaveChanges();

            //assert
            Assert.Equal("Osborn", context.Game.Where()
        }
    }
}
