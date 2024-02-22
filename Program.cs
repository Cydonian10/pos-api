using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// configurando la base de datos
builder.Services.AddDbContext<DataContext>((options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// * Services
//builder.Services.AddSingleton<IAuthorizationHandler,EmployedHandler>();
builder.Services.AddHttpContextAccessor();


// * Identity
builder.Services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<DataContext>()
        .AddDefaultTokenProviders();

JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();


// * Token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                                  Encoding.UTF8.GetBytes(builder.Configuration["llavejwt"]!)),
            ClockSkew = TimeSpan.Zero
        });




builder.Services.AddAuthorization(options =>
{
 
    options.AddPolicy(
        "ManejadorProductos",
        policy => 
            policy
            .RequireClaim(ClaimTypes.Role, ["Admin","Empleado"])
        );

    options.AddPolicy(
      "ManejadorVentas",
      policy =>
          policy
          .RequireClaim(ClaimTypes.Role, ["Admin", "Empleado"])
      );

    options.AddPolicy(
        "NivelAccesoTotal",
        policy => 
            policy
            .RequireClaim("NiveAcceso", ["100"])
        );
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
