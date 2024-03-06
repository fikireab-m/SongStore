namespace SongStore.Dtos;
using System.ComponentModel.DataAnnotations;
public record class UpdateSongDto(
    [Required][StringLength(50)] string Title,
    [Required][StringLength(30)] string Album,
    [Required][StringLength(50)] string Artist,
    [Required][StringLength(20)] string Genre,
    [Required] DateOnly ReleaseDate
);