﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartHospitalSystem.Core.Enums;
using SmartHospitalSystem.Core.Interfaces.Configurations;
using SmartHospitalSystem.Core.Interfaces.Managers;
using SmartHospitalSystem.Core.Models;

namespace SmartHospitalSystem.Api.Controllers
{
    /// <summary>
    /// The controller allows managing the authorization
    /// </summary>
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokensController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IAuthorizationManager _authorizationManager;
        private readonly IPasswordHasher<UserProfile> _passwordHasher;
        private readonly ITokenConfiguration _tokenConfiguration;


        /// <summary>
        /// TokensController constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="authorizationManager"></param>
        /// <param name="passwordHasher"></param>
        public TokensController(IUserManager userManager, IAuthorizationManager authorizationManager,
            IPasswordHasher<UserProfile> passwordHasher, ITokenConfiguration tokenConfiguration)
        {
            _userManager = userManager;
            _authorizationManager = authorizationManager;
            _passwordHasher = passwordHasher;
            _tokenConfiguration = tokenConfiguration;
        }

        /// <summary>
        /// Login user via login and password
        /// </summary>
        /// <param name="loginModel">Model for authorization</param>
        /// <response code="404">Profile with give login not found</response>
        /// <response code="400">Invalid login model</response>
        /// <response code="200">Returns new token</response>
        /// <returns>Token</returns>
        [AllowAnonymous]
        [Route("login")]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 200)]
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody]UserLoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest("Invalid model");
            }

            var profile = await _userManager.GetByLogin(loginModel.Username);

            if (profile != null)
            {
                var user = _authorizationManager.Authenticate(loginModel, profile);

                if (user != null)
                {
                    var result = GenerateToken(profile);
                    return Ok(new { User = user.Id, Token = result });
                }
                return Unauthorized();
            }

            return NotFound("Profile with give login not found");
        }


        /// <summary>
        /// Seeds basic users
        /// </summary>
        /// <response code="200">Basic users seeded</response>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("seed"), HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> SeedAdmin()
        {
            var profiles = new[]
            {
                new UserProfile{
                    Login = "mozgokluy",
                    CreatedDate = DateTime.Now,
                    Email = "mozgopluy@gmail.com",
                    FirstName = "Danyil",
                    LastName = "Hryhoriev",
                    Password = "12345678",
                    Roles = new List<UserRoleEnum>
                    {
                        UserRoleEnum.Admin
                    }
                },
                new UserProfile
                {
                    Login = "user1",
                    CreatedDate = DateTime.Now,
                    Email = "qwerty@gmail.com",
                    FirstName = "Danyil",
                    LastName = "Hryhoriev",
                    Password = "12345678",
                    Roles = new List<UserRoleEnum>
                    {
                        UserRoleEnum.Registry
                    }
                }
            };

            foreach (var user in profiles)
            {
                user.Password = _passwordHasher.HashPassword(user, user.Password);
                await _userManager.InsertProfileAsync(user);
            }

            return Ok(profiles);
        }

        private string GenerateToken(UserProfile profile)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, profile.Id),
            };

            claims.AddRange(profile.Roles.Select(x => new Claim(ClaimTypes.Role, x.ToString().ToLower())));


            var token = new JwtSecurityToken(
                _tokenConfiguration.Issuer,
                _tokenConfiguration.Audience,
                claims,
                expires: DateTime.Now.AddDays(_tokenConfiguration.TokenExpiresIn),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
