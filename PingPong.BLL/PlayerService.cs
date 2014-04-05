using PingPong.Data;
using PingPong.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPong.BLL
{
    public class PlayerService
    {
        private readonly IPingPongRepository<Player> _repo;
        private readonly Player _player;

        public PlayerService(Player player)
        {
            _repo = new PingPongRepository<Player>();
            _player = player;
        }

        /// <summary>
        /// Creates a new Player
        /// </summary>
        public void CreateNewPlayer()
        {
            _repo.Create(_player);
        }

        /// <summary>
        /// Updates an existing Player
        /// </summary>
        public void UpdateExistingPlayer()
        {
            _repo.Update(_player);
        }

        /// <summary>
        /// Deletes an existing Player
        /// </summary>
        public void DeleteExistingPlayer()
        {
            _repo.Delete(_player.PlayerId);
        }

        public Dictionary<string, double> GetAllPlayersRestricted()
        {
            var players = GetAllPlayers();
            var restrictedPlayers = new Dictionary<string, double>();
            foreach (var player in players)
            {
                var fullname = GetFullName(player);
                restrictedPlayers.Add(fullname, player.CurrentEloRating);
            }
            return restrictedPlayers;
        }

        /// <summary>
        /// Determines if user's password is correct
        /// </summary>
        /// <param name="password">User's input password</param>
        /// <returns>True or false</returns>
        public bool IsPasswordCorrect(string password)
        {
            return (_player.Password == password ? true : false);
        }

        /// <summary>
        /// Returns Player's first and last name
        /// </summary>
        /// <returns></returns>
        private string GetFullName(Player player)
        {
            return player.FirstName + player.LastName;
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            var players = _repo.GetAll();
            return players;
        }
    }
}
