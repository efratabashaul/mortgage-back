using DataContext;
using Repositories.Interface;
using Service;
using Repositories.Entities;
using Service.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHttpClient();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
         {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer =builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
        });

        builder.Services.AddControllers();
        builder.Services.AddScoped<DropboxService>(sp => new DropboxService(
        builder.Configuration["Dropbox:AccessToken"],
        builder.Configuration["Dropbox:RefreshToken"],
        builder.Configuration["Dropbox:AppKey"],
        builder.Configuration["Dropbox:AppSecret"]
));

        builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();


        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

            // הגדרת אבטחה ב-Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
            new string[] { }
        }});
        });



        builder.Services.AddServices();
        builder.Services.AddDbContext<IContext, Db>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost4200",
                builder => builder.WithOrigins("http://localhost:4200")
                                  .AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .WithExposedHeaders("Content-Disposition")
                                  .AllowCredentials()); // תוסיפי את AllowCredentials כאן


        });

        builder.Services.Configure<MailKitOptions>(builder.Configuration.GetSection("EmailSettings"));

        builder.Services.AddSingleton<Repositories.Interface.IMailKitProvider, Repositories.Repositories.MailKitProvider>();

        builder.Services.AddTransient<Service.Interfaces.IEmailService, Service.Services.EmailService>();
        builder.Services.AddSignalR();

        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowLocalhost4200");

        app.UseAuthorization();



        app.MapControllers();

        app.Run();
    }
}
