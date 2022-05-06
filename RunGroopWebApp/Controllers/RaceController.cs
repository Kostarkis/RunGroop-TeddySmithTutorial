using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _rcRepository;
        public RaceController(IRaceRepository rcRepository)
        {
            _rcRepository = rcRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await _rcRepository.GetAll();
            return View(races);
        }
        public async Task<IActionResult> Details(int id)
        {
            Race race = await _rcRepository.GetByIdAsync(id);
            return View(race);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Race race)
        {
            if (!ModelState.IsValid)
            {
                return View(race);
            }
            _rcRepository.Add(race);
            return RedirectToAction("Index");
        }
    }
}
