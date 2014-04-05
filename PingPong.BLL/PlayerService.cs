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

        /// <summary>
        /// Returns Player information useful to view
        /// Indexes are as follows
        /// [0] = Player Id
        /// [1] = Player FullName
        /// [2] = Player Current Elo ranking
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Player> GetAllPlayersRestricted()
        {
            var players = GetAllPlayers();
            var restrictedPlayers = new List<Player>();

            foreach (var player in players)
            {
                player.Password = "cheeseburgerlova";
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
            var fullname = string.Format("{0} {1}", player.FirstName, player.LastName);
            return fullname;
        }

        /// <summary>
        /// Returns all player objects
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Player> GetAllPlayers()
        {
            var players = _repo.GetAll();
            return players;
        }

        /// <summary>
        /// Returns player based on password
        /// </summary>
        /// <param name="password">Player's password</param>
        /// <returns></returns>
        public Player GetPlayerByPassword(string password)
        {
            var player = _repo.FindBy(x => x.Password == password)
                         .SingleOrDefault();
            return player;
        }
    }
}
