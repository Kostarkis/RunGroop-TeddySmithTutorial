using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Dtos;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dshbrdRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _phtService;
        private readonly IMapper _mapper;

        public DashboardController(IDashboardRepository dshbrdRepository, IHttpContextAccessor httpContextAccessor, IPhotoService phtService, IMapper mapper)
        {
            _dshbrdRepository = dshbrdRepository;
            _httpContextAccessor = httpContextAccessor;
            _phtService = phtService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var userRaces = await _dshbrdRepository.GetAllUserRaces();
            var userClubs = await _dshbrdRepository.GetAllUserClubs();
            var dashboardDto = new IndexDashboardDto()
            {
                Races = userRaces,
                Clubs = userClubs
            };
            return View(dashboardDto);
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dshbrdRepository.GetUserById(curUserId);
            if (user == null) return View("Error");
            var editUserDto = _mapper.Map<EditUserDashboardDto>(user);
            editUserDto.Id = curUserId;
            return View(editUserDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardDto editUserDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editUserDto);
            }

            var user = await _dshbrdRepository.GetUserByIdNoTracking(editUserDto.Id);
            if (user.ProfileImageUrl != "" && user.ProfileImageUrl != null)
            {
                try
                {
                    await _phtService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editUserDto);
                }
            }
            //user = _mapper.Map<User>(editUserDto);
            user.Mileage = editUserDto.Mileage;
            user.Pace = editUserDto.Pace;
            user.City = editUserDto.City;
            user.State = editUserDto.State;
            if (editUserDto.Image != null)
            {
                var photoResult = await _phtService.AddPhotoAsync(editUserDto.Image);
                user.ProfileImageUrl = photoResult.Url.ToString();
            }
            
            
            
            _dshbrdRepository.Update(user);
            return RedirectToAction("Index");
        }
    }
}
