using BookStore.ApplicationCore.Entities;
using BookStore.Infrastructure.Data;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Api.Features.Books.Commands
{
    public class CreateBookCommand : IRequest<Guid>
    {
        public string Title { get; set; }

        public DateTime? PublicationDate { get; set; }

        public Guid AuthorId { get; set; }

        public class CreateCommandValidator : AbstractValidator<CreateBookCommand>
        {
            public CreateCommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty().MaximumLength(500);
                RuleFor(x => x.AuthorId).NotEmpty();
            }
        }

        public class CreateCommandHandler : IRequestHandler<CreateBookCommand, Guid>
        {
            private readonly EfContext _context;

            public CreateCommandHandler(EfContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
            {
                BookEntity book = new BookEntity
                {
                    Title = request.Title,
                    PublicationDate = request.PublicationDate,
                    AuthorId = request.AuthorId
                };

                _context.Book.Add(book);

                int value = await _context.SaveChangesAsync();
                if (value > 0)
                    return book.BookId;

                throw new Exception("Failed to save the book");
            }
        }
    }
}
