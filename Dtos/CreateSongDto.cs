using System.ComponentModel.DataAnnotations;

namespace SongStore.Dtos;
public record class CreateSongDto(
    [Required][StringLength(50)] string Title,
    [Required][StringLength(30)] string Album,
    [Required][StringLength(50)] string Artist,
    [Required][StringLength(20)] string Genre,
    [Required] DateOnly ReleaseDate
);