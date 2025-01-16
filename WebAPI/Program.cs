using Domain.Usuarios.Models;
using Domain.Usuarios.Models.ValueObjects;
using Infraestructure.Persistence;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<Usuario, IdentityRole<IdentityId>>()
.AddRoles<IdentityRole<IdentityId>>()
.AddEntityFrameworkStores<InkboardDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddPersistence(builder.Configuration);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", (UserManager<Usuario> manager) => {
    manager.FindByIdAsync(Guid.NewGuid().ToString());
    
    return "Hello World!";
});

app.UseHttpsRedirection();

app.Run();