namespace SongStore.Dtos;
public record class SongDto(
    int Id,
    string Title,
    string Album,
    string Artist,
    string Genre,
    DateOnly ReleaseDate
);