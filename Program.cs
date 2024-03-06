using Microsoft.OpenApi.Models;
using SongStore.Data;
using SongStore.Endpoints;
var builder = WebApplication.CreateBuilder(args);

var conString = builder.Configuration.GetConnectionString("SongStore");
builder.Services.AddSqlite<SongStoreContext>(conString);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SongStore API", Description = "", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SongStore API V1");
});
await app.MigrateDbAsync();
app.MapSongEndpoints();
app.Run();