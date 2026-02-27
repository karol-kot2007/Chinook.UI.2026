using Chinook.DAL;
using Chinook.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
    public ArtistContext ArtistContext { get; private set; } = new ArtistContext();
    public ArtistModel artistModel { get; set; } = new ArtistModel();


    public Repository()
    {


    }
    public enum Operation
    {
      NextArtist, PrevArtist, NextAlbum, PrevAlbum
    }



    public AlbumInfoModel BuildModel(AlbumInfoModel? currentModel = null, Operation? operation = null)
    {
      if (currentModel != null)
      {

        currentModel.ArtistInfo.Max = ArtistContext.Artists.Count();



        currentModel.ArtistInfo.Current = artistModel.ModifyArtistIndex(currentModel.ArtistInfo.Max, operation);
        var artist = ArtistContext.Artists.ElementAt(currentModel.ArtistInfo.Current);


        currentModel.ArtistInfo.Id = artist.ArtistId;
        currentModel.ArtistInfo.Name = artist.Name;
        currentModel.ArtistInfo.Current = currentModel.ArtistInfo.Current;
        currentModel.ArtistInfo.Max = currentModel.ArtistInfo.Max;
        var albums = ArtistContext.Albums.Where(a => a.ArtistId == currentModel.ArtistInfo.Id).ToList();
        var MaxAlbumIndex = albums.Count;


        currentModel.AlbumInfo.Current = artistModel.ModifyAlbumIndex(MaxAlbumIndex, operation);

        if (MaxAlbumIndex > 0)
        {
          var album = albums[currentModel.AlbumInfo.Current];
          currentModel.AlbumInfo.Id = album.AlbumId;
          currentModel.AlbumInfo.Name = album.Title;
          currentModel.AlbumInfo.Max = MaxAlbumIndex;
          currentModel.Tracks = ArtistContext.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList();
        }
        return currentModel;
      }
      else if (currentModel == null)
      {

        var albumInfoModel = new AlbumInfoModel();
        var artistModel = new ArtistModel();
        artistModel.CurrentAlbumIndex = 1;
        artistModel.MaxArtistIndex = 1;
        var artist = ArtistContext.Artists.ElementAt(artistModel.CurrentArtistIndex);
        albumInfoModel.ArtistInfo.Current = albumInfoModel.ArtistInfo.Current;
        albumInfoModel.ArtistInfo.Id = artist.ArtistId;
        albumInfoModel.ArtistInfo.Name = artist.Name;
        albumInfoModel.ArtistInfo.Max = artistModel.MaxArtistIndex;
        albumInfoModel.ArtistInfo.Current = albumInfoModel.ArtistInfo.Current;
        var albums = ArtistContext.Albums.Where(a => a.ArtistId == albumInfoModel.ArtistInfo.Id).ToList();
        artistModel.MaxAlbumIndex = albums.Count;


        var album = albums[artistModel.CurrentAlbumIndex];


        albumInfoModel.Tracks = ArtistContext.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList();
        return albumInfoModel;
      }
      return null;
    }

  }
}
