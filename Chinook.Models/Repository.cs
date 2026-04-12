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
  /// <summary>
  /// Interface of repo
  /// </summary>
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
      var artistModel = new ArtistModel();
      artistModel.MaxArtistIndex = ArtistContext.Artists.Count() - 1;
      if (currentModel != null)
      {
        artistModel.CurrentArtistIndex = currentModel.ModifyArtistIndex(artistModel.MaxArtistIndex, operation);
      }
      var artist = ArtistContext.Artists.ElementAt(artistModel.CurrentArtistIndex);
      artistModel.AlbumInfo.ArtistInfo.Name = artist.Name;
      //zmienic indeksowanie , na 0 zeby dzialalo
      var albums = ArtistContext.Albums.Where(a => a.ArtistId == artistModel.CurrentArtistIndex + 1).ToList();
      artistModel.MaxAlbumIndex = albums.Count - 1;
      if (currentModel != null)
      {
        artistModel.CurrentAlbumIndex = currentModel.ModifyAlbumIndex(artistModel.MaxAlbumIndex, operation);
      }

      if (artistModel.MaxAlbumIndex > -1)
      {
        var album = albums[artistModel.CurrentAlbumIndex];
        artistModel.AlbumInfo.AlbumInfo.Id = album.AlbumId;
        artistModel.AlbumInfo.AlbumInfo.Name = album.Title;
        artistModel.AlbumInfo.Tracks = ArtistContext.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList(); ;
      }
      return artistModel;
    }

  }
}
