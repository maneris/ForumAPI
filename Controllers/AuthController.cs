﻿using ForumAPI.Auth;
using ForumAPI.Auth.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForumAPI.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/v1")]
    public class AuthController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ForumRestUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(UserManager<ForumRestUser> userManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = await _userManager.FindByNameAsync(registerUserDto.UserName);
            if (user != null)
                return BadRequest("Request invalid.");

            var newUser = new ForumRestUser
            {
                Email = registerUserDto.Email,
                UserName = registerUserDto.UserName
            };
            var createUserResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);
            if (!createUserResult.Succeeded)
                return BadRequest("Could not create a user.");

            await _userManager.AddToRoleAsync(newUser, ForumRoles.AuthForumUser);

            return CreatedAtAction(nameof(Register), new UserDto(newUser.Id, newUser.UserName, newUser.Email));
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
                return BadRequest("User name or password is invalid.");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
                return BadRequest("User name or password is invalid.");

            // valid user
            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);

            return Ok(new SuccessfulLoginDto(accessToken));
        }
        [HttpPost]
        [Route("anonBrowse")]
        public async Task<ActionResult> AnonBrowse()
        {
            var roles = ForumRoles.AnonUsers;
            var accessToken = _jwtTokenService.CreateGuestAccessToken(roles);

            return Ok(new SuccessfulLoginDto(accessToken));
        }
    }
}
