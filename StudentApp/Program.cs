

using Microsoft.EntityFrameworkCore;
using StudentApp.Data;
using StudentApp.Repo;
using StudentApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<StudentAppContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("StudentAppContext") ?? throw new InvalidOperationException("Connection string 'StudentAppContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add interfaces (Context)
builder.Services.AddTransient<IService, Service>();
builder.Services.AddTransient<IRepo, Repo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API");
    s.RoutePrefix = "api";
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
