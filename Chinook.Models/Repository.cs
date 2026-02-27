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
    ArtistModel BuildModel(ArtistModel? currentModel = null, Operation? operation = null);
  }
  public class Repository : IRepository
  {
    public ArtistContext ArtistContext { get; private set; } = new ArtistContext();
    //public ArtistModel artistModel { get; set; } = new ArtistModel();


    public Repository()
    {


    }
    public enum Operation
    {
      NextArtist, PrevArtist, NextAlbum, PrevAlbum
    }



    public ArtistModel BuildModel(ArtistModel? currentModel = null, Operation? operation = null)
    {
      if (currentModel != null)
      {

        currentModel.MaxArtistIndex = ArtistContext.Artists.Count();
        currentModel.CurrentArtistIndex = currentModel.ModifyArtistIndex(currentModel.MaxArtistIndex, operation);
        var artist = ArtistContext.Artists.ElementAt(currentModel.CurrentArtistIndex);
        currentModel.CurrentArtistIndex = artist.ArtistId;
       
        currentModel.AlbumInfo.ArtistInfo.Name = artist.Name;
        
        var albums = ArtistContext.Albums.Where(a => a.ArtistId == currentModel.CurrentArtistIndex).ToList();
        var MaxAlbumIndex = albums.Count;
        currentModel.CurrentAlbumIndex = currentModel.ModifyAlbumIndex(MaxAlbumIndex, operation);
        if (MaxAlbumIndex > 0)
        {
          var album = albums[currentModel.CurrentAlbumIndex];
          currentModel.AlbumInfo.AlbumInfo.Id = album.AlbumId;
          currentModel.AlbumInfo.AlbumInfo.Name = album.Title;
          currentModel.MaxAlbumIndex = MaxAlbumIndex;
          currentModel.AlbumInfo.Tracks = ArtistContext.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList();
        }
        return currentModel;
      }
      else 
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
        return artistModel;
      }
    
    }

  }
}
