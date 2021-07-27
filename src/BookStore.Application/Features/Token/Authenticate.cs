using AutoMapper;
using BookStore.Application.Entities;
using BookStore.Application.Exceptions;
using BookStore.Application.Features.Token.Models;
using BookStore.Application.Interfaces;
using BookStore.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Features.Token
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
            private readonly HttpContext _httpContext;

            public AuthenticateCommandHandler(ITokenService tokenService,
                                  IMapper mapper,
                                  IHttpContextAccessor httpContextAccessor)
            {
                _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _httpContext = (httpContextAccessor != null) ? httpContextAccessor.HttpContext : throw new ArgumentNullException(nameof(httpContextAccessor));

            }

            public async Task<CommandResponse> Handle(AuthenticateCommand command, CancellationToken cancellationToken)
            {
                CommandResponse response = new CommandResponse();

                string ipAddress = _httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

                TokenResponse tokenResponse = await _tokenService.Authenticate(command, ipAddress);
                if (tokenResponse == null)
                    throw new InvalidCredentialsException();

                response.Resource = tokenResponse;
                return response;
            }
        }
    }
}
