using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildTicket.Domain.Contracts;
using BuildTicket.Domain.Implementations;
using BuildTicket.Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Build_Tickets.Controllers
{
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        private readonly ITicketDomain _ticketDomain;

        public TicketController(ITicketDomain ticketDomain)
        {
            this._ticketDomain = ticketDomain;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("saveTicket")]
        public IActionResult SaveTicket([FromBody] Ticket modal)
        {

            return Json(_ticketDomain.Add(modal));
        }

        [HttpPost("updateStatus")]
        public IActionResult UpdateTicket([FromBody] Ticket modal)
        {
            return Json(_ticketDomain.UpdateStatus(modal));
        }

        [HttpGet("ListTickets")]
        public IActionResult List()
        {
            return Json(_ticketDomain.List());
        }

        [HttpGet]
        public Status Delete(int id)
        {
            return _ticketDomain.Delete(id);
        }


    }
}