using System.Text.Json.Serialization;
using Application;
using Domain.Usuarios.Models;
using Domain.Usuarios.Models.ValueObjects;
using Infraestructure;
using Infraestructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using WebApi.Extensions;
using WebAPI;
using WebAPI.Configuration;
using WebAPI.Configuration.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<Usuario, IdentityRole<IdentityId>>()
.AddRoles<IdentityRole<IdentityId>>()
.AddEntityFrameworkStores<InkboardDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddWebApi().AddApplication().AddInfraestructure().AddPersistence(builder.Configuration);

builder.Services.AddSwaggerBearerTokenSupport();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(
    options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);


builder.Services.AddProblemDetails();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();

    app.UseSwaggerUI();
}


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseHttpsRedirection();
app.UseExceptionHandler();



app.UseStatusCodePages();


app.Run();