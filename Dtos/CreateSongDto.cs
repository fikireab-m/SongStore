namespace SongStore.Dtos;
using System.ComponentModel.DataAnnotations;
public record class CreateSongDto(
    [Required][StringLength(50)] string Title,
    [Required][StringLength(30)] string Album,
    [Required][StringLength(50)] string Artist,
    [Required] int GenreId,
    [Required] DateOnly ReleaseDate
);