
using Microsoft.EntityFrameworkCore;
using MoviesApiDevCreed.Data;
using MoviesApiDevCreed.Repository.MainRepository;
using MoviesApiDevCreed.Repository.ModelRepositories;
using MoviesApiDevCreed.Service.IService;
using MoviesApiDevCreed.Service.MainService;
using MoviesApiDevCreed.UnitOfWork;

namespace MoviesApiDevCreed
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connection = builder.Configuration.GetConnectionString("connection") ?? throw new InvalidOperationException("No Connection");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));
            builder.Services.AddCors();
            builder.Services.AddScoped<IUnitOfWork, MyUnitOfWork>();
            builder.Services.AddScoped<IGenreService, GenresService>();
            builder.Services.AddScoped<IMoviesService, MoviesService>();


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}