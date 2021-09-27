using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using BookStore.Application.Token;
using BookStore.Application.Token.Models;

namespace BookStore.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public UserController()
        {
        }

        [HttpGet("Logged")]
        public async Task<ActionResult<AuthUserDTO>> LoggedUser()
        {
            AuthUserDTO loggedUser = new AuthUserDTO()
            {
                username = "test",
                email = "test@gmail.com",
                role = "A",
                completeName = "Test User"
            };

            return loggedUser;
        }

        public class AuthUserDTO
        {
            public Guid? id { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public string role { get; set; }
            public string completeName { get; set; }
        }
    }
}
