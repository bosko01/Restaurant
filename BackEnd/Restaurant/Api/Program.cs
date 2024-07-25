using Api.Data.Mapster;
using Api.Middleware;
using Application.DependencyInjetion;
using Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.Sources.Clear();

    var env = hostingContext.HostingEnvironment;

    config.SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) //load base settings
            .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true) //load local settings
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);//load environment settings

    if (args != null)
    {
        config.AddCommandLine(args);
    }
});

builder.Services.RegisterMapsterConfiguration();

// Add services to the container.

builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApplicationLayer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();  //says u must use https

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();