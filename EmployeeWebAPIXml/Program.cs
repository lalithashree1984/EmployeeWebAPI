using EmployeeDetailsServices.Services;
using EmployeeServices.Data;
using EmployeeWebAPI.Controllers;
using EmployeeWebAPI.Helpers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllersWithViews().AddXmlDataContractSerializerFormatters() ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddDbContext<EmployeeDBContext>(options => options.UseInMemoryDatabase("EmployeeDb"));
builder.Services.AddAutoMapper(typeof(AutoMappingProfiles).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
