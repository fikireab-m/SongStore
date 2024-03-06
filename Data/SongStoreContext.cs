namespace SongStore.Data;
using Microsoft.EntityFrameworkCore;
using SongStore.Entities;

public class SongStoreContext(DbContextOptions<SongStoreContext> options) : DbContext(options)
{
    public DbSet<Song> Songs => Set<Song>();
    public DbSet<Album> Albums => Set<Album>();
    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Genre> Genres => Set<Genre>();
}