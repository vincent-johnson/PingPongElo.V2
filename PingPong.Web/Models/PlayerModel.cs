using PingPong.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PingPong.Entities;

namespace PingPong.Web.Models
{
    public class PlayerModel
    {
        private readonly IPingPongRepository<Player> _repo;
        private readonly Player _player;

        public PlayerModel(int id)
        {
            _repo = new PingPongRepository<Player>();
            _player = _repo.FindBy(x => x.Id == id).SingleOrDefault();
        }

        public string GetPlayerName()
        {
            return _player.FirstName + _player.LastName;
        }
    }
}