﻿using FamousQuoteQuiz.Core;
using FamousQuoteQuiz.Core.Models;
using FamousQuoteQuiz.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ApiController
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="model">The credentials.</param>
        /// <returns>A JWT token.</returns>
        /// <response code="200">If the credentials have a match.</response>
        /// <response code="400">If the credentials don't match/don't meet the requirements.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(JwtModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginUserModel model) =>
            (await _usersService.Login(model))
            .Match(Ok, Error);

        /// <summary>
        /// Register.
        /// </summary>
        /// <param name="model">The user model.</param>
        /// <returns>A user model.</returns>
        /// <response code="201">A user was created.</response>
        /// <response code="400">Invalid input.</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel model) =>
            (await _usersService.Register(model))
            .Match(u => CreatedAtAction(nameof(Register), u), Error);
    }
}