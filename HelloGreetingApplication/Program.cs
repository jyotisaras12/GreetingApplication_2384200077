using BusinessLayer.Service;
using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using RepositoryLayer.Context;
using NLog;
using NLog.Web;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
    builder.Services.AddScoped<IGreetingRL, GreetingRL>();

    var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
    builder.Services.AddDbContext<GreetingApplicationContext>(options => options.UseSqlServer(connectionString));

    // Add Swagger to container
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        // Get the XML file path
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

        // Include XML comments in Swagger
        options.IncludeXmlComments(xmlPath);
    });

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
