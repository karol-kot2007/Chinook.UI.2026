using Chinook.DAL;
using Chinook.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Models
{
  public class MockedRepository : IRepository
  {
    public List<ArtistModel> artist = new();
    public ArtistContext ArtistContext { get; private set; } = new ArtistContext();
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
    public MockedRepository() {
      artist.Add(new ArtistModel
      {
        CurrentAlbumIndex = 1,
        CurrentArtistIndex = 0,
        MaxAlbumIndex = 3,

        
      });

      artist.Add(new ArtistModel
      {
        CurrentAlbumIndex = 2,
        CurrentArtistIndex = 2,
        MaxAlbumIndex = 2,


      });
    }
    public ArtistModel BuildModel(ArtistModel? currentModel = null, Repository.Operation? operation = null)
    {
      var artistModel = new ArtistModel();
      if (currentModel != null)
      {

        artistModel.MaxArtistIndex = ArtistContext.Artists.Count()-1;
        artistModel.CurrentArtistIndex = currentModel.ModifyArtistIndex(artistModel.MaxArtistIndex, operation);
      }

      var artist = CreateArtist(artistModel);
      BindProps(artistModel, artist);
      var albums = CreateAlbums(artistModel);
      artistModel.MaxAlbumIndex = albums.Count-1;

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
