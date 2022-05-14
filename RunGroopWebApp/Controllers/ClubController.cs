using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Dtos;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clbRepository;
        private readonly IPhotoService _phtService;
        private readonly IMapper _mapper;

        public ClubController(IClubRepository clbRepository, IPhotoService phtService, IMapper mapper)
        {
            _clbRepository = clbRepository;
            _phtService = phtService;
            _mapper = mapper;
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
        public async Task<IActionResult> Create(CreateClubDto clubDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _phtService.AddPhotoAsync(clubDto.ImageFile);

                var club = _mapper.Map<Club>(clubDto);

                club.Image = result.Url.ToString();

                _clbRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("","Photo upload failed");
            }

            return View(clubDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clbRepository.GetByIdAsync(id);
            if (club == null) return View("Error");
            var clubDto = _mapper.Map<EditClubDto>(club);
            return View(clubDto);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubDto clubDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", clubDto);
            }

            var userClub = await _clbRepository.GetByIdAsyncNoTracking(id);

            if (userClub != null)
            {
                var club = _mapper.Map<Club>(clubDto);
                if (clubDto.ImageFile != null)
                {
                    try
                    {
                        await _phtService.DeletePhotoAsync(userClub.Image);
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", "Could not delete photo");
                        return View(clubDto);
                    }

                    var photoResult = await _phtService.AddPhotoAsync(clubDto.ImageFile);
                    club.Image = photoResult.Url.ToString();
                }

                _clbRepository.Update(club);

                return RedirectToAction("Index");
            }
            else
            {
                return View(clubDto);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _clbRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");
            return View(clubDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var  clubDetails = await _clbRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");

            _clbRepository.Delete(clubDetails);
            return RedirectToAction("Index");
        }
    }
}