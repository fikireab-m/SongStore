using SongStore.Dtos;
using SongStore.Entities;

namespace SongStore.Mapping;

public static class SongMapping
{
    public static Song ToEntity(this CreateSongDto songDto)
    {
        return new Song()
        {
            Title = songDto.Title,
            Album = songDto.Album,
            Artist = songDto.Artist,
            GenreId = songDto.GenreId,
            ReleaseDate = songDto.ReleaseDate
        };
    }

    public static Song ToEntity(this UpdateSongDto songDto, int id)
    {
        return new Song()
        {
            Id = id,
            Title = songDto.Title,
            Album = songDto.Album,
            Artist = songDto.Artist,
            GenreId = songDto.GenreId,
            ReleaseDate = songDto.ReleaseDate
        };
    }

    public static SongDto ToDto(this Song song)
    {
        return new(
                song.Id,
                song.Title,
                song.Album,
                song.Artist,
                song.Genre?.Name!,
                song.ReleaseDate
        );
    }
    public static SongDetailsDto ToDetailsDto(this Song song)
    {
        return new(
                song.Id,
                song.Title,
                song.Album,
                song.Artist,
                song.GenreId,
                song.ReleaseDate
        );
    }
}
