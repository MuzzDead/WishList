
using Microsoft.EntityFrameworkCore;
using WishList.ApplicationDbContext;
using WishList.Models;
using WishList.Repositories;
using WishList.Services;

namespace WishList
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();

			builder.Services.AddDbContext<WishDbContext>(options => 
				options.UseInMemoryDatabase("WishDb"));

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddTransient<IWishRepository, WishRepository>();
			builder.Services.AddScoped<IWishService, WishService>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
