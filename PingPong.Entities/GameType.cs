using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPong.Entities
{
    public class GameType
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string GameTypeName { get; set; }

    }
}
