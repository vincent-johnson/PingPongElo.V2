using Microsoft.AspNet.Identity;
using PingPong.BLL;
using PingPong.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PingPong.Web.Controllers
{
    public class RecomputeController : Controller
    {
        // GET: Recompute
        public ActionResult Index()
        {
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Login", "Account");
            }
            var id = GetPlayerId(User.Identity.Name);
            var player = PlayerService.GetPlayerById(id);
            if (player.IsAdmin)
            {
                Recompute.EloWeight = 30;
                var players = Recompute.GetRecomputedPlayers();
                //Recompute.SaveRecomputedChanges(players);
                return View(players);    
            }
            return RedirectToAction("Index", "Home");
        }

        private int GetPlayerId(string username)
        {
            return PlayerService.GetAllPlayersRestricted().Where(a => a.LoginName == username).FirstOrDefault().PlayerId;
        }
    }


}