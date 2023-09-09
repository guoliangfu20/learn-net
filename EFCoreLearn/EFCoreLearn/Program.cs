using EFCoreLearn.MyContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Formatting.Compact;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<MyDbContext>(options =>
//{
//    string myDbConnectionString = builder.Configuration.GetConnectionString("MyDbConnectionString") ?? "";
//    options.UseMySql(myDbConnectionString, new MySqlServerVersion(new Version(5, 7, 41)));

//});


builder.Services.AddDbContext<MySqlDbContext>(options =>
{
    var sqlConnectionString = builder.Configuration.GetConnectionString("MySqlDbConnectionString");
    options.UseSqlServer(sqlConnectionString);
});

builder.Services.AddLogging(bulid =>
{
    new LoggerConfiguration()
      .MinimumLevel.Debug()
      .Enrich.FromLogContext()
       .WriteTo.File(formatter: new CompactJsonFormatter(), "log\\mayapp.txt", rollingInterval: RollingInterval.Day).CreateLogger();
    bulid.AddSerilog();

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
