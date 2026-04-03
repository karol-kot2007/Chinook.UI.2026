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
    ArtistModel BuildModel(ArtistModel? currentModel = null, Operation? operation = null);
  }
  public class Repository : IRepository
  {
    public ArtistContext ArtistContext { get; private set; } = new ArtistContext();
    public enum Operation
    {
      NextArtist, PrevArtist, NextAlbum, PrevAlbum
    }
    public ArtistModel BuildModel(ArtistModel? currentModel = null, Operation? operation = null)
    {
      var result = new ArtistModel();
      int maxArtistIndex = ArtistContext.Artists.Count() - 1;
      var modelToUse = currentModel ?? result;
      int artistIndex = -1;
      if(operation == Operation.NextArtist || operation == Operation.PrevArtist)
        artistIndex = modelToUse.ModifyArtistIndex(operation);
      if (artistIndex == -1)
        artistIndex = modelToUse.CurrentArtistIndex;
      var artist = ArtistContext.Artists.ElementAt(artistIndex);
      var artistInfo = new ArtistInfo(artist, maxArtistIndex, artistIndex);
      modelToUse.AlbumInfo.ArtistInfo = artistInfo;

      //albums
      var albums = ArtistContext.Albums.Where(a => a.ArtistId == artistIndex + 1).ToList();
      int albumIndex = 0;
      if (operation == Operation.NextAlbum || operation == Operation.PrevAlbum)
          albumIndex = modelToUse.ModifyAlbumIndex(operation);
      int MaxAlbumIndex = albums.Count() - 1;
      if (MaxAlbumIndex > -1)
      {
        var album = albums[albumIndex];
        modelToUse.AlbumInfo.AlbumInfo.Id = album.AlbumId;
        modelToUse.AlbumInfo.AlbumInfo.Name = album.Title;
        var albumInfo = new AlbumInfo(album, MaxAlbumIndex, albumIndex);
        modelToUse.AlbumInfo.AlbumInfo = albumInfo;
        modelToUse.AlbumInfo.Tracks = ArtistContext.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList(); ;
      }
      return modelToUse;
    }
  }
}
