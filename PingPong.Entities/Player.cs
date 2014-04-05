using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPong.Entities
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
        [MaxLength(100), Required]
        public string FirstName { get; set; }
        [MaxLength(100), Required]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string NickName { get; set; }
        [MaxLength(100), Required]
        public string Password { get; set; }
        [Required]
        public bool Active { get; set; }
        [MaxLength(100)]
        public string Department { get; set; }
        [Required]
        public int CurrentEloRating { get; set; }
        [Required]
        public bool IsAdmin { get; set; }

    }
}
