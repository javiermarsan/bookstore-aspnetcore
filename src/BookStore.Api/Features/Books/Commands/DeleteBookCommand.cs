using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BookStore.ApplicationCore.Entities;
using BookStore.ApplicationCore.Interfaces;
using BookStore.Infrastructure.Data;

namespace BookStore.Api.Features.Books.Commands
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public Guid BookId { get; set; }

        public class DeleteCommandHandler : IRequestHandler<DeleteBookCommand, bool>
        {
            private readonly IRepository<Book> _repository;

            public DeleteCommandHandler(IRepository<Book> repository)
            {
                _repository = repository;
            }

            public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
            {
                Book entity = await _repository.QueryContext().Where(a => a.BookId == request.BookId).FirstOrDefaultAsync();
                if (entity == null)
                    return false;

                _repository.Delete(entity);

                int value = await _repository.SaveAsync();
                if (value > 0)
                    return true;

                throw new Exception("Failed to delete the book");
            }
        }
    }
}
