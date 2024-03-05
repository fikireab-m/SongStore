namespace SongStore.Dtos;
public record class CreateSongDto(
    string Title,
    string Album,
    string Artist,
    string Genre,
    DateOnly ReleaseDate
);