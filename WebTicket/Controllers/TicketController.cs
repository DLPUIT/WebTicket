using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTicket.Repo;

namespace WebTicket.Controllers
{
    public class TicketController : Controller
    {
      
        public  ActionResult TicketRequest()
        {
            return View();
        }

    } 
}