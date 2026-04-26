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
      return MemberwiseClone();
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

  public class Info : ICloneable
  {
    public string? Name { get; set; }
    public int Id { get; set; }

    public object Clone()
    {
      return MemberwiseClone();
    }

    public virtual Info CloneTyped()
    {
      return (Info)Clone();
    }

    public override string ToString()
    {
      return base.ToString() + Name;
    }
    
  }
  public class ArtistInfo : Info
  {
    public ArtistInfo() { }

    public ArtistInfo(DAL.Models.Artist artist, Pager pager)
    {
      Name = artist.Name;
      Id = artist.ArtistId;
    }

    public override ArtistInfo CloneTyped()
    {
      return (ArtistInfo)Clone();
    }

  }
  public class AlbumInfo : Info
  {
    public List<Track>? Tracks { get; set; }

    public AlbumInfo() { }

    public AlbumInfo(DAL.Models.Album album)
    {
      Name = album.Title;
      Id = album.AlbumId;
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

    public Pager ArtistPager { get; set; } = new();
    public Pager AlbumPager { get; set; } = new();
  }
  public class ArtistModel
  {
    public int CurrentAlbumIndex => MusicModel.AlbumPager.CurrentIndex;
    public int MaxAlbumIndex => MusicModel.AlbumPager.MaxIndex;

    public int CurrentArtistIndex => MusicModel.ArtistPager.CurrentIndex;
    public int MaxArtistIndex => MusicModel.ArtistPager.MaxIndex;
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
