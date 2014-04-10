using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PingPong.Data;
using PingPong.Entities;

namespace PingPong.BLL
{
    public static class Recompute
    {
        private static readonly IPingPongRepository<Game> GameRepo;
        private static readonly IPingPongRepository<Player> PlayerRepo;

        static Recompute()
        {
            GameRepo = new PingPongRepository<Game>();
            PlayerRepo = new PingPongRepository<Player>();
        }

        public static int EloWeight { get; set; }

        public static IEnumerable<Player> GetRecomputedPlayers()
        {
            var players = PlayerRepo.GetAll().ToList();
            var games = GameRepo.GetAll().ToList().OrderBy(o => o.GameId);
            var count = games.Count();

            //Reset initial rankings to default value
            players.ToList().ForEach(o => o.CurrentEloRating = 1500);

            for (int i = 0; i < count; i++)
            {
                var defender = players.Find(x => x.PlayerId == games.ElementAt(i).DefenderId);
                var challenger = players.Find(x => x.PlayerId == games.ElementAt(i).ChallengerId);
                double eloDelta;

                //Get Elo delta for game
                if (defender != null && challenger != null)
                    eloDelta = EloCalculator.GetPlayerEloChange(defender.CurrentEloRating, 
                        challenger.CurrentEloRating,games.ElementAt(i).DefenderWon, EloWeight);
                else
                    throw new NullReferenceException("Players could not be retrieved.");

                //Reassign elo ratings
                if (games.ElementAt(i).DefenderWon)
                {
                    defender.CurrentEloRating += eloDelta;
                    challenger.CurrentEloRating -= eloDelta;
                }
                else
                {
                    defender.CurrentEloRating -= eloDelta;
                    challenger.CurrentEloRating += eloDelta;
                }
            }

            if (players.Count() == players.Distinct().Count())
                return players;

            throw new Exception("Player record is duplicated");
        }



        public static void SaveRecomputedChanges(IEnumerable<Player> players)
        {
            var playerCount = players.Count();
            for (int i = 0; i < playerCount; i++)
            {
                PlayerRepo.Update(players.ElementAt(i));
            }
        }
    }
}
