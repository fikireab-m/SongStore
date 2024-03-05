namespace SongStore.Dtos;
public record class UpdateSongDto(
    string Title,
    string Album,
    string Artist,
    string Genre,
    DateOnly ReleaseDate
);