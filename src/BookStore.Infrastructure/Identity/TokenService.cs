using BookStore.Application.Features.Token.Models;
using BookStore.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Identity
{
    /// <inheritdoc cref="ITokenService" />
    public class TokenService : ITokenService
    {
        public const string ServerSigningPassword = "vUQPjQaSy2cg-HJcE@hg9meLBv2gyLa5";

        private readonly Token _token;
        private readonly IConfiguration _configuration;

        /// <inheritdoc cref="ITokenService" />
        public TokenService(
            IOptions<Token> tokenOptions,
            IConfiguration configuration)
        {
            _token = tokenOptions.Value;
            _configuration = configuration;
        }

        /// <inheritdoc cref="ITokenService.Authenticate(TokenRequest, string)"/>
        public async Task<TokenResponse> Authenticate(TokenRequest request, string ipAddress)
        {
            if (await IsValidUser(request.Username, request.Password))
            {
                ApplicationUser user = await GetUserByEmail(request.Username);

                if (user != null && user.IsEnabled)
                {
                    string jwtToken = GenerateJwtToken(user);

                    //RefreshToken refreshToken = GenerateRefreshToken(ipAddress);
                    //user.RefreshTokens.Add(refreshToken);

                    return new TokenResponse(user,
                                             "",
                                             jwtToken
                                             //""//refreshToken.Token
                                             );
                }
            }

            return null;
        }

        public Task<TokenResponse> RefreshToken(string refreshToken, string ipAddress)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="ITokenService.IsValidUser(string, string)" />
        public async Task<bool> IsValidUser(string username, string password)
        {
            ApplicationUser user = await GetUserByEmail(username);

            if (user == null)
            {
                // Username or password was incorrect.
                return false;
            }

            // TODO verify password

            return true;
        }

        /// <inheritdoc cref="ITokenService.GetUserByEmail(string)" />
        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            // TODO Database

            ApplicationUser user = new ApplicationUser()
            {
                Id = "1",
                Email = email,
                FirstName = "User",
                LastName = "Test",
                IsEnabled = true
            };

            return user;
        }

        /// <summary>
        ///     Issue JWT token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateJwtToken(ApplicationUser user)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // this guarantees the token is unique
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ServerSigningPassword));
            DateTime expires = DateTime.Now.AddMinutes(int.Parse(_configuration["AccessTokenDurationInMinutes"]));

            JwtSecurityToken jwtToken = new JwtSecurityToken(
                issuer: "Microsoft",
                audience: "Anyone",
                claims: claims,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return handler.WriteToken(jwtToken);
        }
    }
}
