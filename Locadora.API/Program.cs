using AutoMapper;
using Locadora.API.Data;
using Locadora.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

internal class Program {
    private static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers()
            .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new OpenApiInfo {
                Version = "1.0",
                Title = "Locadora de Livros",
                Description = "Locadora massa da WDA",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact {
                    Name = "Example Contact",
                    Url = new Uri("https://example.com/contact")
                },
                License = new OpenApiLicense {
                    Name = "Example License",
                    Url = new Uri("https://example.com/license")
                },

            });
            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlCommentsFile));
            
        });
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlite("Data Source=Locadora.db"));
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddScoped<IRepository, Repository>();
        builder.Services.AddScoped<PublishersServices>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.InjectStylesheet("/swagger-ui/custom.css");
                options.DocExpansion(DocExpansion.None);
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}