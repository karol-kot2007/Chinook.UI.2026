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
      Assert.IsNotNull(albumInfo.AlbumInfo.ArtistInfo.Current);
      Assert.IsNotNull(albumInfo.AlbumInfo.ArtistInfo.Max);

      Assert.IsNotNull(albumInfo.AlbumInfo.AlbumInfo.Name);
      Assert.IsNotNull(albumInfo.AlbumInfo.AlbumInfo.Id);
      Assert.IsNotNull(albumInfo.AlbumInfo.AlbumInfo.Current);
      Assert.IsNotNull(albumInfo.AlbumInfo.AlbumInfo.Max);
      return albumInfo;
    }

    protected void DoLoadNextArtist(IRepository repo)
    {
      var albumInfo = DoLoadFirstArtist(repo);

      var albumInfo1 = repo.BuildModel(null);
      var albumInfo2 = repo.BuildModel(albumInfo1, Repository.Operation.NextArtist);
      var albumInfo3 = repo.BuildModel(albumInfo1, Repository.Operation.NextArtist);

      Assert.IsNotNull(albumInfo1);
      Assert.IsNotNull(albumInfo2);
      Assert.IsNotNull(albumInfo3);

      Assert.AreNotEqual(albumInfo1.CurrentArtistIndex, albumInfo2.CurrentArtistIndex);
      Assert.AreNotEqual(albumInfo2.CurrentArtistIndex, albumInfo3.CurrentArtistIndex);
      Assert.AreNotEqual(albumInfo1.CurrentArtistIndex, albumInfo3.CurrentArtistIndex);
    }


    protected void DoLoadPrevArtist(IRepository repo)
    {
      var albumInfo = DoLoadFirstArtist(repo);

      var albumInfo1 = repo.BuildModel(null);
      var albumInfo2 = repo.BuildModel(albumInfo1, Repository.Operation.PrevArtist);
      var albumInfo3 = repo.BuildModel(albumInfo1, Repository.Operation.PrevArtist);

      Assert.IsNotNull(albumInfo1);
      Assert.IsNotNull(albumInfo2);
      Assert.IsNotNull(albumInfo3);

      Assert.AreNotEqual(albumInfo1.CurrentArtistIndex, albumInfo2.CurrentArtistIndex);
      Assert.AreNotEqual(albumInfo2.CurrentArtistIndex, albumInfo3.CurrentArtistIndex);
      Assert.AreNotEqual(albumInfo1.CurrentArtistIndex, albumInfo3.CurrentArtistIndex);
    }


    protected void DoLoadNextAlbum(IRepository repo)
    {
      var albumInfo = DoLoadFirstArtist(repo);

      var albumInfo1 = repo.BuildModel(null);
      var albumInfo2 = repo.BuildModel(albumInfo1, Repository.Operation.NextAlbum);
      var albumInfo3 = repo.BuildModel(albumInfo1, Repository.Operation.NextAlbum);

      Assert.IsNotNull(albumInfo1);
      Assert.IsNotNull(albumInfo2);
      Assert.IsNotNull(albumInfo3);

      Assert.AreNotEqual(albumInfo1.CurrentAlbumIndex, albumInfo2.CurrentAlbumIndex);
      Assert.AreNotEqual(albumInfo2.CurrentAlbumIndex, albumInfo3.CurrentAlbumIndex);
      Assert.AreEqual(albumInfo1.CurrentAlbumIndex, albumInfo3.CurrentAlbumIndex);
    }

    protected void DoLoadPrevAlbum(IRepository repo)
    {
      var albumInfo = DoLoadFirstArtist(repo);

      var albumInfo1 = repo.BuildModel(null);
      var albumInfo2 = repo.BuildModel(albumInfo1, Repository.Operation.PrevAlbum);
      var albumInfo3 = repo.BuildModel(albumInfo1, Repository.Operation.PrevAlbum);

      Assert.IsNotNull(albumInfo1);
      Assert.IsNotNull(albumInfo2);
      Assert.IsNotNull(albumInfo3);

      Assert.AreNotEqual(albumInfo1.CurrentAlbumIndex, albumInfo2.CurrentAlbumIndex);
      Assert.AreNotEqual(albumInfo2.CurrentAlbumIndex, albumInfo3.CurrentAlbumIndex);
      Assert.AreEqual(albumInfo1.CurrentAlbumIndex, albumInfo3.CurrentAlbumIndex);
    }
  }
}
