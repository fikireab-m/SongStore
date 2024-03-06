namespace SongStore.Entities;
public class Song
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required int AlbumId { get; set; }
    public required int ArtistId { get; set; }
    public required int GenreId { get; set; }
    public Album? Album { get; set; }
    public Artist? Artist { get; set; }
    public Genre? Genre { get; set; }

    public required DateOnly ReleaseDate { get; set; }
}