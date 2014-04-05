using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPong.Entities
{
    public class Elo
    {
        [Key]
        public int EloId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int EloScore { get; set; }
        [Required]
        public int GameId { get; set; }
    }
}
