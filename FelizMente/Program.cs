
using FelizMente.Data;
using FelizMente.Model;
using FelizMente.Validator;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FelizMente
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectionString = builder.Configuration
                .GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(connectionString));

            builder.Services.AddTransient<IValidator<Tema>, TemaValidator>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });


            var app = builder.Build();

            using (var scope = app.Services.CreateAsyncScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureCreated();
            }
            // Gosto de bolo 
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("MyPolicy");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}