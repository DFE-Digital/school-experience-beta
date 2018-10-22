using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace SchoolExperienceUi.Services
{
    internal class DfePublicSignInJwtAuthentication : IDfePublicSignInAuthentication
    {
        private const string Issuer = "SchoolExperienceIssuer";
        private const string Audience = "SchoolExperienceConsumer";
        private readonly DfePublicSignInJwtAuthenticationOptions _options;
        private readonly SecurityKey _securityKey;

        private readonly SigningCredentials _credentials;

        public DfePublicSignInJwtAuthentication(IOptions<DfePublicSignInJwtAuthenticationOptions> options)
        {
            _options = options.Value;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.ClientSecret));
            _credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.EcdsaSha512Signature);
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var header = new JwtHeader(_credentials);

            var now = DateTime.UtcNow;

            var payload = new JwtPayload(Issuer, Audience, claims, now, now + _options.TokenExpiresAfter);

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            // Token to String so you can use it in your client
            var tokenString = handler.WriteToken(secToken);

            return tokenString;
        }

        public IEnumerable<Claim> VerifyToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var principal = handler.ValidateToken(token, GetValidationParameters(), out var validatedToken);

            return principal.Claims;
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true, // Because there is no expiration in the generated token
                ValidateAudience = true, // Because there is no audience in the generated token
                ValidateIssuer = true,   // Because there is no issuer in the generated token
                ValidIssuer = Issuer,
                ValidAudience = Audience,
                IssuerSigningKey = _securityKey, // The same key as the one that generate the token
            };
        }
    }
}
