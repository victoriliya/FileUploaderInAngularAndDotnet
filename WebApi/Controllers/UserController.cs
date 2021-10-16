using AutoMapper;
using CoolApi.DTOs;
using CoolApi.Helpers;
using CoolApi.Models;
using CoolApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly int _perPage;

        public UserController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _perPage = Convert.ToInt32(configuration.GetSection("PaginationSettings:perPage").Value);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUser([FromQuery] int page)
        {

            page = page <= 0 ? 1 : page;

            var users = await _userRepository.GetUsers(page, _perPage);

            var customerToReturn = _mapper.Map<List<UserReadDto>>(users);

            int i = 0;
            foreach (var user in users)
            {
                customerToReturn[i].FileReadDto = _mapper.Map<List<FileReadDto>>(user.File);
                i++;

            }

            var pageMetaData = Utilities.Paginate(page, _perPage, _userRepository.TotalCount);
            var pagedItems = new PaginatedResultDto<UserReadDto> { PageMetaData = pageMetaData, ResponseData = customerToReturn };
            return Ok(Utilities.CreateResponse(message: "All Customers", errs: null, data: pagedItems));

        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserToAddDto user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(Utilities.CreateResponse(message: "Model state error", errs: ModelState, data: ""));
            }

            if (await _userRepository.EmailExist(user.Email))
            {
                return BadRequest(Utilities.CreateResponse(message: "Email already exists", errs: ModelState, data: ""));
            }

            var userToAdd = _mapper.Map<User>(user);
            var filesToAdd = _mapper.Map<IEnumerable<File>>(user.FilesToAddDto);

            var userFiles = new List<File>();
            foreach (var file in filesToAdd)
            {
                file.UserId = userToAdd.Id;
                userFiles.Add(file);
            }

            var userAdded = await _userRepository.AddUsers(userToAdd, userFiles);

            if (userAdded == false)
            {
                ModelState.AddModelError("User", "Could not new user");
                return BadRequest(Utilities.CreateResponse(message: "User not added", errs: ModelState, data: ""));
            }

            return Ok(Utilities.CreateResponse(message: "User Added sucessfully", errs: null, data: ""));

        }


        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(Utilities.CreateResponse(message: "Model state error", errs: ModelState, data: ""));
            }

            var result = await _userRepository.GetUserByEmailAsync(email);


            if (result == null)
            {
                ModelState.AddModelError("User", "Email not associated with any user");
                return NotFound(Utilities.CreateResponse(message: "Email not associated with any userd", errs: ModelState, data: ""));
            }

            var user = _mapper.Map<UserReadDto>(result);

            user.FileReadDto = _mapper.Map<List<FileReadDto>>(result.File);



            return Ok(Utilities.CreateResponse(message: "New customer added", errs: null, data: user));

        }
    }
}
