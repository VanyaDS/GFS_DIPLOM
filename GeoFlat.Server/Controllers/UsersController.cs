using AutoMapper;
using GeoFlat.Server.Automapper.RequestModels;
using GeoFlat.Server.Automapper.ResponseModels;
using GeoFlat.Server.Helpers;
using GeoFlat.Server.Models.Database.Entities;
using GeoFlat.Server.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GeoFlat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private int _UserId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        private readonly ILogger<UsersController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UsersController(
            ILogger<UsersController> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
             _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = RoleHealper.ADMIN)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _unitOfWork.Users.All();

            List<UserResponse> usersResponse = new List<UserResponse>();
            if (users is not null)
            {
                foreach (var user in users)
                {
                    usersResponse.Add(_mapper.Map<UserResponse>(user));
                }
            }
            return Ok(usersResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _unitOfWork.Users.GetById(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserResponse>(user));
        }
        
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _unitOfWork.Users.GetById(_UserId);

            if (user is null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<UserResponse>(user));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRequest UserRequest)
        {
            if (UserRequest is null)
            {
                return BadRequest();
            }

            var hasTheSameEmail = await _unitOfWork.Accounts.FindSingleOrDefaultAsync(x => x.Email == UserRequest.Email);
          
            if(hasTheSameEmail is not null || UserRequest.Email == "geoflatbel@gmail.com")
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var account = _mapper.Map<Account>(UserRequest);
                account.Password = HashingMD5.GetHashStringMD5(account.Password);
                account.Role = RoleHealper.CLIENT;
                var user = _mapper.Map<User>(UserRequest);
                user.Account = account;

                if (await _unitOfWork.Users.Add(user))
                {
                    await _unitOfWork.CompleteAsync();
                    return Ok(_mapper.Map<UserResponse>(user));
                }
            }
            return BadRequest();
        }

        [HttpPut]// update of CURRENT user
        [Authorize]
        public async Task<IActionResult> UpdateCurrentUser(UserRequest UserRequest)
        {
            if (UserRequest is null)
            {
                return BadRequest();
            }

            var anyUser = await _unitOfWork.Users.GetById(_UserId);

            if (anyUser is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var account = _mapper.Map<Account>(UserRequest);
                var user = _mapper.Map<User>(UserRequest);
                user.Account = account;
                user.Id = _UserId;

                if (await _unitOfWork.Users.Update(user))
                {
                    await _unitOfWork.CompleteAsync();
                    return NoContent();
                }

            }
            return BadRequest();
        }


        [HttpPut("moderate/{id}")]
        [Authorize(Roles = RoleHealper.ADMIN)]
        public async Task<IActionResult> UpdateRole(int id)
        {
            var user = await _unitOfWork.Users.GetById(id);

            if (user is null)
            {
                return NotFound();
            }
            user.Account.Role = RoleHealper.MODER;
            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<UserResponse>(user));
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = RoleHealper.ADMIN)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var User = await _unitOfWork.Users.GetById(id);

            if (User == null)
            {
                return NotFound();
            }

            if (await _unitOfWork.Accounts.Delete(id) /* to delete cascade*/ )
            {
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            return BadRequest();
        }
    }
}
