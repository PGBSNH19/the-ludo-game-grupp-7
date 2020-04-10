﻿using Microsoft.EntityFrameworkCore;
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
           
            Game game = new Game();
            _ludoContext.Game.Add(game);
            _ludoContext.Player.AddRange(game.Players);

            for (int i = 0; i < game.Players.Count; i++)
            {
                _ludoContext.GamePiece.AddRange(game.Players[i].GamePieces);
            }
            _ludoContext.SaveChanges();

            return game;
        }

      

        public void UpdateGame(Game game)
        {
            _ludoContext.Game.Update(game);
            _ludoContext.Player.UpdateRange(game.Players);
            for (int i = 0; i < game.Players.Count; i++)
            {
                _ludoContext.GamePiece.UpdateRange(game.Players[i].GamePieces);
            }
            _ludoContext.SaveChanges();
        }

        public Game ContinueCurrentGame()
        {
            var game = _ludoContext.Game.Include(x => x.Players).Where(x => x.WinnerPlayerName == null).FirstOrDefault();

            return game;
        }

        public List<Game> ShowGameHistory()
        {
            var games = _ludoContext.Game.Include(x => x.Players).Where(x => x.WinnerPlayerName != null).ToList();

            return games;
        }
    }
}