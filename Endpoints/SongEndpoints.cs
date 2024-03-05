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

        app.MapGet("/songs/{id}", (int id) =>
        {
            SongDto? song = _songs.Find(song => song.Id == id);
            return song == null ? Results.NotFound() : Results.Ok(song);
        }).WithName(GetSongEndpoint);
        // app.MapGet("/songs/{id}", (int id) => SongDB.GetSong(id));
        // app.MapGet("/songs", () => SongDB.GetSongs());
        app.MapGet("/songs", () => _songs);
        app.MapPost("/songs", (CreateSongDto newSong) =>
        {
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
        // app.MapPost("/songs", (Song song) => SongDB.CreateSong(song));
        app.MapPut("/songs/{id}", (int id, UpdateSongDto updatedSong) =>
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
        // app.MapPut("/songs", (Song song) => SongDB.UpdateSong(song));
        app.MapDelete("/songs/{id}", (int id) =>
        {
            var song = _songs.Find(song => song.Id == id);
            _songs.RemoveAll(song => song.Id == id);
            return song != null ? Results.NoContent() : Results.NotFound();
        });
        // app.MapDelete("/songs/{id}", (int id) => SongDB.RemoveSong(id));
        return app;
    }
}