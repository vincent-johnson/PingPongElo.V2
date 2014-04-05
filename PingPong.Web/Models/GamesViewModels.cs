using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PingPong.Entities;

namespace PingPong.Web.Models
{
    public class GamesIndexViewModel
    {
        public IEnumerable<Game> games { get; set; }
    }
}