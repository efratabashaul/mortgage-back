//using DataContext;
//using Repositories.Interface;
//using Service;
//
//var builder = WebApplication.CreateBuilder(args);
//
//// Add services to the container.
//
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddServices();
//builder.Services.AddDbContext<IContext, Db>();
//
//builder.Services.AddCors(option =>
//{
//    option.AddDefaultPolicy(policy =>
//    {
//        policy.WithOrigins("http://localhost:4200","http://www.contoso.com")
//        .AllowAnyHeader()
//        .AllowAnyMethod();
//    });
//});
//
//
//
//
//
//
//var app = builder.Build();
//
//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//
//app.UseHttpsRedirection();
//
//app.UseAuthorization();
//
//app.MapControllers();
//
//app.Run();

//using DataContext;
//using Repositories.Interface;
//using Service;
//
//var builder = WebApplication.CreateBuilder(args);
//
//// Add services to the container.
//
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddServices();
//builder.Services.AddDbContext<IContext, Db>();
//
//
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowLocalhost4200",
//        builder => builder.WithOrigins("http://localhost:4200")
//                          .AllowAnyMethod()
//                          .AllowAnyHeader());
//});
//
//var app = builder.Build();
//
//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//
//app.UseHttpsRedirection();
//
//app.UseAuthorization();
//
//app.UseCors("AllowLocalhost4200");
//
//
//app.MapControllers();
//
//app.Run();


using DataContext;
using Repositories.Interface;
using Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddDbContext<IContext, Db>();

<<<<<<< HEAD
// Configure CORS policies
=======
>>>>>>> cce40433a4569358d4a4ebc97813eab6655677df
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

<<<<<<< HEAD
// Enable CORS
app.UseCors(policyName: "AllowLocalhost4200");

app.MapControllers();
app.Run();
=======
app.UseCors("AllowLocalhost4200");


app.MapControllers();

app.Run();
>>>>>>> cce40433a4569358d4a4ebc97813eab6655677df
