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
  }

  public class Info
  {
    public string? Name { get; set; }
    public int Id { get; set; }
    public Pager Pager { get; set; } = new();
    public int ModifyCurrent(BaseOperation operation)
    {
      int max = Pager.MaxIndex;
      if (max <= -1)
        return -1;

      switch (operation)
      {
        case BaseOperation.Next:
          Pager.CurrentIndex++;
          if (Pager.CurrentIndex > max)
            Pager.CurrentIndex = 0;
          return Pager.CurrentIndex;

        case BaseOperation.Prev:
          Pager.CurrentIndex--;
          if (Pager.CurrentIndex < 0)
            Pager.CurrentIndex = max;
          return Pager.CurrentIndex;
        default:
          return 0;
      }

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

    public override string ToString()
    {
      return base.ToString() + " " + Name;
    }
  }
  public class AlbumInfo : Info
  {
    public AlbumInfo() { }
    public AlbumInfo(DAL.Models.Album album, int maxIndex, int currentIndex)
    {
      Name = album.Title;
      Id = album.AlbumId;
      Pager.MaxIndex = maxIndex;
      Pager.CurrentIndex = currentIndex;
    }

    public override string ToString()
    {
      return base.ToString() + " " + Name;
    }
  }
  public class TrackInfo : Info
  {
  }
  public class AlbumInfoModel
  {
    public AlbumInfo AlbumInfo { get; set; } = new AlbumInfo();
    public ArtistInfo ArtistInfo { get; set; } = new ArtistInfo();
    public List<Track> Tracks { get; set; }
  }
  public class ArtistModel
  {
    public int CurrentAlbumIndex => AlbumInfo.AlbumInfo.Pager.CurrentIndex;
    public int MaxAlbumIndex => AlbumInfo.AlbumInfo.Pager.MaxIndex;

    public int CurrentArtistIndex => AlbumInfo.ArtistInfo.Pager.CurrentIndex;
    public int MaxArtistIndex => AlbumInfo.ArtistInfo.Pager.MaxIndex;
    public AlbumInfoModel AlbumInfo { get; set; } = new();
    public string GetArtistInfo()
    {
      return "artist:" + AlbumInfo.ArtistInfo.Name + " " + (CurrentArtistIndex + 1).ToString() + "/" + (MaxArtistIndex + 1);
    }

    public string GetAlbumInfo()
    {
      return "album:" + AlbumInfo.AlbumInfo.Name + " " + (CurrentAlbumIndex + 1).ToString() + "/" + (MaxAlbumIndex + 1);
    }

    public override string ToString()
    {
      return base.ToString() + "" + AlbumInfo.ArtistInfo;
    }

    public int ModifyArtistIndex(Operation? operation = null)
    {
      switch (operation)
      {
        case Operation.NextArtist:
          AlbumInfo.AlbumInfo.Pager.CurrentIndex = 0;
          AlbumInfo.ArtistInfo.ModifyCurrent(BaseOperation.Next);
          break;

        case Operation.PrevArtist:
          AlbumInfo.AlbumInfo.Pager.CurrentIndex = 0;
          AlbumInfo.ArtistInfo.ModifyCurrent(BaseOperation.Prev);
          break;
      }
      return AlbumInfo.ArtistInfo.Pager.CurrentIndex;
    }
    public int ModifyAlbumIndex(Operation? operation = null)
    {
      switch (operation)
      {
        case Operation.NextAlbum:
          AlbumInfo.AlbumInfo.ModifyCurrent(BaseOperation.Next);
          break;

        case Operation.PrevAlbum:
          AlbumInfo.AlbumInfo.ModifyCurrent(BaseOperation.Prev);
          break;
      }
      return AlbumInfo.AlbumInfo.Pager.CurrentIndex;
    }


  }
}
