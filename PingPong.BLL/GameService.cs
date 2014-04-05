using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PingPong.Data;
using PingPong.Entities;

namespace PingPong.BLL
{
    public class GameService
    {
        private readonly IPingPongRepository<Game> _repo;
        private readonly Game _game;

        public GameService(Game game)
        {
            _repo = new PingPongRepository<Game>();
            _game = game;
        }

        public GameService()
        {
            _repo = new PingPongRepository<Game>();
        }

        /// <summary>
        /// Returns all games associated with player id
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public IEnumerable<Game> GetGamesByPlayerId(int playerId)
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
        public bool playerWon(int playerScore, int opponentScore)
        {
            return (playerScore > opponentScore ? true : false);
        }

        /// <summary>
        /// Updates an existing game
        /// </summary>
        public void UpdateExistingGame()
        {
            _repo.Update(_game);
        }

        /// <summary>
        /// Creates a new game
        /// </summary>
        public void CreateNewGame()
        {
            _repo.Create(_game);
        }


        /// <summary>
        /// Deletes an existing game
        /// </summary>
        public void DeleteExistingGame()
        {
            _repo.Delete(_game);
        }
    }
}
