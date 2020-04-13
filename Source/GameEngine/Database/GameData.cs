using Microsoft.EntityFrameworkCore;
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

            _ludoContext.SaveChanges();

            return game;
        }

        public void InitilizePlayersAndPieces(Game game)
        {
            foreach (var player in game.Players)
            {
                _ludoContext.Player.Add(player);
                foreach (var gp in player.GamePieces)
                {
                    _ludoContext.GamePiece.Add(gp);
                }
            }

            _ludoContext.SaveChanges();

        }
      

        public void UpdateGame(Game game)
        {
            _ludoContext.Game.Update(game);
            foreach (var player in game.Players)
            {
                _ludoContext.Player.Update(player);
                foreach (var gp in player.GamePieces)
                {
                    SaveGamePieceMove(gp);
                }
            }
            _ludoContext.SaveChanges();
        }

        //@Spy grupp 3 (Benjamin), hjälp med att vår databas fick constraint problem och Benjamin visade hur rupp tre hade löst detta problem.
        public void SaveGamePieceMove(GamePiece gp)
        {
            var currentPiece = _ludoContext.GamePiece.SingleOrDefault(x => x.GamePieceID == gp.GamePieceID);

            currentPiece.BoardPosition = gp.BoardPosition;
            currentPiece.PositionType = gp.PositionType;
            currentPiece.StepCounter = gp.StepCounter;
            currentPiece.PlayerID = gp.PlayerID;

            _ludoContext.SaveChanges();
        }

        public Game LoadGame()
        { 

           var currentGame = _ludoContext.Game.Include(x => x.Players)
                .ThenInclude(x => x.GamePieces)
                .FirstOrDefault(x => x.HasFinished == false);

            if(currentGame == null)
            {
                throw new NullReferenceException("No current game");
            }
            

            return currentGame;
        }

        public Game GetCurrentGame()
        {
            return _ludoContext.Game.Where(x => x.HasFinished == false).FirstOrDefault();
        }

        public void FinishGame(Game game)
        {
            Player winner = null;
            foreach (var player in game.Players)
            {
                winner = game.Players.Where(x => x.Score == 4).FirstOrDefault();
            }

            game.WinnerPlayerName = winner.Name;
            game.HasFinished = true;

            _ludoContext.GamePiece.RemoveRange(_ludoContext.GamePiece.ToList());
            _ludoContext.Player.RemoveRange(_ludoContext.Player.ToList());
            _ludoContext.Game.Update(game);

        }

        public List<Game> ShowGameHistory()
        {
            var games = _ludoContext.Game.Where(x => x.WinnerPlayerName != null).ToList();

            return games;
        }
    }
}
