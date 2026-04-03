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

    AlbumInfo LoadAlbumInfoAtNextPagerPosition(int artistID, AlbumInfo albumInfo)
    {
      var newAlbumInfo = new AlbumInfo();
      //albums
      //var albums = ArtistContext.Albums.Where(a => a.ArtistId == artistID).ToList();

      //Pager newAlbumPager = modelToUse.AlbumInfo.AlbumInfo.Pager;
      //if (resetAlbumPager)
      //  newAlbumPager = new Pager();
      //if (operation == Operation.NextAlbum || operation == Operation.PrevAlbum)
      //  newAlbumPager = modelToUse.ModifyAlbumPager(operation);
      //int maxAlbumIndex = albums.Count() - 1;
      //if (maxAlbumIndex > -1)
      //{
      //  var album = albums[newAlbumPager.CurrentIndex];
      //  var albumInfo = new AlbumInfo(album, newAlbumPager);
      //  modelToUse.AlbumInfo.AlbumInfo = albumInfo;
      //  modelToUse.AlbumInfo.Tracks = ArtistContext.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList(); ;
      //}

      return newAlbumInfo;
    }

    public ArtistModel BuildModel(ArtistModel? currentModel = null, Operation? operation = null)
    {
      //Pager prevArtistPager = currentModel != null ? currentModel.a
      //var result = new ArtistModel();
      var modelToUse = currentModel ?? new ArtistModel();
      //Pager newArtistPager = modelToUse.AlbumInfo.ArtistInfo.Pager;
      //ArtistInfo artistInfo = null;
      //if (operation == Operation.NextArtist || operation == Operation.PrevArtist)
      //{
      //  newArtistPager = modelToUse.ModifyArtistPager(operation);
      //  var artist = ArtistContext.Artists.ElementAt(newArtistPager.CurrentIndex);
      //  artistInfo = new ArtistInfo(artist, newArtistPager);
      //  modelToUse.AlbumInfo.ArtistInfo = artistInfo;
      //}
      //else
      //{
      //  artistInfo = modelToUse.AlbumInfo.ArtistInfo;
      //}

      //var albumInfo = LoadAlbumInfoAtNextPagerPosition(artistInfo.Id, modelToUse.AlbumInfo.AlbumInfo);

      return BuildModel(modelToUse.MusicModel.ArtistInfo.Pager.CloneTyped(), modelToUse.MusicModel.AlbumInfo.Pager.CloneTyped(), operation);
    }

    BaseOperation GetBaseOperation(Operation operation)
    {
      if (operation == Operation.NextArtist || operation == Operation.NextAlbum)
        return BaseOperation.Next;

      return BaseOperation.Prev;
    }

    public ArtistModel BuildModel(Pager artistPager, Pager albumPager, Operation? operation = null)
    {
      var result = new ArtistModel();
      int maxArtistIndex = ArtistContext.Artists.Count() - 1;
      var baseOper = GetBaseOperation(operation.Value);
      ArtistInfo artistInfo = null;
      if (operation == Operation.NextArtist || operation == Operation.PrevArtist)
      {
        var newArtistPagerIndex = artistPager.ModifyCurrent(baseOper);
        var artist = ArtistContext.Artists.ElementAt(newArtistPagerIndex);
        artistInfo = new ArtistInfo(artist, artistPager);
        result.MusicModel.ArtistInfo = artistInfo;
      }
      else
      {
        //artistInfo = modelToUse.AlbumInfo.ArtistInfo;
      }
      return null;
    }
  }
}
