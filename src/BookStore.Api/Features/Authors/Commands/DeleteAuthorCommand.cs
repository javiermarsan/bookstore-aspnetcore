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
using BookStore.ApplicationCore.Interfaces;

namespace BookStore.Api.Features.Authors.Commands
{
    public class DeleteAuthorCommand : IRequest<bool>
    {
        public Guid AuthorId { get; set; }

        public class DeleteCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
        {
            private readonly IRepository<Author> _repository;

            public DeleteCommandHandler(IRepository<Author> repository)
            {
                _repository = repository;
            }

            public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
            {
                Author entity = await _repository.QueryContext().Where(a => a.AuthorId == request.AuthorId).FirstOrDefaultAsync();
                if (entity == null)
                    return false;

                _repository.Delete(entity);

                int value = await _repository.SaveAsync();
                if (value > 0)
                    return true;

                throw new Exception("Failed to delete the author");
            }
        }
    }
}
