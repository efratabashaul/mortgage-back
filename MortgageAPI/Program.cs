//using DataContext;
//using Repositories.Interface;
//using Service;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.DependencyInjection;
//using NETCore.MailKit.Core;
//using NETCore.MailKit;
//using Repositories.Entities;
//using Service.Interfaces;

//internal class Program
//{
//    private static void Main(string[] args)
//    {
//        var builder = WebApplication.CreateBuilder(args);

//        // Add services to the container.
//        builder.Services.AddControllers();
//        builder.Services.AddEndpointsApiExplorer();
//        builder.Services.AddSwaggerGen();
//        builder.Services.AddServices();
//        builder.Services.AddDbContext<IContext, Db>();

//        builder.Services.AddCors(options =>
//        {
//            options.AddPolicy("AllowLocalhost4200",
//                builder => builder.WithOrigins("http://localhost:4200")
//                                  .AllowAnyMethod()
//                                  .AllowAnyHeader());
//        });

//        //builder.Services.Configure<MailKitOptions>(builder.Configuration.GetSection("EmailSettings"));
//        //builder.Services.AddSingleton<IMailKitProvider,MailKitProvider>();
//        //builder.Services.AddTransient<IEmailService, EmailService>();

//        builder.Services.Configure<MailKitOptions>(builder.Configuration.GetSection("EmailSettings"));

//        // Register your own MailKitProvider implementation
//        builder.Services.AddSingleton<Repositories.Interface.IMailKitProvider, Repositories.Repositories.MailKitProvider>();

//        // Register EmailService
//        builder.Services.AddTransient<Service.Interfaces.IEmailService, Service.Services.EmailService>();


//        var app = builder.Build();

//        // Configure the HTTP request pipeline.
//        if (app.Environment.IsDevelopment())
//        {
//            app.UseSwagger();
//            app.UseSwaggerUI();
//        }

//        app.UseHttpsRedirection();
//        app.UseAuthorization();

//        app.UseCors("AllowLocalhost4200");


//        app.MapControllers();

//        app.Run();
//    }
//}






////using DataContext;
////using Repositories.Interface;
////using Service;
////using Repositories.Entities;
////using Service.Services;
////using System.Security.Claims;
////using Microsoft.AspNetCore.Authentication.JwtBearer;
////using Microsoft.IdentityModel.Tokens;
////using System.Text;
////using Microsoft.OpenApi.Models;

////internal class Program
////{
////    private static void Main(string[] args)
////    {
////        var builder = WebApplication.CreateBuilder(args);
////        builder.Services.AddHttpClient();

////        builder.Services.AddAuthentication(options =>
////        {
////            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
////            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
////        })
////        .AddJwtBearer(options =>
////         {
////            options.TokenValidationParameters = new TokenValidationParameters
////            {
////                ValidateIssuer = true,
////                ValidateAudience = true,
////                ValidateLifetime = true,
////                ValidateIssuerSigningKey = true,
////                ValidIssuer =builder.Configuration["Jwt:Issuer"],
////                ValidAudience = builder.Configuration["Jwt:Audience"],
////                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
////                };
////            });

////        builder.Services.AddAuthorization(options =>
////        {
////            options.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
////        });

////        builder.Services.AddControllers();
////        builder.Services.AddScoped<DropboxService>(sp => new DropboxService(
////        builder.Configuration["Dropbox:AccessToken"],
////        builder.Configuration["Dropbox:RefreshToken"],
////        builder.Configuration["Dropbox:AppKey"],
////        builder.Configuration["Dropbox:AppSecret"]
////));

////        builder.Services.AddEndpointsApiExplorer();
////        //builder.Services.AddSwaggerGen();


////        builder.Services.AddSwaggerGen(c =>
////        {
////            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

////            // הגדרת אבטחה ב-Swagger
////            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
////            {
////                In = ParameterLocation.Header,
////                Description = "Please insert JWT with Bearer into field",
////                Name = "Authorization",
////                Type = SecuritySchemeType.ApiKey,
////                Scheme = "Bearer"
////            });

////            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
////    {
////        new OpenApiSecurityScheme
////        {
////            Reference = new OpenApiReference
////            {
////                Type = ReferenceType.SecurityScheme,
////                Id = "Bearer"
////            }
////        },
////            new string[] { }
////        }});
////        });



////        builder.Services.AddServices();
////        builder.Services.AddDbContext<IContext, Db>();

////        builder.Services.AddCors(options =>
////        {
////            options.AddPolicy("AllowLocalhost4200",
////                builder => builder.WithOrigins("http://localhost:4200")
////                                  .AllowAnyMethod()
////                                  .AllowAnyHeader()
////                                  .WithExposedHeaders("Content-Disposition")
////                                  .AllowCredentials()); // תוסיפי את AllowCredentials כאן


////        });

////        builder.Services.Configure<MailKitOptions>(builder.Configuration.GetSection("EmailSettings"));

////        builder.Services.AddSingleton<Repositories.Interface.IMailKitProvider, Repositories.Repositories.MailKitProvider>();

