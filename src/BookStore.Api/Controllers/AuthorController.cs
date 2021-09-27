using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using BookStore.Application.Authors.Commands;
using BookStore.Application.Authors.Queries;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpPost]
        [HttpPost("Create")]
        public async Task<ActionResult<Guid>> CreateAuthor(CreateAuthorCommand data)
        {
            Guid newId = await _mediator.Send(data);
            return newId;
        }

        //[HttpPut]
        [HttpPost("Update")]
        public async Task<ActionResult> UpdateAuthor(UpdateAuthorCommand data)
        {
            bool found = await _mediator.Send(data);
            if (!found)
                return NotFound();

            return NoContent();
        }

        //[HttpDelete]
        [HttpPost("Delete")]
        public async Task<ActionResult> DeleteAuthor(DeleteAuthorCommand data)
        {
            bool found = await _mediator.Send(data);
            if (!found)
                return NotFound();

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAuthors()
        {
            return await _mediator.Send(new GetAuthorListQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthorById(Guid id)
        {
            return await _mediator.Send(new GetAuthorDetailQuery { AuthorId = id });
        }
    }
}
