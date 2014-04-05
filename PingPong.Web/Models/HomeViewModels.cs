using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PingPong.Entities;

namespace PingPong.Web.Models
{
    public class HomeIndexViewModel
    {
        public List<Player> players { get; set; }
    }
}