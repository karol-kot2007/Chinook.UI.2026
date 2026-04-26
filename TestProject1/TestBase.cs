using Chinook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Tests
{
  [TestClass]
  public class TestBase
  {
    [TestInitialize]
    public void TestIntialize()
    {
      SQLitePCL.Batteries.Init();
    }

    protected ArtistModel DoLoadFirstArtist(IRepository repo)
    {
      Assert.IsNotNull(repo);
      var albumInfo = repo.BuildModel(null,Repository.Operation.NextArtist);
      Assert.IsNotNull(albumInfo);
      Assert.IsNotNull(albumInfo.MusicModel.ArtistInfo.Name);
      Assert.IsNotNull(albumInfo.MusicModel.ArtistInfo.Id);
      Assert.IsNotNull(albumInfo.CurrentArtistIndex);
      Assert.IsNotNull(albumInfo.MaxArtistIndex);

      Assert.IsNotNull(albumInfo.MusicModel.AlbumInfo.Name);
      Assert.IsNotNull(albumInfo.MusicModel.AlbumInfo.Id);
      Assert.IsNotNull(albumInfo.CurrentAlbumIndex);
      Assert.IsNotNull(albumInfo.MaxAlbumIndex);
      return albumInfo;
    }
    protected void DoIndexOperation(IRepository repo, Repository.Operation op)
    {
      var albumInfo = DoLoadFirstArtist(repo);
      var albumInfo1 = repo.BuildModel(albumInfo, op); 
      var albumInfo2 = repo.BuildModel(albumInfo1, op); 
      var albumInfo3 = repo.BuildModel(albumInfo2, op); 

      switch (op)
      {
        case Repository.Operation.NextArtist:
          Assert.AreEqual(1, albumInfo1.CurrentArtistIndex);
          Assert.AreEqual(2, albumInfo2.CurrentArtistIndex);
          Assert.AreEqual(3, albumInfo3.CurrentArtistIndex);
          break;
        case Repository.Operation.PrevArtist:
          Assert.AreEqual(albumInfo.CurrentArtistIndex, 0);
          Assert.AreEqual(albumInfo1.CurrentArtistIndex, albumInfo1.MaxArtistIndex);
          Assert.AreEqual(albumInfo2.CurrentArtistIndex, albumInfo2.MaxArtistIndex - 1);
          Assert.AreNotEqual(albumInfo.CurrentArtistIndex, albumInfo1.CurrentArtistIndex);
          Assert.AreNotEqual(albumInfo1.CurrentArtistIndex, albumInfo2.CurrentArtistIndex);
          Assert.AreNotEqual(albumInfo.CurrentArtistIndex, albumInfo2.CurrentArtistIndex);
          break;
        case Repository.Operation.NextAlbum:
          //1 artysta ma 2 albumy
          Assert.AreEqual(albumInfo.CurrentAlbumIndex, 0);
          Assert.AreEqual(albumInfo1.CurrentAlbumIndex, 1);
          Assert.AreEqual(albumInfo2.CurrentAlbumIndex, 0);
          Assert.AreNotEqual(albumInfo.CurrentAlbumIndex, albumInfo1.CurrentAlbumIndex);
          Assert.AreNotEqual(albumInfo1.CurrentAlbumIndex, albumInfo2.CurrentAlbumIndex);
          Assert.AreEqual(albumInfo.CurrentAlbumIndex, albumInfo2.CurrentAlbumIndex);
          break;
        case Repository.Operation.PrevAlbum:
          // ac/dc ma 2 albumy 
          Assert.AreEqual(albumInfo.CurrentAlbumIndex, 0);
          Assert.AreEqual(albumInfo1.CurrentAlbumIndex, 1);
          Assert.AreEqual(albumInfo2.CurrentAlbumIndex, 0);
          Assert.AreNotEqual(albumInfo.CurrentAlbumIndex, albumInfo1.CurrentAlbumIndex);
          Assert.AreNotEqual(albumInfo1.CurrentAlbumIndex, albumInfo2.CurrentAlbumIndex);
          Assert.AreEqual(albumInfo.CurrentAlbumIndex, albumInfo2.CurrentAlbumIndex);
          break;
      }

    }
    protected void DoLoadNextArtist(IRepository repo)
    {
      DoIndexOperation(repo, Repository.Operation.NextArtist);
    }

    protected void DoLoadPrevArtist(IRepository repo)
    {
      DoIndexOperation(repo, Repository.Operation.PrevArtist);
    }

    protected void DoLoadNextAlbum(IRepository repo)
    {
      DoIndexOperation(repo, Repository.Operation.NextAlbum);
    }

    protected void DoLoadPrevAlbum(IRepository repo)
    {
      DoIndexOperation(repo, Repository.Operation.PrevAlbum);
    }
  }
}
