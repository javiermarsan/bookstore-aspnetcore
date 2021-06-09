using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookStore.ApplicationCore.Entities;
using BookStore.Infrastructure.Data;

namespace BookStore.Api.Features.Authors.Commands
{
    public class CreateAuthorCommand : IRequest<Guid>
    {
        public string Name { get; set; }


        public class CreateCommandValidator : AbstractValidator<CreateAuthorCommand>
        {
            public CreateCommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
            }
        }

        public class CreateCommandHandler : IRequestHandler<CreateAuthorCommand, Guid>
        {
            private readonly EfContext _context;

            public CreateCommandHandler(EfContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
            {
                Author entity = new Author
                {
                    Name = request.Name
                };

                _context.Author.Add(entity);

                int value = await _context.SaveChangesAsync();
                if (value > 0)
                    return entity.AuthorId;

                throw new Exception("Failed to save the author");
            }
        }
    }
}
