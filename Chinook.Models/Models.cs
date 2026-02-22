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
    public TrackInfo TrackInfo { get; set; } = new TrackInfo();
    //  public Track trackId { get; set; } = new Track(); 
    public List<Track> Tracks { get; set; }

  }
  public class ArtistModel
  {
    public int CurrentAlbumIndex { get; set; }
    public int CurrentArtistIndex { get; set; }
    public int MaxAlbumIndex { get; set; }
    public int MaxArtistIndex { get; set; }
    public AlbumInfoModel BuildModel(ArtistContext context)
    {
      MaxArtistIndex = context.Artists.Count();

     // MaxAlbumIndex = context.Albums.Count();
      var model = new AlbumInfoModel();
      var artistContext = new ArtistContext();
      //TODO changing props starting with Current... shall be only in button handlers AlbumInfoControl_OnNext... - -d
      //To simplify app add a class member: ArtistContext context and use it where you need to have context e.g. in AlbumInfoControl_OnNext -
      var artist = context.Artists.ElementAt(CurrentArtistIndex);  //TODO use context.Artists.ElementAt(CurrentArtistIndex) -d

      //if (model.AlbumInfo.Id == null)
      //{
      //  return null;
      //}
      //   var artist = context.Artists.First();  //TODO use context.Artists.ElementAt(CurrentArtistIndex)
      model.ArtistInfo.Name = artist.Name;//
      //AlbumInfoControl.ArtistName.Text = model.ArtistInfo.Name;

      model.ArtistInfo.Id = artist.ArtistId;
      model.ArtistInfo.Max = MaxArtistIndex;
      model.ArtistInfo.Current = CurrentArtistIndex;
      var albums = context.Albums.Where(a => a.ArtistId == model.ArtistInfo.Id).ToList();
      MaxAlbumIndex = albums.Count;
      model.AlbumInfo.Current = CurrentAlbumIndex;

      if (MaxAlbumIndex < 1)
        throw new Exception("l");
        var album = albums[CurrentAlbumIndex];//nie dziala bo ssa nule w bazie danych w albumid

        model.AlbumInfo.Id = album.AlbumId;

        model.AlbumInfo.Name = album.Title;
       // AlbumInfoControl.AlbumName.Text = model.AlbumInfo.Name;
        model.AlbumInfo.Max = MaxAlbumIndex;

   
        model.Tracks = context.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList();
      
      return model;
    }
  }
}
