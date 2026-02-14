using Chinook.DAL.Models;
namespace Chinook.UI
{
  public class Info
  {
    public string? Name { get; set; }
    public int Id { get; set; }
    public int Current { get; set; }
    public int Max { get; set; }
  }
  public class ArtistInfo : Info
  {

  }
  public class AlbumInfo : Info
  {

  }
  public class TrackInfo : Info 
  { 
  }
  public class AlbumInfoModel
  {
    public  AlbumInfo AlbumInfo { get; set; } = new AlbumInfo();  
    public ArtistInfo ArtistInfo { get; set; } = new ArtistInfo();
    public TrackInfo TrackInfo { get; set; }= new TrackInfo();  
    //  public Track trackId { get; set; } = new Track(); 
    public List<Track> Tracks{ get; set; }
  
  }
}
