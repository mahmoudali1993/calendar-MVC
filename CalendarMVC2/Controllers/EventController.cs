using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CalendarMVC2.Data;
using CalendarMVC2.Models;
using CalendarMVC2.Models.ViewModels;

namespace CalendarMVC2.Controllers
{
    public class EventController : Controller
    {
        private readonly IDAL _idal;

        public EventController(IDAL idal)
        {
            _idal = idal;
        }

        // GET: Event
        public IActionResult Index()
        {
            if (TempData["Alert"] != null)
            {
                ViewData["Alert"] = TempData["Alert"];
            }
            return View(_idal.GetEvents());
        }

        // GET: Event/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _idal.GetEvent((int)id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            return View(new EventViewModel(_idal.GetLocations()));
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EventViewModel vm, IFormCollection form)
        {

            try
            {
                _idal.CreateEvent(form);
                TempData["Alert"] = "Success! You Created a new event for: " + form["Name"];
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                TempData["Alert"] = "An error occurred: " + ex.Message;
                return View(vm);
            }
        }

        // GET: Event/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _idal.GetEvent((int)id);

            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,EventViewModel vm, IFormCollection form)
        {
            if (id != vm.Event.Id)
            {
                return NotFound();
            }
            try
            {
                _idal.UpdateEvent(form);
                TempData["Alert"] = "Success! You modified a record for: " + vm.Event.Name;
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewData["Alert"] = "An error occurred: " + ex.Message;
                return View(vm);
            }
        }

        // GET: Event/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _idal.GetEvent((int)id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _idal.DeleteEvent(id);
            TempData["Alert"] = "You deleted an event.";
            return RedirectToAction(nameof(Index));
;        }

    }
}
