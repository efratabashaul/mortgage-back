using DataContext;
using Repositories.Interface;
using Service;
using Repositories.Entities;
using Service.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHttpClient();

        builder.Services.AddControllers();
        builder.Services.AddScoped<DropboxService>(sp => new DropboxService(
        builder.Configuration["Dropbox:AccessToken"],
        builder.Configuration["Dropbox:RefreshToken"],
        builder.Configuration["Dropbox:AppKey"],
        builder.Configuration["Dropbox:AppSecret"]
));

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
