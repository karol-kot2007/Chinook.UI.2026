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

    public Pager Pager { get; set; }

    //TODO remove them, use Pager
    public int CurrentIndex { get; set; } = -1;
    public int MaxIndex { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public int ModifyCurrent(BaseOperation operation, int max)
    {
      if (max <= -1) 
        return -1;

      switch (operation)
      {
        case BaseOperation.Next:
          CurrentIndex++;
          if(CurrentIndex > max)
            CurrentIndex = 0;
        return CurrentIndex;

        case BaseOperation.Prev:
          CurrentIndex--;
          if(CurrentIndex < 0) 
            CurrentIndex = max;
          return CurrentIndex; 
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
      MaxIndex = maxIndex;
      CurrentIndex = currentIndex;
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
      MaxIndex = maxIndex;
      CurrentIndex = currentIndex;
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
    public int CurrentAlbumIndex => AlbumInfo.AlbumInfo.CurrentIndex;
    public int MaxAlbumIndex => AlbumInfo.AlbumInfo.MaxIndex;

    public int CurrentArtistIndex => AlbumInfo.ArtistInfo.CurrentIndex;
    public int MaxArtistIndex => AlbumInfo.ArtistInfo.MaxIndex;
    public AlbumInfoModel AlbumInfo { get; set; } = new();

    public string GetArtistInfo(ArtistModel model)
    {
      return "artist:" + model.AlbumInfo.ArtistInfo.Name + " " + (model.CurrentArtistIndex + 1).ToString() + "/" + (model.MaxArtistIndex+1);
    }

    public string GetAlbumInfo(ArtistModel model)
    {
      return "album:" + model.AlbumInfo.AlbumInfo.Name + " " + (model.CurrentAlbumIndex + 1).ToString() + "/" + (model.MaxAlbumIndex+1);
    }

    public override string ToString()
    {
      return base.ToString() + ""+ AlbumInfo.ArtistInfo;
    }

    public int ModifyArtistIndex(int maxArtistIndex, Operation? operation = null)
    {
      switch (operation)
      {
        case Operation.NextArtist:
          AlbumInfo.AlbumInfo.CurrentIndex = 0;
          AlbumInfo.ArtistInfo.ModifyCurrent(BaseOperation.Next, maxArtistIndex);
          break;

        case Operation.PrevArtist:
          AlbumInfo.AlbumInfo.CurrentIndex = 0;
          AlbumInfo.ArtistInfo.ModifyCurrent(BaseOperation.Prev, maxArtistIndex);
          break;
      }
      return AlbumInfo.ArtistInfo.CurrentIndex;
    }

    public int ModifyAlbumIndex(int maxAlbumIndex, Operation? operation = null)
    {
      switch (operation)
      {
        case Operation.NextAlbum:
          AlbumInfo.AlbumInfo.ModifyCurrent(BaseOperation.Next, maxAlbumIndex);
          break;

        case Operation.PrevAlbum:
          AlbumInfo.AlbumInfo.ModifyCurrent(BaseOperation.Prev, maxAlbumIndex);
          break;
      }

      return AlbumInfo.AlbumInfo.CurrentIndex;
    }


  }
}
