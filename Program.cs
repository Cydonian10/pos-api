using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Services.StoreImage;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIAutores", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

});


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
builder.Services.AddTransient<IStoreImageService,LocalStoreImageService>();


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
            .RequireClaim(ClaimTypes.Role, ["Admin", "Empleado"])
        );

    options.AddPolicy(
     "VisualizarAdmin",
     policy =>
         policy
         .RequireClaim(ClaimTypes.Role, ["Admin"])
     );


    options.AddPolicy(
      "ManejadorVentas",
      policy =>
          policy
          .RequireClaim(ClaimTypes.Role, ["Empleado","Vendedor"])
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
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
        .WithExposedHeaders(new string[] { "totalRegistros" });
        
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
