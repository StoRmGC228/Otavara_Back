using API.Configurations;
using API.DtoProfile;

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
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{Environment.GetEnvironmentVariable("PORT") ?? "5000"}");

var app = builder.Build();


if (app.Environment.IsDevelopment()||app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyAllowSpecificOrigins");
app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();
app.Run();