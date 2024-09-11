using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WishList.Services;

public class JwtTokenGenerator
{
	private readonly IConfiguration _configuration;

	public JwtTokenGenerator(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public string GenerateToken(string userId)
	{
		var jwtSettings = _configuration.GetSection("JwtSettings");

		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, userId),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: jwtSettings["Issuer"],
			audience: jwtSettings["Audience"],
			claims: claims,
			expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"])),
			signingCredentials: creds);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
