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
        group.MapGet("/{id}", async (int id, SongStoreContext dbContext) =>
        {
            Song? song = await dbContext.Songs.FindAsync(id);
            return song == null ? Results.NotFound() : Results.Ok(song.ToDetailsDto());
        }).WithName(GetSongEndpoint);

        // GET
        group.MapGet("/", async (SongStoreContext dbContext) => await dbContext
        .Songs
        .Include(song => song.Genre)
        .Select(song => song.ToDto())
        .AsNoTracking()
        .ToListAsync()
        );

        // POST
        group.MapPost("/", async (CreateSongDto newSong, SongStoreContext dbContext) =>
        {
            // Tiresome, but possible
            // if (string.IsNullOrEmpty(newSong.Title))
            // {
            //     return Results.BadRequest("Title is required");
            // }
            Song song = newSong.ToEntity();
            dbContext.Songs.Add(song);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                GetSongEndpoint,
                new { id = song.Id },
                song.ToDto()
                );
        });

        // PUT
        group.MapPut("/{id}", async (int id, SongStoreContext dbContext, UpdateSongDto updatedSong) =>
        {
            var existingSong = await dbContext.Songs.FindAsync(id);
            if (existingSong is null)
            {
                return Results.NotFound();
            }
            dbContext.Entry(existingSong).CurrentValues.SetValues(updatedSong.ToEntity(id));
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        // DELETE
        group.MapDelete("/{id}", async (int id, SongStoreContext dbContext) =>
        {
            var songDeleted = await dbContext.Songs.Where(song => song.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });
        return app;
    }
}