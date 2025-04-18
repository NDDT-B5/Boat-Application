using BoatApi.Seeder;
using BoatApi.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi("BoatAPI");

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddBoatApiServices(builder.Configuration);
builder.Services.AddCorsForFrontend();
builder.Services.AddAuthorization();

var app = builder.Build();

UserSeeder.EnsureSeeded(app);

app.UseOpenApi();
app.UseCorsPolicy();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();