using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
   .Enrich.FromLogContext()
   .WriteTo.Logger(conf =>

       conf.MinimumLevel.Debug()
       .WriteTo.File(
           @"logs\\log.txt",
           rollingInterval: RollingInterval.Day,
           outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
       .WriteTo.RollingFile(
           "logs\\{Date}-log.txt",
           retainedFileCountLimit: 7,
           outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
           ))
   .CreateLogger();
builder.Host.UseSerilog();


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
