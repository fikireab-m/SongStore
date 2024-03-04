namespace SongStore.DB; 

 public record Song 
 {
   public int Id {get; set;} 
   public required string Title { get; set; }
   public required string Album { get; set; }
   public required string Artist { get; set; }
   public required string Genre { get; set; }
 }

 public class SongDB
 {
   private static List<Song> _songs =
   [
     new Song{ Id=1, Title="Hello", Album="Hello", Artist="Celiendion",Genre="Blues" },
     new Song{ Id=2, Title="Ethiopia", Album="Ethiopia", Artist="Teddy Afro",Genre="Pop" },
     new Song{ Id=3, Title="Hilm ayidegemim", Album="Tkur sew", Artist="Teddy Afro",Genre="Pop" }
   ];

   public static List<Song> GetSongs() 
   {
     return _songs;
   } 

   public static Song ? GetSong(int id) 
   {
     return _songs.SingleOrDefault(song => song.Id == id);
   } 

   public static Song CreateSong(Song song) 
   {
     _songs.Add(song);
     return song;
   }

   public static Song UpdateSong(Song update) 
   {
     _songs = _songs.Select(song =>
     {
       if (song.Id == update.Id)
       {
         song.Title = update.Title;
         song.Album = update.Album;
         song.Artist = update.Artist;
         song.Genre = update.Genre;
       }
       return song;
     }).ToList();
     return update;
   }

   public static void RemoveSong(int id)
   {
     _songs = [.. _songs.FindAll(song => song.Id != id)];
   }
 }