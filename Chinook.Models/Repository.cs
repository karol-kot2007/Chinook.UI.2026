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


    public Repository()
    {


    }
    public enum Operation
    {
      NextArtist, PrevArtist, NextAlbum, PrevAlbum
    }

    private void BindProps(ArtistModel artistModel, Artist artist)
    {
    // artistModel.CurrentArtistIndex = artist.ArtistId;
      artistModel.AlbumInfo.ArtistInfo.Name = artist.Name;
    }

    private List<Album> CreateAlbums(ArtistModel artistModel)
    {
      var albums = ArtistContext.Albums.Where(a => a.ArtistId == artistModel.CurrentArtistIndex+1).ToList();
      return albums;
    }

    private Album CreateAlbum(List<Album> albums, ArtistModel artistModel)
    {
      var album = albums[artistModel.CurrentAlbumIndex];
      return album;
    }
    private Artist CreateArtist(ArtistModel artistModel)
    {
      var artist = ArtistContext.Artists.ElementAt(artistModel.CurrentArtistIndex);
      return artist;
    }

    private List<Track> CreateTracks(Album album)
    {
      var tracks = ArtistContext.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList();
      return tracks;
    }
    public ArtistModel BuildModel(ArtistModel? currentModel = null, Operation? operation = null)
    {
      var artistModel = new ArtistModel();
      artistModel.MaxArtistIndex = ArtistContext.Artists.Count();
      if (currentModel != null)
      {
       
        artistModel.CurrentArtistIndex = currentModel.ModifyArtistIndex(artistModel.MaxArtistIndex, operation);
      }
  
      
      var artist = CreateArtist(artistModel);
      BindProps(artistModel, artist);
      var albums = CreateAlbums(artistModel);
      artistModel.MaxAlbumIndex = albums.Count;

      if (currentModel != null)
      {
        artistModel.CurrentAlbumIndex = currentModel.ModifyAlbumIndex(artistModel.MaxAlbumIndex, operation);
      }

      if (artistModel.MaxAlbumIndex > 0)
      {
        var album = CreateAlbum(albums, artistModel);
        artistModel.AlbumInfo.AlbumInfo.Id = album.AlbumId;
        artistModel.AlbumInfo.AlbumInfo.Name = album.Title;
        artistModel.AlbumInfo.Tracks = CreateTracks(album);
      }
      return artistModel;
    }

  }
}
