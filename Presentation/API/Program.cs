using API.Configurations;
using API.DtoProfile;
using API.Services;

var builder = WebApplication.CreateBuilder(args);
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(DtoProfile));
builder.Services.AddCors(options =>
{
    options.AddPolicy(myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://otavara-60887440e467.herokuapp.com", "http://localhost:5173").AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});
builder.Services.AddInfrastructureConfigurations(builder.Configuration);
builder.Services.AddApplicationConfigurations(builder.Configuration);

builder.Services.AddDataSeeders();
builder.Services.AddScoped<DatabaseSeedService>();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);
app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();

await app.EnsureDatabaseCreatedAsync();

app.Run();