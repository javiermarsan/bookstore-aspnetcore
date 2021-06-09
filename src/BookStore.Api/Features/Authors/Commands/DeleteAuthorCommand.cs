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
    public class DeleteAuthorCommand : IRequest<bool>
    {
        public Guid AuthorId { get; set; }

        public class DeleteCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
        {
            private readonly EfContext _context;

            public DeleteCommandHandler(EfContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
            {
                Author entity = await _context.Author.Where(a => a.AuthorId == request.AuthorId).FirstOrDefaultAsync();
                if (entity == null)
                    return false;

                _context.Author.Remove(entity);

                int value = await _context.SaveChangesAsync();
                if (value > 0)
                    return true;

                throw new Exception("Failed to delete the author");
            }
        }
    }
}
