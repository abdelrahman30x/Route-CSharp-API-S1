
using ECommerceG02.Domian.Contacts;
using ECommerceG02.Presistence.Contexts;
using ECommerceG02.Presistence.Seed;
using Microsoft.EntityFrameworkCore;

namespace ECommerceG02.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));
            }

           );

            builder.Services.AddScoped<IDataSeed, DataSeed>();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();
            var scoped = app.Services.CreateScope();
            var ObjectDataSeeding = scoped.ServiceProvider.GetRequiredService<IDataSeed>();
            ObjectDataSeeding.DataSeeding();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
