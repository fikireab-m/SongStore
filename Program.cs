using Microsoft.OpenApi.Models;
using SongStore.DB;
var builder = WebApplication.CreateBuilder(args);

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

app.MapGet("/", () => "Hello World!");
app.MapGet("/songs/{id}", (int id) => SongDB.GetSong(id));
app.MapGet("/songs", () => SongDB.GetSongs());
app.MapPost("/songs", (Song song) => SongDB.CreateSong(song));
app.MapPut("/songs", (Song song) => SongDB.UpdateSong(song));
app.MapDelete("/songs/{id}", (int id) => SongDB.RemoveSong(id));
app.Run();