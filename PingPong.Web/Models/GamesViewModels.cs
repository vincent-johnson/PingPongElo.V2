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
}