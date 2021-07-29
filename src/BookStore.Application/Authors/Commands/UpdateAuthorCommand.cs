using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BookStore.Application.Common.Interfaces;

namespace BookStore.Application.Authors.Commands
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
            private readonly ICatalogRepository<Author> _repository;

            public UpdateCommandHandler(ICatalogRepository<Author> repository)
            {
                _repository = repository;
            }

            public async Task<bool> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
            {
                Author entity = await _repository.QueryContext().Where(a => a.AuthorId == request.AuthorId).FirstOrDefaultAsync();
                if (entity == null)
                    return false;

                entity.Name = request.Name;
                _repository.Update(entity);

                await _repository.SaveAsync();
                
                return true;
            }
        }
    }
}
