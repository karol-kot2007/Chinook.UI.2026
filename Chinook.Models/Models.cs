using Chinook.DAL.Models;
using static Chinook.Models.Repository;

namespace Chinook.Models
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
    public AlbumInfo AlbumInfo { get; set; } = new AlbumInfo();
    public ArtistInfo ArtistInfo { get; set; } = new ArtistInfo();
    public List<Track> Tracks { get; set; }

  }
  public class ArtistModel
  {
    public int CurrentAlbumIndex { get; set; }
    public int CurrentArtistIndex { get; set; }
    public int MaxAlbumIndex { get; set; }
    public int MaxArtistIndex { get; set; }
    public AlbumInfoModel AlbumInfo { get; set; } = new();

    public string GetArtistInfo(ArtistModel model)
    {
      return "artist:" + model.AlbumInfo.ArtistInfo.Name + " " + model.CurrentArtistIndex.ToString() + "/" + model.MaxArtistIndex ;
    }

    public string GetAlbumInfo(ArtistModel model)
    {
      return "album:" + model.AlbumInfo.AlbumInfo.Name + " " + (model.CurrentAlbumIndex+1).ToString() + "/" + model.MaxAlbumIndex;
    }
    public int ModifyArtistIndex(int maxArtistIndex, Operation? operation = null)
    {
      switch (operation)
      {
        case Operation.NextArtist:
          AlbumInfo.ArtistInfo.Current++;
          AlbumInfo.AlbumInfo.Current = 0;
          if (AlbumInfo.ArtistInfo.Current == maxArtistIndex)
          {
            AlbumInfo.ArtistInfo.Current = 0;
          }
          break;

        case Operation.PrevArtist:
          AlbumInfo.ArtistInfo.Current--;
          AlbumInfo.AlbumInfo.Current = 0;
          if (AlbumInfo.ArtistInfo.Current < 0)
          {
            AlbumInfo.ArtistInfo.Current = maxArtistIndex - 1;
          }
          break;
      }
      return AlbumInfo.ArtistInfo.Current;
    }

    public int ModifyAlbumIndex(int maxAlbumIndex, Operation? operation = null)
    {
      switch (operation)
      {
        case Operation.NextAlbum:
          AlbumInfo.AlbumInfo.Current++;
          if (AlbumInfo.AlbumInfo.Current == maxAlbumIndex)
            AlbumInfo.AlbumInfo.Current = 0;
          break;

        case Operation.PrevAlbum:
          AlbumInfo.AlbumInfo.Current--;
          if (AlbumInfo.AlbumInfo.Current < 0)
            AlbumInfo.AlbumInfo.Current = maxAlbumIndex - 1;
          break;
      }
      return AlbumInfo.AlbumInfo.Current;
    }
  }
}
