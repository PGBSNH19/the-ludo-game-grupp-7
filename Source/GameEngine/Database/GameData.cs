using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LudoGameEngine.Database
{
    public class GameData
    {
        private LudoContext _ludoContext = new LudoContext();

        public Game NewGame()
        {

            _ludoContext.GamePiece.RemoveRange(_ludoContext.GamePiece.ToList());
            _ludoContext.Player.RemoveRange(_ludoContext.Player.ToList());
            _ludoContext.Game.RemoveRange(_ludoContext.Game.ToList());

            Game game = new Game() { EndTimeOfGame = DateTime.Now };
            _ludoContext.Game.Add(game);
            _ludoContext.SaveChanges();

            return game;
        }

        public void UpdateGame(Game game)
        {
            _ludoContext.Game.Update(game);
            _ludoContext.SaveChanges();
        }

        //save restore delete
    }
}
