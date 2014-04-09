﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using PingPong.Data;
using PingPong.Entities;

namespace PingPong.BLL
{
    public class GameService
    {
        private static readonly IPingPongRepository<Game> _repo;

        static GameService()
        {
            _repo = new PingPongRepository<Game>();
        }


        /// <summary>
        /// Returns all games associated with player id
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static IEnumerable<Game> GetGamesByPlayerId(int playerId)
        {
            var games = _repo.FindBy(x => x.ChallengerId == playerId || x.DefenderId == playerId)
                        .ToList();
            return games;
        }

        /// <summary>
        /// Determines if player won game
        /// </summary>
        /// <param name="playerScore"></param>
        /// <param name="opponentScore"></param>
        /// <returns></returns>
        public static bool playerWon(int playerScore, int opponentScore)
        {
            return (playerScore > opponentScore ? true : false);
        }

        /// <summary>
        /// Updates an existing game
        /// </summary>
        public static void UpdateExistingGame(Game game)
        {
            _repo.Update(game);
        }

        /// <summary>
        /// Creates a new game
        /// </summary>
        public static void CreateNewGame(Game game)
        {
            _repo.Create(game);
        }


        /// <summary>
        /// Deletes an existing game
        /// </summary>
        public static void DeleteExistingGame(Game game)
        {
            _repo.Delete(game.GameId);
        }

        public static void DeleteExistingGameById(int id)
        {
            _repo.Delete(id);
        }

        public static Game FindLatestGameByUserId(int id)
        {
            try
            {
                var games = _repo.FindBy(x => x.ChallengerId == id || x.DefenderId == id).ToList();
                var latestGameId = games.Max(x => x.GameId);
                return _repo.FindBy(x => x.GameId == latestGameId).SingleOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("You have no existing games");
            }
            
        }

        public static Game GetGameById(int id)
        {
            var game = _repo.FindBy(x => x.GameId == id).SingleOrDefault();
            return game;
        }

        public static int GetNumberOfGamesForPlayer(int id)
        {
            var gameCount = _repo.FindBy(x => x.ChallengerId == id || x.DefenderId == id).Count();
            return gameCount;
        }
    }
}
