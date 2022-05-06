using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clbRepository;
        private readonly IPhotoService _phtService;

        public ClubController(IClubRepository clbRepository, IPhotoService phtService)
        {
            _clbRepository = clbRepository;
            _phtService = phtService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clbRepository.GetAll();
            return View(clubs);
        }

        public async Task<IActionResult> Details(int id)
        {
            Club club = await _clbRepository.GetByIdAsync(id);
            return View(club);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Club club)
        {
            if (ModelState.IsValid)
            {
                //var result = await _phtService.AddPhotoAsync(club.Image)
                return View(club);
            }
            _clbRepository.Add(club);
            return RedirectToAction("Index");
        }
    }
}