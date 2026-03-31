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
    protected void TestIntialize()
    {
      SQLitePCL.Batteries.Init();
    }

    protected ArtistModel DoLoadFirstArtist(IRepository repo)
    {
      Assert.IsNotNull(repo);
      var albumInfo = repo.BuildModel();
      Assert.IsNotNull(albumInfo);
      Assert.IsNotNull(albumInfo.AlbumInfo.ArtistInfo.Name);
      Assert.IsNotNull(albumInfo.AlbumInfo.ArtistInfo.Id);
      Assert.IsNotNull(albumInfo.AlbumInfo.ArtistInfo.CurrentIndex);
      Assert.IsNotNull(albumInfo.AlbumInfo.ArtistInfo.MaxIndex);

      Assert.IsNotNull(albumInfo.AlbumInfo.AlbumInfo.Name);
      Assert.IsNotNull(albumInfo.AlbumInfo.AlbumInfo.Id);
      Assert.IsNotNull(albumInfo.AlbumInfo.AlbumInfo.CurrentIndex);
      Assert.IsNotNull(albumInfo.AlbumInfo.AlbumInfo.MaxIndex);
      return albumInfo;
    }
    protected void DoIndexOperation(IRepository repo, Repository.Operation op)
    {
      var albumInfo = DoLoadFirstArtist(repo);
      var albumInfo1 = repo.BuildModel(null);
      var albumInfo2 = repo.BuildModel(albumInfo1, op);
      var albumInfo3 = repo.BuildModel(albumInfo1, op);
      Assert.IsNotNull(albumInfo1);
      Assert.IsNotNull(albumInfo2);
      Assert.IsNotNull(albumInfo3);
      switch (op)
      {
        case Repository.Operation.NextArtist:
          Assert.AreEqual(albumInfo1.CurrentArtistIndex, 0);
          Assert.AreEqual(albumInfo2.CurrentArtistIndex, 1);
          Assert.AreEqual(albumInfo3.CurrentArtistIndex, 2);
          Assert.AreNotEqual(albumInfo1.CurrentArtistIndex, albumInfo2.CurrentArtistIndex);
          Assert.AreNotEqual(albumInfo2.CurrentArtistIndex, albumInfo3.CurrentArtistIndex);
          Assert.AreEqual(albumInfo1.CurrentAlbumIndex, albumInfo3.CurrentAlbumIndex);
          break;
        case Repository.Operation.PrevArtist:
          Assert.AreEqual(albumInfo1.CurrentArtistIndex, 0);
          Assert.AreEqual(albumInfo2.CurrentArtistIndex, albumInfo2.MaxArtistIndex);
          Assert.AreEqual(albumInfo3.CurrentArtistIndex, albumInfo3.MaxArtistIndex - 1);
          Assert.AreNotEqual(albumInfo1.CurrentArtistIndex, albumInfo2.CurrentArtistIndex);
          Assert.AreNotEqual(albumInfo2.CurrentArtistIndex, albumInfo3.CurrentArtistIndex);
          Assert.AreNotEqual(albumInfo1.CurrentArtistIndex, albumInfo3.CurrentArtistIndex);
          break;
        case Repository.Operation.NextAlbum:
          //1 artysta ma 2 albumy
          Assert.AreEqual(albumInfo1.CurrentAlbumIndex, 0);
          Assert.AreEqual(albumInfo2.CurrentAlbumIndex, 1);
          Assert.AreEqual(albumInfo3.CurrentAlbumIndex, 0);
          Assert.AreNotEqual(albumInfo1.CurrentAlbumIndex, albumInfo2.CurrentAlbumIndex);
          Assert.AreNotEqual(albumInfo2.CurrentAlbumIndex, albumInfo3.CurrentAlbumIndex);
          Assert.AreEqual(albumInfo1.CurrentAlbumIndex, albumInfo3.CurrentAlbumIndex);
          break;
        case Repository.Operation.PrevAlbum:
          // ac/dc ma 2 albumy 
          Assert.AreEqual(albumInfo1.CurrentAlbumIndex, 0);
          Assert.AreEqual(albumInfo2.CurrentAlbumIndex, 1);
          Assert.AreEqual(albumInfo3.CurrentAlbumIndex, 0);
          Assert.AreNotEqual(albumInfo1.CurrentAlbumIndex, albumInfo2.CurrentAlbumIndex);
          Assert.AreNotEqual(albumInfo2.CurrentAlbumIndex, albumInfo3.CurrentAlbumIndex);
          Assert.AreEqual(albumInfo1.CurrentAlbumIndex, albumInfo3.CurrentAlbumIndex);
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
