using Chinook.DAL;
using Chinook.DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Models
{
  public class MockedRepository : IRepository
  {
    public List<ArtistModel> artist = new();
    public ArtistContext ArtistContext { get; private set; } = new ArtistContext();
    public MockedRepository()
    {
      //artist.Add(new ArtistModel
      //{
      //  CurrentAlbumIndex = 1,
      //  CurrentArtistIndex = 0,
      //  MaxAlbumIndex = 3,


      //});

      //artist.Add(new ArtistModel
      //{
      //  CurrentAlbumIndex = 2,
      //  CurrentArtistIndex = 2,
      //  MaxAlbumIndex = 2,


      //});
    }
    public ArtistModel BuildModel(ArtistModel? currentModel = null, Repository.Operation? operation = null)
    {
      var artistModel = new ArtistModel();
      //if (currentModel != null)
      //{

      //  artistModel.MaxArtistIndex = ArtistContext.Artists.Count() - 1;
      //  artistModel.CurrentArtistIndex = currentModel.ModifyArtistIndex(artistModel.MaxArtistIndex, operation);
      //}

      //var artist = ArtistContext.Artists.ElementAt(artistModel.CurrentArtistIndex);
      //artistModel.AlbumInfo.ArtistInfo.Name = artist.Name;
      //var albums = ArtistContext.Albums.Where(a => a.ArtistId == artistModel.CurrentArtistIndex + 1).ToList();
      //artistModel.MaxAlbumIndex = albums.Count - 1;

      //if (currentModel != null)
      //{
      //  artistModel.CurrentAlbumIndex = currentModel.ModifyAlbumIndex(artistModel.MaxAlbumIndex, operation);
      //}

      //if (artistModel.MaxAlbumIndex > 0)
      //{
      //  var album = albums[artistModel.CurrentAlbumIndex];
      //  artistModel.AlbumInfo.AlbumInfo.Id = album.AlbumId;
      //  artistModel.AlbumInfo.AlbumInfo.Name = album.Title;
      //  artistModel.AlbumInfo.Tracks = ArtistContext.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList();
      //}
      return artistModel;
    }
  }
}
