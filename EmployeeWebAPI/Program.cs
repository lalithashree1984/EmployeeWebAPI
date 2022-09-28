using EmployeeWebAPI.Data;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using EmployeeWebAPI;
using Serilog;
using MyILoggerLibrary;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging(loggingbuilder => { loggingbuilder.ClearProviders(); loggingbuilder.AddFile(); });
// Add services to the container.
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddControllersWithViews().AddNewtonsoftJson
(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EmployeeDBContext>(options => options.UseInMemoryDatabase("EmployeeDb"));
//builder.Services.AddDbContext<EmployeeDBContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeConnectionString")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMyCustomMiddleware();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthorization();

app.MapControllers();

app.Run();
