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

    BaseOperation GetBaseOperation(Operation operation)
    {
      if (operation == Operation.NextArtist || operation == Operation.NextAlbum)
        return BaseOperation.Next;

      return BaseOperation.Prev;
    }

    AlbumInfo LoadAlbumInfoAtNextPagerPosition(int artistID, Pager albumPager, Operation? operation = null)
    {
      var newAlbumInfo = new AlbumInfo();
      
      var albums = ArtistContext.Albums.Where(a => a.ArtistId == artistID).ToList();
      albumPager.MaxIndex = albums.Count() - 1;
      
      var baseOper = GetBaseOperation(operation.Value);
      var newPagerIndex = albumPager.ModifyCurrent(baseOper);
      if (albumPager.MaxIndex > -1)
      {
        var album = albums[albumPager.CurrentIndex];
        newAlbumInfo = new AlbumInfo(album);
        newAlbumInfo.Tracks = ArtistContext.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList(); 
      }

      return newAlbumInfo;
    }

    public ArtistModel BuildModel(ArtistModel? currentModel = null, Operation? operation = null)
    {
      var modelToUse = currentModel ?? new ArtistModel();
      return BuildModel(modelToUse.MusicModel.ArtistPager.CloneTyped(), modelToUse.MusicModel.AlbumPager.CloneTyped(), modelToUse.MusicModel.ArtistInfo.CloneTyped(), operation);
    }

    public ArtistModel BuildModel(Pager artistPager, Pager albumPager, ArtistInfo artistInfo, Operation? operation = null)
    {
      var result = new ArtistModel();
      artistPager.MaxIndex = ArtistContext.Artists.Count() - 1;
      var baseOper = GetBaseOperation(operation.Value);
      ArtistInfo newArtistInfo = artistInfo;
      if (operation == Operation.NextArtist || operation == Operation.PrevArtist)
      {
        var newArtistPagerIndex = artistPager.ModifyCurrent(baseOper);
        var artist = ArtistContext.Artists.ElementAt(newArtistPagerIndex);
        newArtistInfo = new ArtistInfo(artist, artistPager);
        operation = Operation.NextAlbum;
        albumPager = new Pager();
      }
      
      result.MusicModel.ArtistInfo = newArtistInfo;
      result.MusicModel.ArtistPager = artistPager;

      var albumInfo = LoadAlbumInfoAtNextPagerPosition(newArtistInfo.Id, albumPager, operation);
      result.MusicModel.AlbumInfo = albumInfo;
      result.MusicModel.AlbumPager = albumPager;
      return result;
    }
  }
}
