using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PassKeeperAuthorizationService.Configuration;
using PassKeePerLib.Models;

namespace PassKeeperAuthorizationService
{
	internal class JwtTokenProvider : IUserTwoFactorTokenProvider<Users>
	{
		private readonly JwtSecurityTokenHandler _tokenHandler;
		private readonly TokenParametres _tokenParametres;

		private JwtSecurityToken CreateNewToken(Users user)
        {
            var now = DateTime.UtcNow;
            var exp = now.AddMinutes(_tokenParametres.LifeTimeMinutes);
            var signingCredentials = new SigningCredentials(_tokenParametres.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("UserName", user.UserName)
            };

            var token = new JwtSecurityToken(
                issuer: _tokenParametres.Issuser,
                audience: _tokenParametres.Audience,
                claims: claims,
                notBefore: now,
                expires: exp,
                signingCredentials: signingCredentials
            );

            return token;
        }

		public JwtTokenProvider(TokenParametres tokenParametres)
		{
			_tokenParametres = tokenParametres;
			_tokenHandler = new JwtSecurityTokenHandler();
		}

		public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<Users> manager, Users user)
		{
			throw new System.NotImplementedException();
		}

		public async Task<string> GenerateAsync(string purpose, UserManager<Users> manager, Users user)
		{
			var t = new Task<string>(() => _tokenHandler.WriteToken(CreateNewToken(user)), TaskCreationOptions.AttachedToParent);
			t.Start();
			return await t;
		}

		public Task<bool> ValidateAsync(string purpose, string token, UserManager<Users> manager, Users user)
		{
			throw new System.NotImplementedException();
		}
	}
}