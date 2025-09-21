using Microsoft.EntityFrameworkCore;
using Task1.Data;
using Task1.Interfaces;
using Task1.Services;
using Task1.MiddleWares;
using Task1.ExtensionMethods;
using Microsoft.AspNetCore.Builder;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("mycon")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmplyeeService, EmployeeServices>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
//app.UseMiddleware<RequestLoggingMiddleware>();
app.UserequestLogging();

app.MapControllers();

app.Run();
