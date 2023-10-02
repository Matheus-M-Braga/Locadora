using AutoMapper;
using Locadora.API.Data;
using Locadora.API.Repository;
using Locadora.API.Services;
using Locadora.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers()
            .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "1.0",
                Title = "BookStore",
                Description = "Books 📚",
                //TermsOfService = new Uri("https://example.com/terms"),
                //Contact = new OpenApiContact
                //{
                //    Name = "Example Contact",
                //    Url = new Uri("https://example.com/contact")
                //},
                //License = new OpenApiLicense
                //{
                //    Name = "Example License",
                //    Url = new Uri("https://example.com/license")
                //},
            });
            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlCommentsFile));

        });
        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite("Data Source=Locadora.db");
            // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost8080",
                builder =>
                {
                    builder.WithOrigins("http://localhost:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
        });
        // Repos
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
        builder.Services.AddScoped<IRentalRepository, RentalRepository>();
        // Services
        builder.Services.AddScoped<IUsersService, UsersService>();
        builder.Services.AddScoped<IBooksService, BooksService>();
        builder.Services.AddScoped<IPublishersService, PublishersService>();
        builder.Services.AddScoped<IRentalsService, RentalsService>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.InjectStylesheet("/swagger-ui/custom.css");
                options.DocExpansion(DocExpansion.None);
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseCors("AllowLocalhost8080");

        app.Run();
    }
}