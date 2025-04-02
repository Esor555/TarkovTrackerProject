using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace TTBusinesLogic.DAL
{
	public class LoginService
	{
		private readonly IConfiguration _configuration;

		public LoginService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public LoginService()
		{

		}

		public LoginGet Authenticate(LoginSend login)
		{
			// Dummy user validation (Replace with database validation)
			if (login.Username != "admin" || login.Password != "password")
				return null;

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]!);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, login.Username!)
				}),
				Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtConfig:TokenValidityMins"]!)),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
				Issuer = _configuration["JwtConfig:Issuer"],
				Audience = _configuration["JwtConfig:Audience"]
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return new LoginGet
			{
				UserName = login.Username,
				AccesToken = tokenHandler.WriteToken(token),
				ExpiresIn = int.Parse(_configuration["JwtConfig:TokenValidityMins"]!)
			};
		}
	}
}