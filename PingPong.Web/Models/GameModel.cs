using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PingPong.Data;
using PingPong.Entities;

namespace PingPong.Web.Models
{
    public class GameModel
    {
        private readonly IPingPongRepository<Game> _repo;
        private readonly Game _game;

        public GameModel(int id)
        {
            _repo = new PingPongRepository<Game>();
            _game = _repo.FindBy(x => x.Id == id).SingleOrDefault();
        }
    }
}