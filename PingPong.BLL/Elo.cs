using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PingPong.Entities;

namespace PingPong.BLL
{
    public class Elo
    {
        private readonly Player _player;
        private readonly Player _opponent;
        private readonly bool _playerWon;
        private readonly int _numOfGames;

        public Elo(Player player, Player opponent, bool playerWon, int numOfGames)
        {
            _player = player;
            _opponent = opponent;
            _playerWon = playerWon;
            _numOfGames = numOfGames;
            Swing = 400;
        }

        private static int Swing { get; set; }

        public double GetNewPlayerElo()
        {
            if (_playerWon)
                _player.CurrentEloRating += (_opponent.CurrentEloRating + Swing) * (_numOfGames / (_numOfGames + 1));
            else
                _player.CurrentEloRating += (_opponent.CurrentEloRating - Swing) * (_numOfGames / (_numOfGames + 1));

            return _player.CurrentEloRating;
        }
    }
}
