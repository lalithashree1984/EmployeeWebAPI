using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using EmployeeWebAPI;
using Serilog;
using EmployeeWebAPI.MiddleWare;
using Microsoft.Extensions.Configuration;
using Serilog.Extensions.Logging.File;
using EmployeeWebAPI.Helpers;
using EmployeeDetailsServices.Services;
using EmployeeServices.Data;
using EmployeeDetailsModels.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
//Initialize Logger    
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();

builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(Logger);
// Add services to the container.
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
//var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
//Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Services.AddControllersWithViews().AddNewtonsoftJson
(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeDepartmentService, EmployeeDepartmentService>();
builder.Services.AddAutoMapper(typeof(AutoMappingProfiles).Assembly);
builder.Services.AddDbContext<EmployeeDBContext>(options => options.UseInMemoryDatabase("EmployeeDb"));
//builder.Services.AddDbContext<EmployeeDBContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeConnectionString")));
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCustomMiddleWare();

//app.UseMyCustomMiddleware();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthorization();

app.MapControllers();

app.Run();
