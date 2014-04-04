using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PingPong.Entities;

namespace PingPong.Data
{
    public class PingPongContext : DbContext
    {
        public DbSet<Elo> Elos { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<GameType> GameTypes { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}
