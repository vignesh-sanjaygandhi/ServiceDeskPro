using Microsoft.AspNetCore.Mvc;
using ServiceDeskPro.Data;
using ServiceDeskPro.Models;

namespace ServiceDeskPro.Controllers
{
    public class TicketsController : Controller
    {
        private readonly TicketRepository _repository;

        public TicketsController()
        {
            _repository = new TicketRepository();
        }

        // GET: Loads the dashboard with all tickets
        public IActionResult Index()
        {
            var tickets = _repository.GetAllTickets();
            return View(tickets);
        }

        // GET: Loads the Create Ticket form
        public IActionResult Create()
        {
            return View();
        }

        // POST: Saves the new ticket to the database
        [HttpPost]
        public IActionResult Create(Ticket ticket)
        {
            _repository.AddTicket(ticket);
            return RedirectToAction("Index"); // Send user back to dashboard
        }
    }
}