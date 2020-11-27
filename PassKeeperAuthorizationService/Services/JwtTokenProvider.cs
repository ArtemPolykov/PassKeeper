using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PassKeeperAuthorizationService.Configuration;
using PassKeePerLib.Models;

namespace PassKeeperAuthorizationService.Services
{
	public class JwtTokenProvider : IUserTwoFactorTokenProvider<Users>
	{
        private readonly TokenParametres _tokenParametres;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        // Создать новый токен для пользователя
        private JwtSecurityToken CreateNewToken(Users user)
        {
            var now = DateTime.UtcNow;
            var exp = now.AddMinutes(_tokenParametres.LifeTimeMinutes);
            var signingCredentials = new SigningCredentials(_tokenParametres.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            
            // Необходимо чтобы из токена можно было извлекать UserName
            var claims = new List<Claim>() { new Claim("UserName", user.UserName) };

            return new JwtSecurityToken(
                issuer: _tokenParametres.Issuser,
                audience: _tokenParametres.Audience,
                claims: claims,
                notBefore: now,
                expires: exp,
                signingCredentials: signingCredentials
            );
        }

        public JwtTokenProvider(TokenParametres tokenParametres)
        {
            _tokenParametres = tokenParametres;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

		public async Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<Users> manager, Users user)
		{
			var resultTask = new Task<bool>(() => true, TaskCreationOptions.AttachedToParent);
            resultTask.Start();

            return await resultTask;
		}

        // Создает новый токен и записывает его в базу
		public async Task<string> GenerateAsync(string purpose, UserManager<Users> manager, Users user)
		{
			var token = _tokenHandler.WriteToken(CreateNewToken(user));
            await manager.SetAuthenticationTokenAsync(
                user, _tokenParametres.LoginProvider, _tokenParametres.TokenName, token);

            return token;
		}

        // Сравнивает токен со значением в базе
		public async Task<bool> ValidateAsync(string purpose, string token, UserManager<Users> manager, Users user)
		{
            var tokenFromBase = await manager.GetAuthenticationTokenAsync(
                user, _tokenParametres.LoginProvider, _tokenParametres.TokenName);

            return token == tokenFromBase;
		}
	}
}
