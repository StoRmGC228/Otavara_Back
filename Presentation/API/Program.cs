using API.Configurations;
using API.DtoProfile;
using API.Middleware;
using Application.Services;

var builder = WebApplication.CreateBuilder(args);
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(DtoProfile));
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins", policy =>
    {
        policy.WithOrigins(
                "https://otavara-60887440e467.herokuapp.com",
                "http://localhost:5173"
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
app.MapControllers();

await app.EnsureDatabaseCreatedAsync();

app.Run();