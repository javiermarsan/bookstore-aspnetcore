using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BookStore.ApplicationCore.Entities;
using BookStore.Infrastructure.Data;

namespace BookStore.Api.Features.Books.Commands
{
    public class UpdateBookCommand : IRequest<bool>
    {
        public Guid BookId { get; set; }

        public string Title { get; set; }

        public DateTime? PublicationDate { get; set; }

        public Guid AuthorId { get; set; }

        public class UpdateCommandValidator : AbstractValidator<UpdateBookCommand>
        {
            public UpdateCommandValidator()
            {
                RuleFor(x => x.BookId).NotEmpty();
                RuleFor(x => x.Title).NotEmpty().MaximumLength(500);
                RuleFor(x => x.AuthorId).NotEmpty();
            }
        }

        public class UpdateCommandHandler : IRequestHandler<UpdateBookCommand, bool>
        {
            private readonly EfContext _context;

            public UpdateCommandHandler(EfContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
            {
                BookEntity entity = await _context.Book.Where(a => a.BookId == request.BookId).FirstOrDefaultAsync();
                if (entity == null)
                    return false;

                entity.Title = request.Title;
                entity.PublicationDate = request.PublicationDate;
                entity.AuthorId = request.AuthorId;

                await _context.SaveChangesAsync();
                
                return true;
            }
        }
    }
}
