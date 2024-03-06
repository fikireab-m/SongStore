namespace SongStore.Data;
using Microsoft.EntityFrameworkCore;
using SongStore.Entities;

public class SongStoreContext(DbContextOptions<SongStoreContext> options) : DbContext(options)
{
    public DbSet<Song> Songs => Set<Song>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new {Id = 1, Name = "Pop"},
            new {Id = 2, Name = "Rock"},
            new {Id = 3, Name = "Jazz"},
            new {Id = 4, Name = "Hiphop"},
            new {Id = 5, Name = "Electronic"},
            new {Id = 6, Name = "Country"},
            new {Id = 7, Name = "Blues"},
            new {Id = 8, Name = "Ambassel"},
            new {Id = 9, Name = "Anchihoye"},
            new {Id = 10, Name = "Tizta"},
            new {Id = 11, Name = "Batti"}
        );
    }
}