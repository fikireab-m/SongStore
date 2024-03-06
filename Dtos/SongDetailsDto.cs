namespace SongStore.Dtos;

public record class SongDetailsDto
(
    int Id,
    string Title,
    string Album,
    string Artist,
    int GenreId,
    DateOnly ReleaseDate
);
