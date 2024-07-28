using System.Text.Json.Serialization;
using WebAPI.Modules.Common;
using WebAPI.Modules.Common.FeatureFlags;
using WebAPI.Modules.Common.Versioning;
using WebAPI.Modules.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
       .AddJsonFile($"appsettings.Local.json", optional: true, reloadOnChange: true)
       .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true)
       .AddJsonFile($"appsettings.Staging.json", optional: true, reloadOnChange: true);

builder.Services.AddControllers()
                    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddVersioning();
builder.Services.AddFeatureFlags(builder.Configuration);
builder.Services.AddServices();

builder.Services.AddContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<NamedSwaggerGenOptions>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsEnvironment("Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in app.DescribeApiVersions())
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();
app.UseExceptionMiddleware();
app.UseAuthorization();
app.MapControllers();

app.Run();
