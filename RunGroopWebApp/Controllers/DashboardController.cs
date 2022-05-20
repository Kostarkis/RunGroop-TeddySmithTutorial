using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Dtos;
using RunGroopWebApp.Interfaces;

namespace RunGroopWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dshbrdRepository;

        public DashboardController(IDashboardRepository dshbrdRepository)
        {
            _dshbrdRepository = dshbrdRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userRaces = await _dshbrdRepository.GetAllUserRaces();
            var userClubs = await _dshbrdRepository.GetAllUserClubs();
            var dashboardDto = new DashboardIndexDto()
            {
                Races = userRaces,
                Clubs = userClubs
            };
            return View(dashboardDto);
        }
    }
}
