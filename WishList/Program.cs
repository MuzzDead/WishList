
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using WishList.ApplicationDbContext;
using WishList.Models;
using WishList.Repositories;
using WishList.Services;
using WishList.Swagger;

namespace WishList
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'Default' not found.");


			// Add services to the container.


			builder.Services.AddCors(options =>
			{
				options.AddPolicy(name: "_wishOrigin", policy =>
				{
					policy.WithOrigins("http://localhost:3000")
					.AllowAnyHeader()
					.AllowAnyMethod();
				});
			});

			builder.Services.AddControllers();

			builder.Services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(connectionString));

			var jwtSettings = builder.Configuration.GetSection("JwtSettings");

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtSettings["Issuer"],
					ValidAudience = jwtSettings["Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]))
				};
			});


			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();


			builder.Services.AddTransient<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddTransient<IWishRepository, WishRepository>();
			builder.Services.AddScoped<IWishService, WishService>();
			builder.Services.AddSingleton<JwtTokenService>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseCors("_wishOrigin");

			app.MapControllers();

			app.Run();
		}
	}
}
