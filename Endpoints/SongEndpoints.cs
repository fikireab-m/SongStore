using SongStore.Dtos;

namespace SongStore.Endpoints;
public static class SongEndpoints
{

    const string GetSongEndpoint = "GetSong";

    private static readonly List<SongDto> _songs = [
         new SongDto( 1, "Hello", "Hello", "Celiendion","Blues", new DateOnly(2023,05,06)),
     new SongDto(2, "Ethiopia", "Ethiopia", "Teddy Afro","Pop", new DateOnly(2013,05,06)),
     new SongDto( 3, "Hilm ayidegemim", "Tkur sew", "Teddy Afro","Pop", new DateOnly(2021,05,06))
       ];

    public static WebApplication MapSongEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("songs").WithParameterValidation();
        // GET{id}
        group.MapGet("/{id}", (int id) =>
        {
            SongDto? song = _songs.Find(song => song.Id == id);
            return song == null ? Results.NotFound() : Results.Ok(song);
        }).WithName(GetSongEndpoint);

        // GET
        group.MapGet("/", () => _songs);

        // POST
        group.MapPost("/", (CreateSongDto newSong) =>
        {
            // Tiresome, but possible
            // if (string.IsNullOrEmpty(newSong.Title))
            // {
            //     return Results.BadRequest("Title is required");
            // }
            SongDto songDto = new(
                _songs.Count + 1,
                newSong.Title,
                newSong.Album,
                newSong.Artist,
                newSong.Genre,
                newSong.ReleaseDate
            );
            _songs.Add(songDto);
            return Results.CreatedAtRoute(GetSongEndpoint, new { id = songDto.Id }, songDto);
        });

        // PUT
        group.MapPut("/{id}", (int id, UpdateSongDto updatedSong) =>
        {
            var index = _songs.FindIndex(song => song.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }
            _songs[index] = new SongDto(
                id,
                updatedSong.Title,
                updatedSong.Album,
                updatedSong.Artist,
                updatedSong.Genre,
                updatedSong.ReleaseDate
            );
            return Results.NoContent();
        });

        // DELETE
        group.MapDelete("/{id}", (int id) =>
        {
            var song = _songs.Find(song => song.Id == id);
            _songs.RemoveAll(song => song.Id == id);
            return song != null ? Results.NoContent() : Results.NotFound();
        });
        return app;
    }
}