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
            int playerId = -1;
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Login", "Account");
            }
            IEnumerable<Game> games;
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                games = new List<Game>();
            }
            else
            {
                playerId = GetPlayerId(User.Identity.Name);
                games = GetGames(playerId).OrderByDescending(d=>d.GameId);
            }
            IEnumerable<Player> players= PlayerService.GetAllPlayersRestricted();
            var a = new GamesIndexViewModel();
            a.games = games;
            a.PlayerId = playerId;
            a.players = players;
            return View(a);
        }

        // GET: Games/Details/5
        public ActionResult Details(int id)
        {
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Login", "Account");
            }
            var repo = new PingPongRepository<Game>();
            var game = repo.FindBy(x => x.GameId == id).SingleOrDefault();
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Login", "Account");
            }
            var players = PlayerService.GetAllPlayersRestricted().Where(b=>b.LoginName!=User.Identity.Name);
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
                if (String.IsNullOrEmpty(User.Identity.Name))
                {
                    return RedirectToAction("Login", "Account");
                }
                Game game=new Game();
                game.Weight = 30;
                game.IsRankedGame = true;
                game.VerifiedById = 0;
                game.GamePlayedDate = DateTime.UtcNow;
                game.ChallengerSecondId = -1;
                game.DefenderSecondId = -1;
                game.GameTypeId = 1;
                var players=PlayerService.GetAllPlayersRestricted();
                Player defender = players.Where(a => a.LoginName == gamevM.OpponenUsername).FirstOrDefault();
                game.DefenderId = defender.PlayerId;
                game.DefenderEloRating = (int)defender.CurrentEloRating;
                Player challenger = players.Where(a => a.LoginName == User.Identity.Name).FirstOrDefault();
                game.ChallengerId = challenger.PlayerId;
                game.ChallengerEloRating = (int)challenger.CurrentEloRating;
                game.DefenderWon = !gamevM.YouWonFlag;
                double challengerEloChange=GetChallengerEloChange(game);
                game.pointSwing = challengerEloChange;
                GameService.CreateNewGame(game);
                challenger.CurrentEloRating += challengerEloChange;
                defender.CurrentEloRating -= challengerEloChange;
                PlayerService.UpdateExistingPlayer(challenger);
                PlayerService.UpdateExistingPlayer(defender);
                return RedirectToAction("Index");
            }
            catch
            {
                var players = PlayerService.GetAllPlayersRestricted();
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

        public ActionResult RatingCalculator()
        {
            return View(new RatingCalculatorViewModel() { YourRating=1500,OpponentRating=1500});
        }
        [HttpPost]
        public ActionResult RatingCalculator(RatingCalculatorViewModel vM)
        {
            vM.MyRatingChange = EloCalculator.GetPlayerEloChange(vM.YourRating, vM.OpponentRating, vM.YouWonFlag, 30);
            return View(vM);
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

        private IEnumerable<Game> GetGames(int playerId)
        {
            return GameService.GetGamesByPlayerId(playerId);
        }

        private int GetPlayerId(string username)
        {
            return PlayerService.GetAllPlayersRestricted().Where(a => a.LoginName == username).FirstOrDefault().PlayerId;
        }

        private double GetChallengerEloChange(Game game)
        {
            return EloCalculator.GetPlayerEloChange(game.ChallengerEloRating, game.DefenderEloRating, !game.DefenderWon, game.Weight);
        }
    }
}
