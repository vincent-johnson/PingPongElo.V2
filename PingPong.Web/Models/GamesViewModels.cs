using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;
using PingPong.BLL;
using PingPong.Data;
using PingPong.Entities;

namespace PingPong.Web.Models
{
    public class GamesIndexViewModel
    {
        public IEnumerable<Game> games { get; set; }
        public int PlayerId { get; set; }
        public IEnumerable<Player> players { get; set; }
        public int? DeletedGamesCounter { get; set; }
        public Game LatestGame { get; set; }
        public string OpponentFullName { get; set; }
        public int OpponentId { get; set; }
        public int GameCount { get; set; }
    }
    public class GamesCreateViewModel
    {
        public IEnumerable<string> Usernames {get;set;}

        [Display(Name = "Opponent")]
        [Required]
        public string OpponenUsername { get; set; }
        [Display(Name = "Did you win?")]
        [Required]
        public bool YouWonFlag { get; set; }

        [Display(Name = "Your Score")]
        public int YourScore { get; set; }
        [Display(Name = "Opponent's Score")]
        public int OpponentScore { get; set; }
    }

    public class GamesDeleteViewModel
    {
        private readonly Game _game;

        public GamesDeleteViewModel(Game game)
        {
            _game = game;
        }

        public void DeleteLatestGame()
        {
            var challengerLatestGame = GameService.FindLatestGameByUserId(_game.ChallengerId);
            var defenderLatestGame = GameService.FindLatestGameByUserId(_game.DefenderId);

            if (defenderLatestGame.GameId == challengerLatestGame.GameId)
            {
                var pointsToDevalue = _game.pointSwing;
                var defender = PlayerService.GetPlayerById(_game.DefenderId);
                var challenger = PlayerService.GetPlayerById(_game.ChallengerId);

                challenger.CurrentEloRating -= pointsToDevalue;
                defender.CurrentEloRating += pointsToDevalue;

                PlayerService.UpdateExistingPlayer(challenger);
                PlayerService.UpdateExistingPlayer(defender);
                GameService.DeleteExistingGame(_game); 
            }
            else
                throw new Exception("Make this a pretty popup saying user's latest game was not opponent's latest game");
        }   
    }

    public class RatingCalculatorViewModel
    {
        [Display(Name = "Your Rating")]
        [Required]
        public int YourRating { get; set; }
        [Display(Name = "Opponent Rating")]
        [Required]
        public int OpponentRating{ get; set; }
        [Display(Name = "Did you win?")]
        [Required]
        public bool YouWonFlag { get; set; }
        [Display(Name = "My Rating Change (in points)")]
        [Required]
        public double? MyRatingChange { get; set; }
    }
}