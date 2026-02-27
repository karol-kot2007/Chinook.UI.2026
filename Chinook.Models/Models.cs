using Chinook.DAL;
using Chinook.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public AlbumInfoModel AlbumInfo { get; set; }
    //public AlbumInfoModel BuildModel(ArtistContext context)
    //{
    //  MaxArtistIndex = context.Artists.Count();
    //  var model = new AlbumInfoModel();
    //  var artist = context.Artists.ElementAt(CurrentArtistIndex);  
    //  model.ArtistInfo.Name = artist.Name;
    //  model.ArtistInfo.Id = artist.ArtistId;
    //  model.ArtistInfo.Max = MaxArtistIndex;
    //  model.ArtistInfo.Current = CurrentArtistIndex;
    //    var albums = context.Albums.Where(a => a.ArtistId == model.ArtistInfo.Id).ToList();
    //  MaxAlbumIndex = albums.Count;
    //  model.AlbumInfo.Current = CurrentAlbumIndex;

    //  if (MaxAlbumIndex > 0)
    //  {
    //    var album = albums[CurrentAlbumIndex];
    //    model.AlbumInfo.Id = album.AlbumId;
    //    model.AlbumInfo.Name = album.Title;
    //    model.AlbumInfo.Max = MaxAlbumIndex;
    //    model.Tracks = context.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList();
    //  }
    //  return model;
    //}
  }
}
