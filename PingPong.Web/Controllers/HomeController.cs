using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PingPong.BLL;
using PingPong.Entities;
using Microsoft.AspNet.Identity;
using PingPong.Web.Models;

namespace PingPong.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var vM = new HomeIndexViewModel();
            vM.players = GetPlayers().OrderByDescending(d=>d.CurrentEloRating).ToList();
            return View(vM);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact information for the STAPPPP team.";

            return View();
        }

        public IEnumerable<Player> GetPlayers()
        { 
            PlayerService pService = new PlayerService(new Player());
            return pService.GetAllPlayersRestricted(); //User.Identity.GetUserName();
        }
    }
}