////        builder.Services.AddTransient<Service.Interfaces.IEmailService, Service.Services.EmailService>();
////        builder.Services.AddSignalR();

////        var app = builder.Build();


////        if (app.Environment.IsDevelopment())
////        {
////            app.UseSwagger();
////            app.UseSwaggerUI();
////        }

////        app.UseHttpsRedirection();
////        app.UseCors("AllowLocalhost4200");

////        app.UseAuthorization();



////        app.MapControllers();

////        app.Run();
////    }
////}



//using DataContext;
//using Repositories.Interface;
//using Service;
//using Repositories.Entities;
//using Service.Services;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using Microsoft.OpenApi.Models;

//internal class Program
//{
//    private static void Main(string[] args)
//    {
//        var builder = WebApplication.CreateBuilder(args);
//        builder.Services.AddHttpClient();

//        builder.Services.AddAuthentication(options =>
//        {
//            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//        })
//        .AddJwtBearer(options =>
//        {
//            options.TokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateIssuer = true,
//                ValidateAudience = true,
//                ValidateLifetime = true,
//                ValidateIssuerSigningKey = true,
//                ValidIssuer = builder.Configuration["Jwt:Issuer"],
//                ValidAudience = builder.Configuration["Jwt:Audience"],
//                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//            };
//        });

//        builder.Services.AddAuthorization(options =>
//        {
//            options.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
//        });

//        builder.Services.AddControllers();
//        builder.Services.AddScoped<DropboxService>(sp => new DropboxService(
//            builder.Configuration["Dropbox:AccessToken"],
//            builder.Configuration["Dropbox:RefreshToken"],
//            builder.Configuration["Dropbox:AppKey"],
//            builder.Configuration["Dropbox:AppSecret"]
//        ));

//        builder.Services.AddEndpointsApiExplorer();

//        builder.Services.AddSwaggerGen(c =>
//        {
//            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

//            // הגדרת אבטחה ב-Swagger
//            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//            {
//                In = ParameterLocation.Header,
//                Description = "Please insert JWT with Bearer into field",
//                Name = "Authorization",
//                Type = SecuritySchemeType.ApiKey,
//                Scheme = "Bearer"
//            });

//            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
//                {
//                    new OpenApiSecurityScheme
//                    {
//                        Reference = new OpenApiReference
//                        {
//                            Type = ReferenceType.SecurityScheme,
//                            Id = "Bearer"
//                        }
//                    },
//                    new string[] { }
//                }
//            });
//        });

//        builder.Services.AddServices();
//        builder.Services.AddDbContext<IContext, Db>();

//        builder.Services.AddCors(options =>
//        {
//            options.AddPolicy("AllowLocalhost4200",
//                builder => builder.WithOrigins("http://localhost:4200")
//                                  .AllowAnyMethod()
//                                  .AllowAnyHeader()
//                                  .AllowCredentials()
//                                  .WithExposedHeaders("Content-Disposition"));
//        });

//        builder.Services.Configure<MailKitOptions>(builder.Configuration.GetSection("EmailSettings"));
//        builder.Services.AddSingleton<Repositories.Interface.IMailKitProvider, Repositories.Repositories.MailKitProvider>();
//        builder.Services.AddTransient<Service.Interfaces.IEmailService, Service.Services.EmailService>();
//        builder.Services.AddSignalR();

//        var app = builder.Build();

//        if (app.Environment.IsDevelopment())
//        {
//            app.UseSwagger();
//            app.UseSwaggerUI();
//        }

//        app.UseHttpsRedirection();

//        // יש להפעיל את ה-CORS לפני Authorization
//        app.UseCors("AllowLocalhost4200");

//        app.UseAuthentication(); // יש להוסיף Authentication
//        app.UseAuthorization();

//        app.MapControllers();

//        app.Run();
//    }
//}



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

var builder = WebApplication.CreateBuilder(args);

// הוספת שירותים
builder.Services.AddHttpClient();

// הגדרת אימות JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// הגדרת הרשאות
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
});

builder.Services.AddControllers();

// הגדרת שירות Dropbox
builder.Services.AddScoped<DropboxService>(sp => new DropboxService(
    builder.Configuration["Dropbox:AccessToken"],
    builder.Configuration["Dropbox:RefreshToken"],
    builder.Configuration["Dropbox:AppKey"],
    builder.Configuration["Dropbox:AppSecret"]
));

builder.Services.AddEndpointsApiExplorer();

// הגדרת Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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
            Array.Empty<string>()
        }
    });
});

// הוספת שירותים נוספים
builder.Services.AddServices();
builder.Services.AddDbContext<IContext, Db>();

// הגדרת CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithExposedHeaders("Content-Disposition"));
});

// הגדרת שירותי דואר אלקטרוני
builder.Services.Configure<MailKitOptions>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton<IMailKitProvider, Repositories.Repositories.MailKitProvider>();
builder.Services.AddTransient<Service.Interfaces.IEmailService, Service.Services.EmailService>();

builder.Services.AddSignalR();

var app = builder.Build();

// הגדרת סביבת הפיתוח
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// שימוש ב-CORS לפני Authorization
app.UseCors("AllowLocalhost4200");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
