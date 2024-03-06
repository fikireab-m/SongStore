using Microsoft.EntityFrameworkCore;
using SongStore.Data;
using SongStore.Entities;

namespace SongStore.Endpoints;

public static class GenreEndpoints
{
    const string GetGenresEndpoint = "GetGenres";

    public static WebApplication MapGenreEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("genres").WithParameterValidation();
        // GET{id}
        group.MapGet("/{id}", async (int id, SongStoreContext dbContext) =>
        {
            Genre? genre = await dbContext.Genres.FindAsync(id);
            return genre == null ? Results.NotFound() : Results.Ok(genre);
        }).WithName(GetGenresEndpoint);

        // GET
        group.MapGet("/", async (SongStoreContext dbContext) => await dbContext
        .Genres
        .AsNoTracking()
        .ToListAsync()
        ).WithName(GetGenresEndpoint);

        // POST
        group.MapPost("/", async (Genre newGenre, SongStoreContext dbContext) =>
        {
            Genre genre = new(){
                Id=newGenre.Id,
                Name = newGenre.Name
            };
            dbContext.Genres.Add(genre);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                GetGenresEndpoint,
                new { id = genre.Id },
                genre
                );
        });

        // DELETE
        group.MapDelete("/{id}", async (int id, SongStoreContext dbContext) =>
        {
            var genreDeleted = await dbContext.Genres.Where(genre => genre.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });
        return app;
    }

}
