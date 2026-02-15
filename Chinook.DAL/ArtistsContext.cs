using Chinook.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DAL
{
  public class ArtistContext : DbContext
  {
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Album> Albums { get; set; }




    public DbSet<AlbumTracks> AlbumTracks { get; set; }

    // tabvele jak powzyej zrobvic 
    public static string chinookPath = "chinook.db";
    public string DbPath { get; }
    public ArtistContext()
    {
      Console.WriteLine($"Database path: {chinookPath}.");
      DbPath = chinookPath;
    }
    public List<Artist> GetArtists()
    {
      return Artists.ToList();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
      options.UseSqlite($"Data Source={chinookPath}");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Track>().HasKey(x => x.TrackId);
      modelBuilder.Entity<Artist>().HasKey(x => x.ArtistId);
      
      modelBuilder.Entity<AlbumTrack>().HasNoKey();
      modelBuilder.Entity<AlbumTracks>().HasNoKey();
    }
  }
}
