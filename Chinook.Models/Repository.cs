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
    public ArtistContext ArtistContext { get; set; } = new ArtistContext();
    public ArtistModel artistModel { get; set; } = new ArtistModel();
    public int MaxAlbumIndex { get; set; }
    public int MaxArtistIndex { get; set; } 

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
        MaxArtistIndex = ArtistContext.Artists.Count();
        switch (operation)
        {
          case Operation.NextArtist:
            artistModel.CurrentArtistIndex++;
            artistModel.CurrentAlbumIndex = 0;
            if (artistModel.CurrentArtistIndex == MaxArtistIndex)
            {
              artistModel.CurrentArtistIndex = 0;
            }
            break;

          case Operation.PrevArtist:
            artistModel.CurrentArtistIndex--;
            artistModel.CurrentAlbumIndex = 0;
            if (artistModel.CurrentArtistIndex < 0)
            {
              artistModel.CurrentArtistIndex = MaxArtistIndex - 1;
            }
            break;


        }
        var artist = ArtistContext.Artists.ElementAt(artistModel.CurrentArtistIndex);


        currentModel.ArtistInfo.Id = artist.ArtistId;
        currentModel.ArtistInfo.Name = artist.Name;
        currentModel.ArtistInfo.Current = artistModel.CurrentArtistIndex;
        currentModel.ArtistInfo.Max = MaxArtistIndex;
        var albums = ArtistContext.Albums.Where(a => a.ArtistId == currentModel.ArtistInfo.Id).ToList();
        MaxAlbumIndex = albums.Count;
        artistModel.MaxAlbumIndex = MaxAlbumIndex;
        switch (operation)
        {

          case Operation.NextAlbum:
            artistModel.CurrentAlbumIndex++;
            if (artistModel.CurrentAlbumIndex == artistModel.MaxAlbumIndex)
            {
              artistModel.CurrentAlbumIndex = 0;

            }
            break;

          case Operation.PrevAlbum:
            artistModel.CurrentAlbumIndex--;
            if (artistModel.CurrentAlbumIndex < 0)
            {
              artistModel.CurrentAlbumIndex = artistModel.MaxAlbumIndex - 1;
            }
            break;
        }
        currentModel.AlbumInfo.Current = artistModel.CurrentAlbumIndex;

        if (MaxAlbumIndex > 0)
        {
          var album = albums[artistModel.CurrentAlbumIndex];
          currentModel.AlbumInfo.Id = album.AlbumId;
          currentModel.AlbumInfo.Name = album.Title;
          currentModel.AlbumInfo.Max = MaxAlbumIndex;
          currentModel.Tracks = ArtistContext.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList();
        }
        return currentModel;
      }
      else if (currentModel == null)
      {

        var artist = ArtistContext.Artists.ElementAt(artistModel.CurrentArtistIndex);
        var albumInfoModel = new AlbumInfoModel();
        albumInfoModel.ArtistInfo.Current = artistModel.CurrentArtistIndex;
        albumInfoModel.ArtistInfo.Id = artist.ArtistId;
        albumInfoModel.ArtistInfo.Name = artist.Name;
        MaxArtistIndex = 1;
        albumInfoModel.ArtistInfo.Max = MaxArtistIndex;
        albumInfoModel.ArtistInfo.Current = artistModel.CurrentArtistIndex;
        var albums = ArtistContext.Albums.Where(a => a.ArtistId == albumInfoModel.ArtistInfo.Id).ToList();
        MaxAlbumIndex = albums.Count;
        artistModel.CurrentAlbumIndex = 1;

        var album = albums[artistModel.CurrentAlbumIndex];


        albumInfoModel.Tracks = ArtistContext.Tracks.Where(i => i.AlbumId == album.AlbumId).ToList();
        return albumInfoModel;
      }
      return null;
    }

  }
}
