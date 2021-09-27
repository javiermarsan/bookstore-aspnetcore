using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using BookStore.Application.Books.Commands;
using BookStore.Application.Books.Queries;

namespace BookStore.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpPost]
        [HttpPost("Create")]
        public async Task<ActionResult<Guid>> CreateBook(CreateBookCommand data)
        {
            Guid newId = await _mediator.Send(data);
            return newId;
        }

        //[HttpPut]
        [HttpPost("Update")]
        public async Task<ActionResult> UpdateBook(UpdateBookCommand data)
        {
            bool found = await _mediator.Send(data);
            if (!found)
                return NotFound();

            return NoContent();
        }

        //[HttpDelete]
        [HttpPost("Delete")]
        public async Task<ActionResult> DeleteBook(DeleteBookCommand data)
        {
            bool found = await _mediator.Send(data);
            if (!found)
                return NotFound();

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetBooks()
        {
            return await _mediator.Send(new GetBookListQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(Guid id)
        {
            return await _mediator.Send(new GetBookDetailQuery { BookId = id });
        }
    }
}
