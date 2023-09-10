using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(u =>
{
    u.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "Ver:0.0.1",  // 版本
        Title = "BC测试题目",  // 标题
        Description = "写一个Web API程序满足技术要求和业务需求",  // API描述
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "付国良",
            Email = "fglone@126.com",
        }
    });
});


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
   .Enrich.FromLogContext()
   .WriteTo.Logger(conf =>
       conf.MinimumLevel.Debug()
       //.WriteTo.File(
       //    @"logs\\log.txt",
       //    rollingInterval: RollingInterval.Day,
       //    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
       .WriteTo.RollingFile(
           "logs\\{Date}.txt",
           retainedFileCountLimit: 7,
           outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
           ))
   .CreateLogger();
builder.Host.UseSerilog();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI(u =>
{
    u.SwaggerEndpoint("/swagger/v1/swagger.json", "BCTest.API_v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
