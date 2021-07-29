using BookStore.Domain.Entities;
using BookStore.Application.Common.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Commands
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
            private readonly ICatalogRepository<Book> _repository;

            public CreateCommandHandler(ICatalogRepository<Book> repository)
            {
                _repository = repository;
            }

            public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
            {
                Book book = new Book
                {
                    Title = request.Title,
                    PublicationDate = request.PublicationDate,
                    AuthorId = request.AuthorId
                };

                _repository.Add(book);

                int value = await _repository.SaveAsync();
                if (value > 0)
                    return book.BookId;

                throw new Exception("Failed to save the book");
            }
        }
    }
}
