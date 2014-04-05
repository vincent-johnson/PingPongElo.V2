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

        public IEnumerable<Game> GetGamesByUsername(int playerId)
        {
            var games = _repo.FindBy(x => x.ChallengerId == playerId || x.DefenderId == playerId)
                        .ToList();
            return games;
        }
    }
}
