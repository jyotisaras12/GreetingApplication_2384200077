using BusinessLayer.Service;
using BusinessLayer.Interface;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    logger.Info("HelloGreetingApplication is starting.");
    var builder = WebApplication.CreateBuilder(args);

    // Add NLog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddScoped<IGreetingBL, GreetingBL>();

    // Add Swagger to container
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();


    // Configure the HTTP request pipeline.

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "HelloGreetingApplication startup failed.");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
