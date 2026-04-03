using Chinook.DAL;
using Chinook.DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chinook.Models.Repository;

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
      var result = new ArtistModel();
      //var modelToUse = currentModel ?? result;
      //int artistIndex = modelToUse.CurrentArtistIndex;
      //switch (operation)
      //{
      //  case Operation.NextArtist:
      //    artistIndex = modelToUse.ModifyArtistPager(operation);
      //    break;
      //  case Operation.PrevArtist:
      //    artistIndex = modelToUse.ModifyArtistPager(operation);
      //    break;
      //  default:
      //    break;
      //}
      //var artist = ArtistContext.Artists.ElementAt(artistIndex);
      //int maxArtistIndex = ArtistContext.Artists.Count() - 1;
      //var artistInfo = new ArtistInfo(artist, maxArtistIndex, artistIndex);
      //result.AlbumInfo.ArtistInfo = artistInfo;
      //var albums = ArtistContext.Albums.Where(a => a.ArtistId == artistIndex + 1).ToList();
      //int albumIndex = 0;
      //switch (operation)
      //{
      //  case Operation.NextAlbum:
      //    albumIndex = modelToUse.ModifyAlbumIndex(operation);
      //    break;
      //  case Operation.PrevAlbum:
      //    albumIndex = modelToUse.ModifyAlbumIndex(operation);
      //    break;
      //  default:
      //    break;
      //}
      //int MaxAlbumIndex = albums.Count() - 1;
      //if (MaxAlbumIndex > -1)
      //{
      //  var album = albums[albumIndex];
      //  result.AlbumInfo.AlbumInfo.Id = album.AlbumId;
      //  result.AlbumInfo.AlbumInfo.Name = album.Title;
      //  var albumInfo = new AlbumInfo(album, MaxAlbumIndex, albumIndex);
      //  result.AlbumInfo.AlbumInfo = albumInfo;
      //  result.AlbumInfo.Tracks = ArtistContext.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList(); ;
      //}
      return result;
    }
  }
}
