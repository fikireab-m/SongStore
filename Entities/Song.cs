namespace SongStore.Entities;
public class Song
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Album { get; set; }
    public required string Artist { get; set; }
    public required int GenreId { get; set; }
    public Genre? Genre { get; set; }
    public required DateOnly ReleaseDate { get; set; }
}