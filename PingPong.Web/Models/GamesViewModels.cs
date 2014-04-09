using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PingPong.Entities;

namespace PingPong.Web.Models
{
    public class GamesIndexViewModel
    {
        public IEnumerable<Game> games { get; set; }
        public int PlayerId { get; set; }
        public IEnumerable<Player> players { get; set; }
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