using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WishList.Services;

public class JwtTokenService
{
	private readonly IConfiguration _configuration;

	public JwtTokenService(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public string GenerateToken(Guid userId, string username)
	{
		var jwtSettings = _configuration.GetSection("JwtSettings");

		var claims = new[]
		{
		new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
		new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
		new Claim("username", username)  // Додаємо username в claims
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

	/*public Guid GetUserIdFromTeken(ClaimsPrincipal user) // BUGS!!!!!
	{
		if (user == null || !user.Identity.IsAuthenticated)
		{
			throw new UnauthorizedAccessException("User is not authenticated.");
		}

		// Отримуємо User ID із токену
		var userIdString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		// Перевіряємо, чи User ID є правильним GUID
		if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
		{
			throw new FormatException("Invalid GUID format in token.");
		}

		return userId;
	}*/
}
