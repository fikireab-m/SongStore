using Microsoft.EntityFrameworkCore;
using SongStore.Data;
using SongStore.Dtos;
using SongStore.Entities;
using SongStore.Mapping;

namespace SongStore.Endpoints;
public static class SongEndpoints
{

    const string GetSongEndpoint = "GetSong";

    public static WebApplication MapSongEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("songs").WithParameterValidation();
        // GET{id}
        group.MapGet("/{id}", (int id, SongStoreContext dbContext) =>
        {
            Song? song = dbContext.Songs.Find(id);
            return song == null ? Results.NotFound() : Results.Ok(song.ToDetailsDto());
        }).WithName(GetSongEndpoint);

        // GET
        group.MapGet("/", (SongStoreContext dbContext) => dbContext
        .Songs
        .Include(game => game.Genre)
        .Select(game => game.ToDto())
        );

        // POST
        group.MapPost("/", (CreateSongDto newSong, SongStoreContext dbContext) =>
        {
            // Tiresome, but possible
            // if (string.IsNullOrEmpty(newSong.Title))
            // {
            //     return Results.BadRequest("Title is required");
            // }
            Song song = newSong.ToEntity();
            dbContext.Songs.Add(song);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(
                GetSongEndpoint,
                new { id = song.Id },
                song.ToDto()
                );
        });

        // PUT
        group.MapPut("/{id}", (int id, SongStoreContext dbContext, UpdateSongDto updatedSong) =>
        {
            var existingSong = dbContext.Songs.Find(id);
            if (existingSong is null)
            {
                return Results.NotFound();
            }
            dbContext.Entry(existingSong).CurrentValues.SetValues(updatedSong.ToEntity(id));
            dbContext.SaveChanges();
            return Results.NoContent();
        });

        // DELETE
        // group.MapDelete("/{id}", (int id, SongStoreContext dbContext) =>
        // {
        //     var song = _songs.Find(song => song.Id == id);
        //     _songs.RemoveAll(song => song.Id == id);
        //     return song != null ? Results.NoContent() : Results.NotFound();
        // });
        return app;
    }
}