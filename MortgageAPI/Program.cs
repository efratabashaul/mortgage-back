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





using DataContext;
using Repositories.Interface;
using Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NETCore.MailKit.Core;
using NETCore.MailKit;
using Repositories.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddDbContext<IContext, Db>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost4200",
                builder => builder.WithOrigins("http://localhost:4200")
                                  .AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .AllowCredentials()); // תוסיפי את AllowCredentials כאן
        });

        builder.Services.Configure<MailKitOptions>(builder.Configuration.GetSection("EmailSettings"));

        builder.Services.AddSingleton<Repositories.Interface.IMailKitProvider, Repositories.Repositories.MailKitProvider>();

        builder.Services.AddTransient<Service.Interfaces.IEmailService, Service.Services.EmailService>();

        var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors("AllowLocalhost4200");


app.MapControllers();

app.Run();
