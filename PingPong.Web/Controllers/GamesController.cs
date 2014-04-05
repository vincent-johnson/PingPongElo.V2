using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PingPong.BLL;
using PingPong.Data;
using PingPong.Entities;
using PingPong.Web.Models;

namespace PingPong.Web.Controllers
{
    public class GamesController : Controller
    {
        // GET: Games
        public ActionResult Index()
        {
            IEnumerable<Game> games= GetGames(User.Identity.Name);
            var a = new GamesIndexViewModel();
            a.games = games;
            return View(a);
        }

        // GET: Games/Details/5
        public ActionResult Details(int id)
        {
            var repo = new PingPongRepository<Game>();
            var game = repo.FindBy(x => x.GameId == id).SingleOrDefault();
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            var ps = new PlayerService(new Player());
            var players = ps.GetAllPlayersRestricted();
            var usernames = new List<string>();
            foreach (Player player in players)
            {
                usernames.Add(player.LoginName);
            }
            var a = new GamesCreateViewModel();
            a.Usernames = usernames;
            return View(a);
        }

        // POST: Games/Create
        [HttpPost]
        public ActionResult Create(GamesCreateViewModel gamevM)
        {
            try
            {
                Game game=new Game();
                game.IsRankedGame = true;
                game.VerifiedById = 0;
                game.GamePlayedDate = DateTime.UtcNow;
                game.ChallengerSecondId = -1;
                game.DefenderSecondId = -1;
                game.GameTypeId = 1;
                PlayerService ps = new PlayerService(new Player());
                var players=ps.GetAllPlayersRestricted();
                Player defender = players.Where(a => a.LoginName == gamevM.DefenderUsername).FirstOrDefault();
                game.DefenderId = defender.PlayerId;
                game.DefenderEloRating = (int)defender.CurrentEloRating;
                Player challenger = players.Where(a => a.LoginName == gamevM.ChallengerUsername).FirstOrDefault();
                game.ChallengerId = challenger.PlayerId;
                game.ChallengerEloRating = (int)challenger.CurrentEloRating;
                game.DefenderWon = gamevM.DefenderWonFlag;
                GameService gs = new GameService(game);
                gs.CreateNewGame();
                return RedirectToAction("Index");
            }
            catch
            {
                var ps = new PlayerService(new Player());
                var players = ps.GetAllPlayersRestricted();
                var usernames = new List<string>();
                foreach (Player player in players)
                {
                    usernames.Add(player.LoginName);
                }
                var a = new GamesCreateViewModel();
                a.Usernames = usernames;
                return View(a);
            }
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Games/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Games/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private IEnumerable<Game> GetGames(string username)
        {
            PlayerService ps = new PlayerService(new Player());
            GameService gs = new GameService();
            var player=ps.GetAllPlayersRestricted().Where(a => a.LoginName == username).FirstOrDefault();
            return gs.GetGamesByPlayerId(player.PlayerId);
        }
    }
}
