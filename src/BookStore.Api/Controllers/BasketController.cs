using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using BookStore.Application.Features.Baskets.Commands;
using BookStore.Application.Features.Baskets.Queries;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBasket(CreateBasketCommand data)
        {
            Guid newId = await _mediator.Send(data);
            return newId;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> GetBasketById(Guid id)
        {
            return await _mediator.Send(new GetBasketDetailQuery { BasketId = id });
        }
    }
}
