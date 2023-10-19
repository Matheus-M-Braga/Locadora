using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using Library.Business.Services;
using Library.Business.Interfaces.IRepository;
using Library.Data.Repository;
using Library.Business.Interfaces.IServices;
using Library.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "BookStore",
        Description = "Books 📚",
    });
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlCommentsFile));

});
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite("Data Source=Locadora.db");
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
//app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.UseCors("AllowLocalhost8080");

app.Run();
