using Microsoft.EntityFrameworkCore;
using System;
using WorkoutTracker.Data.Contexts;
using WorkoutTracker.Logic.Services;

namespace WorkoutTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddDbContext<WorkoutContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WorkoutDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");
                options.UseLazyLoadingProxies();
            });

            // Add services to the container.
            builder.Services.AddScoped<WorkoutTypeService>();
            builder.Services.AddScoped<WorkoutSessionService>();
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            builder.Services.AddControllers();
            builder.Services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            var app = builder.Build();
            app.UseCors("AllowAngular");
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
