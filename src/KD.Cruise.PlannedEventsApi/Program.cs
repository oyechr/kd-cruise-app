using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using PlannedEventsApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApiVersioning(opt =>
{
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.ReportApiVersions = true;
});
builder.Services.AddLogging();
builder.Services.AddDbContext<PlannedEventsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Planned Events API",
        Version = "v1",
        Description = "API for managing planned events in the cruise system."
    });
    c.EnableAnnotations();
});
var app = builder.Build();

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Planned Events API V1");
    c.RoutePrefix = string.Empty; // Serve Swagger at the app's root
});

app.MapControllers();
app.Run();