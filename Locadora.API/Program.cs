using AutoMapper;
using Locadora.API.Context;
using Locadora.API.Repository;
using Locadora.API.Repository.Interfaces;
using Locadora.API.Services;
using Locadora.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Locadora.API.Dtos.Validations;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => {
    options.Filters.Add<ResultService>();
});
//builder.Services.AddSingleton<IActionResultExecutor<ObjectResult>, CustomTest>();
// builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<UserDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateBookDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateBookDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PublisherDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdatePublisherDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RentalDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateRentalDtoValidator>();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = "BookStore",
        Description = "Books 📚",
    });
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlCommentsFile));

});
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlite("Data Source=Locadora.db");
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(options => {
    options.AddPolicy("AllowLocalhost8080",
        builder => {
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
//app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.UseCors("AllowLocalhost8080");

app.Run();
