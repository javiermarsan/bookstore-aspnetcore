using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using BookStore.Application.Common.Interfaces;

namespace BookStore.Application.Authors.Commands
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
            private readonly ICatalogRepository<Author> _repository;

            public CreateCommandHandler(ICatalogRepository<Author> repository)
            {
                _repository = repository;
            }

            public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
            {
                Author entity = new Author
                {
                    Name = request.Name
                };

                _repository.Add(entity);

                int value = await _repository.SaveAsync();
                if (value > 0)
                    return entity.AuthorId;

                throw new Exception("Failed to save the author");
            }
        }
    }
}
