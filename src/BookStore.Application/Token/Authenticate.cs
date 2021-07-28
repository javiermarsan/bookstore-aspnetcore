using AutoMapper;
using BookStore.Application.Token.Models;
using BookStore.Application.Baskets.Services;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Common.Exceptions;

namespace BookStore.Application.Token
{
    public class Authenticate
    {
        public class AuthenticateCommand : TokenRequest, IRequest<CommandResponse>
        {
        }

        public class CommandResponse
        {
            public TokenResponse Resource { get; set; }
        }

        public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
        {
            public AuthenticateCommandValidator()
            {
                RuleFor(x => x.Username)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty();

                RuleFor(x => x.Password)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty();
            }

        }

        public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, CommandResponse>
        {
            private readonly ITokenService _tokenService;
            private readonly IMapper _mapper;

            public AuthenticateCommandHandler(ITokenService tokenService, IMapper mapper)
            {
                _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<CommandResponse> Handle(AuthenticateCommand command, CancellationToken cancellationToken)
            {
                CommandResponse response = new CommandResponse();

                TokenResponse tokenResponse = await _tokenService.Authenticate(command);
                if (tokenResponse == null)
                    throw new InvalidCredentialsException();

                response.Resource = tokenResponse;
                return response;
            }
        }
    }
}
