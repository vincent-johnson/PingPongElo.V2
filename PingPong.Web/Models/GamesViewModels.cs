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

        [Display(Name = "Challenger")]
        [Required]
        public string ChallengerUsername { get; set; }
        [Display(Name = "Defender")]
        [Required]
        public string DefenderUsername { get; set; }
        [Display(Name = "Defender Won?")]
        [Required]
        public bool DefenderWonFlag { get; set; }

        [Display(Name = "Challenger Score")]
        public int ChallengerScore { get; set; }
        [Display(Name = "Defender Score")]
        public int DefenderScore { get; set; }
    }
}