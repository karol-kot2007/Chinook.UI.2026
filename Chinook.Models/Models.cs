using Chinook.DAL.Models;
using static Chinook.Models.Repository;

namespace Chinook.Models
{
  public enum BaseOperation
  {
    Next, Prev
  }
  public class Pager : ICloneable
  {
    public int CurrentIndex { get; set; } = -1;
    public int MaxIndex { get; set; }
    public object Clone()
    {
      var pager = new Pager();
      pager.CurrentIndex = CurrentIndex;
      pager.MaxIndex = MaxIndex;

      return pager;
    }
    public Pager CloneTyped()
    {
      return (Pager)Clone();
    }

    public int ModifyCurrent(BaseOperation operation)
    {
      int max = MaxIndex;
      if (max <= -1)
        return -1;

      switch (operation)
      {
        case BaseOperation.Next:
          CurrentIndex++;
          if (CurrentIndex > max)
            CurrentIndex = 0;
          return CurrentIndex;

        case BaseOperation.Prev:
          CurrentIndex--;
          if (CurrentIndex < 0)
            CurrentIndex = max;
          return CurrentIndex;
        default:
          return 0;
      }

    }
  }

  public class Info
  {
    public string? Name { get; set; }
    public int Id { get; set; }
    public Pager Pager { get; set; } = new();

    public override string ToString()
    {
      return base.ToString() + Name;
    }
    
  }
  public class ArtistInfo : Info
  {
    public ArtistInfo() { }
    public ArtistInfo(DAL.Models.Artist artist, int maxIndex, int currentIndex)
    {
      Name = artist.Name;
      Id = artist.ArtistId;
      Pager.MaxIndex = maxIndex;
      Pager.CurrentIndex = currentIndex;
    }
    public ArtistInfo(DAL.Models.Artist artist, Pager pager)
    {
      Name = artist.Name;
      Id = artist.ArtistId;
      Pager = pager;
    }
  }
  public class AlbumInfo : Info
  {
    public List<Track> Tracks { get; set; }

    public AlbumInfo() { }
    public AlbumInfo(DAL.Models.Album album, int maxIndex, int currentIndex)
    {
      Name = album.Title;
      Id = album.AlbumId;
      Pager.MaxIndex = maxIndex;
      Pager.CurrentIndex = currentIndex;
    }

    public AlbumInfo(DAL.Models.Album album, Pager pager)
    {
      Name = album.Title;
      Id = album.AlbumId;
      Pager = pager;
    }

    public override string ToString()
    {
      return base.ToString() + " " + Name;
    }
  }
  public class TrackInfo : Info
  {
  }
  public class MusicModel
  {
    public AlbumInfo AlbumInfo { get; set; } = new AlbumInfo();
    public ArtistInfo ArtistInfo { get; set; } = new ArtistInfo();
    
  }
  public class ArtistModel
  {
    public int CurrentAlbumIndex => MusicModel.AlbumInfo.Pager.CurrentIndex;
    public int MaxAlbumIndex => MusicModel.AlbumInfo.Pager.MaxIndex;

    public int CurrentArtistIndex => MusicModel.ArtistInfo.Pager.CurrentIndex;
    public int MaxArtistIndex => MusicModel.ArtistInfo.Pager.MaxIndex;
    public MusicModel MusicModel { get; set; } = new();
    public string GetArtistInfo()
    {
      return "artist:" + MusicModel.ArtistInfo.Name + " " + (CurrentArtistIndex + 1).ToString() + "/" + (MaxArtistIndex + 1);
    }

    public string GetAlbumInfo()
    {
      return "album:" + MusicModel.AlbumInfo.Name + " " + (CurrentAlbumIndex + 1).ToString() + "/" + (MaxAlbumIndex + 1);
    }

    public override string ToString()
    {
      return base.ToString() + "" + MusicModel.ArtistInfo;
    }
  }
}
