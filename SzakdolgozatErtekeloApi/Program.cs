
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QuestPDF.Infrastructure;
using SzakdolgozatErtekeloApi.Services;

namespace SzakdolgozatErtekeloApi
{
    public class Program
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<PdfService>();
            services.AddScoped<WordService>();
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            QuestPDF.Settings.License = LicenseType.Community;

            // Add services to the container.
            ConfigureServices(builder.Services);
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            var app = builder.Build();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseCors("AllowAllOrigins");

            app.Run();
        }
    }
}
