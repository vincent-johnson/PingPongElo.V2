using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PingPong.BLL;
using PingPong.Entities;

namespace PingPong.Web.Models
{
    public class HomeIndexViewModel
    {
        public List<Player> players { get; set; }

        public Game LatestGame(int playerId)
        {
            return GameService.FindLatestGameByUserId(playerId);
        }

        public string EloDifference(bool playerIsChallenger, Game game)
        {
            int pointSwing = (int)(playerIsChallenger ? game.pointSwing : -game.pointSwing);
            return  pointSwing > 0 ? string.Format("+{0}", pointSwing) : string.Format("{0}", pointSwing);
        }

        public int GameCount(int playerId)
        {
            return GameService.GetNumberOfGamesForPlayer(playerId);
        }

    }
}