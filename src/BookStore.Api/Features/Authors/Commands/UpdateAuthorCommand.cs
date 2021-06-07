using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using BookStore.ApplicationCore.Entities;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Features.Authors.Commands
{
    public class UpdateAuthorCommand : IRequest<bool>
    {
        public Guid AuthorId { get; set; }

        public string Name { get; set; }

        public class UpdateCommandValidator : AbstractValidator<UpdateAuthorCommand>
        {
            public UpdateCommandValidator()
            {
                RuleFor(x => x.AuthorId).NotEmpty();
                RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
            }
        }

        public class UpdateCommandHandler : IRequestHandler<UpdateAuthorCommand, bool>
        {
            private readonly EfContext _context;

            public UpdateCommandHandler(EfContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
            {
                AuthorEntity entity = await _context.Author.Where(a => a.AuthorId == request.AuthorId).FirstOrDefaultAsync();
                if (entity == null)
                    return false;

                entity.Name = request.Name;

                await _context.SaveChangesAsync();
                
                return true;
            }
        }
    }
}
