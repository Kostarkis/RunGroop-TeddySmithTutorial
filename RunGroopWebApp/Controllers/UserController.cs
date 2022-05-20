using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Dtos;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Maps;

namespace RunGroopWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users =  await _userRepository.GetAllUsers();
            List<IndexUserDto> result = new List<IndexUserDto>();
            foreach (var user in users)
            {
                var userDto = _mapper.Map<IndexUserDto>(user);
                result.Add(userDto);
            }
            return View(result);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userDto = _mapper.Map<DetailsUserDto>(user);
            return View(userDto);
        }
    }
}
