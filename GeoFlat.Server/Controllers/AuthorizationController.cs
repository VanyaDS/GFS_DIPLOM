using AutoMapper;
using GeoFlat.Server.Helpers;
using GeoFlat.Server.Models.Authorization;
using GeoFlat.Server.Models.Database.Entities;
using GeoFlat.Server.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GeoFlat.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private IOptions<AuthorizationOptions> _authOptions;
        private readonly ILogger<AuthorizationController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuthorizationController(
            IOptions<AuthorizationOptions> options,
            ILogger<AuthorizationController> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _authOptions = options;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(Login request)
        {

            var account = await AuthenticateUserAsync(request.Email, request.Password);
            if (account is not null)
            {
                var token = GenerateJWT(account);
               
                return Ok(new
                {
                    access_token = token
                });
            }
            return Unauthorized();
        }
        private async Task<Account> AuthenticateUserAsync(string email, string password)
        {
          return await _unitOfWork.Accounts.FindSingleOrDefaultAsync(x => x.Email.Equals(email)
                                  && x.Password.Equals(HashingMD5.GetHashStringMD5(password)));
        }
        private string GenerateJWT(Account account)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email , account.Email),
                new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString())
            };

            claims.Add(new Claim("role", account.Role));

            var token = new JwtSecurityToken(authParams.Issuer, authParams.Audience, claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

