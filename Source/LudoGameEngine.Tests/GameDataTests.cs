using LudoGameEngine.Database;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace LudoGameEngine.Tests
{
    public class GameDataTest
    {

        [Fact]
        public void StoreNewGame()
        {
            LudoContext context = new LudoContext();
            var game = new Game()
            {
                Players = new List<Player>() { new Player() { Name = "Kålle", Color = Color.Blue } }
            };

            context.Game.Add(game);

            context.SaveChanges();
            context = new LudoContext();

            game.HasFinished = false;
            game.Players[0].Score += 1;

            context.Game.Update(game);

            context.SaveChanges();
        }

       
    }
}
