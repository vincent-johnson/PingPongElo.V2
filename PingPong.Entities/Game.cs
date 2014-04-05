using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPong.Entities
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        [Required]
        public int ChallengerId { get; set; }
        [Required]
        public int DefenderId { get; set; }
        [Required]
        public int ChallengerEloRating { get; set; }
        [Required]
        public int DefenderEloRating { get; set; }
        [Required]
        public bool DefenderWon { get; set; }

        public int VerifiedById { get; set; }
        [Required]
        public DateTime GamePlayedDate { get; set; }

        public int ChallengerSecondId { get; set; }
        public int DefenderSecondId { get; set; }

        [Required]
        public int GameTypeId { get; set; }
        [Required]
        public bool IsRankedGame { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        public double pointSwing { get; set; }
    }
}
