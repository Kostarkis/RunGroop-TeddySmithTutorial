using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Dtos;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _rcRepository;
        private readonly IPhotoService _phtService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RaceController(IRaceRepository rcRepository, IPhotoService phtService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _rcRepository = rcRepository;
            _phtService = phtService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
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
            var curUserID = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createRaceDto = new CreateRaceDto
            {
                UserId = curUserID
            };
            return View(createRaceDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceDto raceDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _phtService.AddPhotoAsync(raceDto.ImageFile);
                var race = _mapper.Map<Race>(raceDto);
                race.Image = result.Url.ToString();
                _rcRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("","Photo upload failed");
            }
            return View(raceDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var race = await _rcRepository.GetByIdAsync(id);
            if (race == null) return View("Error");
            var raceDto = _mapper.Map<EditRaceDto>(race);
            return View(raceDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceDto raceDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", raceDto);
            }

            var userRace = await _rcRepository.GetByIdAsyncNoTracking(id);

            if (userRace != null)
            {
                var race = _mapper.Map<Race>(raceDto);
                if (raceDto.ImageFile != null)
                {
                    try
                    {
                        await _phtService.DeletePhotoAsync(userRace.Image);
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", "Could not delete photo");
                        return View(raceDto);
                    }

                    var photoResult = await _phtService.AddPhotoAsync(raceDto.ImageFile);
                    race.Image = photoResult.Url.ToString();
                }

                _rcRepository.Update(race);

                return RedirectToAction("Index");
            }
            else
            {
                return View(raceDto);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var raceDetails = await _rcRepository.GetByIdAsync(id);
            if (raceDetails == null) return View("Error");
            return View(raceDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var raceDetails = await _rcRepository.GetByIdAsync(id);
            if (raceDetails == null) return View("Error");

            _rcRepository.Delete(raceDetails);
            return RedirectToAction("Index");
        }
    }
}
