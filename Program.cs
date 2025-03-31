using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = Env.GetString("DATABASE_STRING");

/* builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString)); */

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapGet("", () => Results.Redirect("/scalar/v1"));

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();