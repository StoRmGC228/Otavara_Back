using API.Configurations;
using API.DtoProfile;
using API.Middleware;
using Application.Services;
using Hangfire;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "����� JWT ����� � ������: Bearer {your token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddAutoMapper(typeof(DtoProfile));
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins", policy =>
    {
        policy.WithOrigins(
                "https://otavara-ddc3167d8b2e.herokuapp.com",
                "http://localhost:5173",
                "https://otavara-front.loca.lt",
                "https://otavara-back.loca.lt",
                "https://fitting-gar-hideously.ngrok-free.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddInfrastructureConfigurations(builder.Configuration);
builder.Services.AddApplicationConfigurations(builder.Configuration);
builder.Services.AddApiConfigurations(builder.Configuration);

builder.Services.AddDataSeeders();
builder.Services.AddScoped<DatabaseSeedService>();
var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
    app.Urls.Add($"http://*:{port}");
}

app.UseCors("MyAllowSpecificOrigins");
app.UseAuthentication();

app.UseAuthorization();

app.UseHangfireDashboard("/dashboard");
app.MapControllers();

app.Services.AddRecurringJobs();
await app.EnsureDatabaseCreatedAsync();

app.Run();