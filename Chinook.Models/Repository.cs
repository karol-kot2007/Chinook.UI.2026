using Chinook.DAL;
using Chinook.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chinook.Models.Repository;

namespace Chinook.Models
{

  public interface IRepository
  {
    //AlbumInfoModel BuildModel();
    AlbumInfoModel BuildModel(AlbumInfoModel? currentModel = null, Operation? operation = null);
  }
  public class Repository : IRepository
  {
    public ArtistContext ArtistContext { get; set; }
    // public ArtistModel ArtistModel { get; set; }
    public int MaxAlbumIndex { get; set; }
    public int MaxArtistIndex { get; set; }

    public Repository()
    {


    }
    public enum Operation
    {
      NextArtist, PrevArtist, NextTrack, PrevTrack
    }


    public AlbumInfoModel BuildModel(AlbumInfoModel? currentModel = null, Operation? operation = null)
    {
      var a = new ArtistModel();
      var ArtistContext = new ArtistContext();
      if (currentModel != null)
      {

        var artist = ArtistContext.Artists.ElementAt(a.CurrentArtistIndex);
        MaxArtistIndex = ArtistContext.Artists.Count();
        currentModel.ArtistInfo.Name = artist.Name;
        currentModel.ArtistInfo.Id = artist.ArtistId;
        currentModel.ArtistInfo.Max = MaxArtistIndex;
        currentModel.ArtistInfo.Current = a.CurrentArtistIndex;
        var albums = ArtistContext.Albums.Where(a => a.ArtistId == currentModel.ArtistInfo.Id).ToList();
        MaxAlbumIndex = albums.Count;
        currentModel.AlbumInfo.Current = a.CurrentAlbumIndex;

        if (MaxAlbumIndex > 0)
        {
          var album = albums[a.CurrentAlbumIndex];
          currentModel.AlbumInfo.Id = album.AlbumId;
          currentModel.AlbumInfo.Name = album.Title;
          currentModel.AlbumInfo.Max = MaxAlbumIndex;
          currentModel.Tracks = ArtistContext.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList();
        }
        return currentModel;
      }
      else if (currentModel == null)
      {
        //dziala
        var albumInfoModel = new AlbumInfoModel();
        var artist = ArtistContext.Artists.ElementAt(0);
        albumInfoModel.ArtistInfo.Current = a.CurrentArtistIndex;
        albumInfoModel.ArtistInfo.Id = artist.ArtistId;
        albumInfoModel.ArtistInfo.Name = artist.Name;

        albumInfoModel.ArtistInfo.Max = 2;
        albumInfoModel.ArtistInfo.Current = a.CurrentArtistIndex;
        var albums = ArtistContext.Albums.Where(a => a.ArtistId == albumInfoModel.ArtistInfo.Id).ToList();
        MaxAlbumIndex = albums.Count;
        a.CurrentAlbumIndex = 1;
        MaxArtistIndex = 2;
        var album = albums[a.CurrentAlbumIndex];


        albumInfoModel.Tracks = ArtistContext.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList();
        return albumInfoModel;
      }
      return null;
    }
    //public AlbumInfoModel BuildModel(AlbumInfoModel? currentModel = null, Operation? operation= null)
    //{

    //  var a = new ArtistModel();
    //  var model = new AlbumInfoModel();
    //  a.MaxArtistIndex = ArtistContext.Artists.Count();
    //  //var artist = ArtistContext.Artists.ElementAt(a.CurrentArtistIndex);
    //  //model.ArtistInfo.Name = artist.Name;
    //  //model.ArtistInfo.Id = artist.ArtistId;
    //  model.ArtistInfo.Max = a.MaxArtistIndex;
    //  model.ArtistInfo.Current = a.CurrentArtistIndex;
    //  var albums = ArtistContext.Albums.Where(a => a.ArtistId == model.ArtistInfo.Id).ToList();
    //  a.MaxAlbumIndex = albums.Count;
    //  model.AlbumInfo.Current = a.CurrentAlbumIndex;
    //  if(currentModel == null)
    //  {
    //    var album = albums[1];
    //    model.AlbumInfo.Id = album.AlbumId;
    //    model.AlbumInfo.Name = album.Title;
    //    model.AlbumInfo.Max = a.MaxAlbumIndex;
    //    model.Tracks = ArtistContext.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList();
    //    a.CurrentArtistIndex = 1;
    //    var artist = ArtistContext.Artists.ElementAt(a.CurrentArtistIndex);
    //    model.ArtistInfo.Name = artist.Name;
    //    model.ArtistInfo.Id = artist.ArtistId;
    //  }
    //  else { 
    //  }
    //  //if (a.MaxAlbumIndex > 0)
    //  //{
    //  //  var album = albums[a.CurrentAlbumIndex];
    //  //  model.AlbumInfo.Id = album.AlbumId;
    //  //  model.AlbumInfo.Name = album.Title;
    //  //  model.AlbumInfo.Max = a.MaxAlbumIndex;
    //  //  model.Tracks = ArtistContext.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList();
    //  //}
    //  return model;
    //}
  }

}
