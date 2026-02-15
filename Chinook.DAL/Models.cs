

namespace Chinook.DAL.Models
{

  public class AlbumTrack
  {
    public int artistId { get; set; }
    public string? artistName { get; set; }
    public int albumId { get; set; }
    public int trackId { get; set; }
    public string? albumName { get; set; }
    public string? trackName { get; set; }
  }
  public class Track
  {
    public int GenreId { get; set; }
    public int MediaTypeId { get; set; }
    public int AlbumId { get; set; }
    public int TrackId { get; set; }
    public string? Name { get; set; }
    public string? Composer { get; set; }
    public int Milliseconds { get; set; }
    public int Bytes { get; set; }
    public string? LocalPath { get; set; }
    public float UnitPrice { get; set; }

  }
  public class Artist
  {
    public string? Name { get; set; }
    public int ArtistId { get; set; }
    //public int albumId { get; set; }
  }
  public class Album
  {
    public string? Title { get; set; }
    public int AlbumId { get; set; }
    public int ArtistId { get; set; }
  }
  public class AlbumTracks
  {
    public string? artistName { get; set; }
    public string? albumName { get; set; }
    public int artistId { get; set; }
    public int albumId { get; set; }
    public int trackId { get; set; }
    public string? trackName { get; set; }
  }
}
