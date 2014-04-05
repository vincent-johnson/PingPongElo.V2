using PingPong.Data;
using PingPong.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPong.BLL
{
    public static class PlayerService
    {
        private static readonly IPingPongRepository<Player> _repo;

        static PlayerService()
        {
            _repo = new PingPongRepository<Player>();
        }

        /// <summary>
        /// Creates a new Player
        /// </summary>
        public static void CreateNewPlayer(Player player)
        {
            _repo.Create(player);
        }

        /// <summary>
        /// Updates an existing Player
        /// </summary>
        public static void UpdateExistingPlayer(Player player)
        {
            _repo.Update(player);
        }

        /// <summary>
        /// Deletes an existing Player
        /// </summary>
        public static void DeleteExistingPlayer(Player player)
        {
            _repo.Delete(player.PlayerId);
        }

        /// <summary>
        /// Returns Player without password info
        ///  </summary>
        /// <returns></returns>
        public static IEnumerable<Player> GetAllPlayersRestricted()
        {
            var players = GetAllPlayers();
            foreach (var player in players)
            {
                player.Password = "cheeseburgerlova";
            }
            return players;
        }

        /// <summary>
        /// Determines if user's password is correct
        /// </summary>
        /// <param name="password">User's input password</param>
        /// <returns>True or false</returns>
        public static bool IsPasswordCorrect(Player player, string password)
        {
            return (player.Password == password ? true : false); 
        }

        /// <summary>
        /// Returns Player's first and last name
        /// </summary>
        /// <returns></returns>
        private static string GetFullName(Player player)
        {
            var fullname = string.Format("{0} {1}", player.FirstName, player.LastName); //NOTE: This method is currently not used in anywhere
            return fullname;
        }

        /// <summary>
        /// Returns all player objects
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Player> GetAllPlayers()
        {
            var players = _repo.GetAll();
            return players;
        }

        /// <summary>
        /// Returns player based on password
        /// </summary>
        /// <param name="password">Player's password</param>
        /// <returns></returns>
        public static Player GetPlayerByPassword(string password)
        {
            var player = _repo.FindBy(x => x.Password == password)
                         .SingleOrDefault();
            return player;
        }
    }
}
