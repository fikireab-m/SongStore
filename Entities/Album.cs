using SongStore.Entities;

namespace SongStore;

public class Album
{
    public int Id {get; set;}
    public required string Name {get; set;}
    public required int ArtistId {get; set;}
    public Artist? Artist {get; set;}
}
