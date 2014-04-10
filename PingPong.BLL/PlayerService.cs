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

        /// <summary>
        /// Returns Players full name based on username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string GetPlayerFullNameByUsername(string username)
        {
            var fullname = _repo.FindBy(x => x.LoginName == username)
                           .Select(x => x.FirstName + " " + x.LastName)
                           .SingleOrDefault();
            return fullname;
        }

        /// <summary>
        /// Returns player's full name based on identification number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetPlayerFullNameById(int id)
        {
            var fullname = _repo.FindBy(x => x.PlayerId == id)
                            .Select(x => x.FirstName + " " + x.LastName)
                            .SingleOrDefault();
            return fullname;
        }


        /// <summary>
        /// Retrieves player by indentification number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Player GetPlayerById(int id)
        {
            var player = _repo.FindBy(x => x.PlayerId == id)
                        .SingleOrDefault();
            return player;
        }

        /// <summary>
        /// Returns boolean value based on whether password is correct
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsCorrectPassword(int id, string password)
        {
            var pw = _repo.FindBy(x => x.PlayerId == id).Select(x => x.Password).SingleOrDefault();
            return password == pw;
        }
    }
}
