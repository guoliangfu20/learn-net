using EFCoreSqlLearn.MyDbContext;
using Microsoft.EntityFrameworkCore;
using Serilog.Formatting.Compact;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MySqlDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MyDbSqlConnectionString");
    options.UseSqlServer(connectionString);
});




builder.Services.AddLogging(bulid =>
{
    //Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(new ConfigurationBuilder()
    //        .SetBasePath(Directory.GetCurrentDirectory())
    //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    //        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    //        .AddEnvironmentVariables()
    //        .Build())
    //  .MinimumLevel.Debug()
    //  .Enrich.FromLogContext()
    //   .WriteTo.File(formatter: new CompactJsonFormatter(), "log\\mayapp.txt", rollingInterval: RollingInterval.Day).CreateLogger();
    //bulid.AddSerilog();


    Log.Logger = new LoggerConfiguration().MinimumLevel.Warning()
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
