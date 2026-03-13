using Chinook.DAL;
using Chinook.Models;

namespace RealDbTests
{
  [TestClass]
  public class RepositoryTest
  {
    [TestInitialize]
    public void TestIntialize()
    {
      SQLitePCL.Batteries.Init();
    }

    [TestMethod]

    public void LoadNextArtistRealDb()
    {

      var repo = new Repository();
      DoLoadNextArtist(repo);

    }




    [TestMethod]
    public void LoadPrevArtist()
    {
      var repo = new Repository();
      var albumInfo = DoLoadFirstArtist(repo);

      var albumInfo1 = repo.BuildModel(albumInfo, Repository.Operation.PrevArtist);
      var albumInfo2 = repo.BuildModel(albumInfo, Repository.Operation.NextArtist);
      var albumInfo3 = repo.BuildModel(albumInfo, Repository.Operation.NextArtist);
      albumInfo3 = repo.BuildModel(albumInfo, Repository.Operation.PrevArtist);

      Assert.IsNotNull(albumInfo1);
      Assert.IsNotNull(albumInfo2);
      Assert.IsNotNull(albumInfo3);

      Assert.AreEqual(albumInfo2.CurrentArtistIndex, albumInfo3.CurrentArtistIndex);
      Assert.AreNotEqual(albumInfo1.CurrentArtistIndex, albumInfo3.CurrentArtistIndex);

    }

    [TestMethod]
    public void LoadNextAlbum()
    {
      var repo = new Repository();
      var albumInfo = DoLoadFirstArtist(repo);

      var albumInfo1 = repo.BuildModel(albumInfo);
      var albumInfo2 = repo.BuildModel(albumInfo, Repository.Operation.NextAlbum);
      var albumInfo3 = repo.BuildModel(albumInfo, Repository.Operation.NextAlbum);

      Assert.IsNotNull(albumInfo1);
      Assert.IsNotNull(albumInfo2);
      Assert.IsNotNull(albumInfo3);

      Assert.AreNotEqual(albumInfo1.CurrentAlbumIndex, albumInfo2.CurrentAlbumIndex);
      //ac dc ma 2 albumy jesli wyjdzie poza index to zeruje sie 
      Assert.AreEqual(albumInfo1.CurrentAlbumIndex, albumInfo3.CurrentAlbumIndex);


    }

    [TestMethod]
    public void LoadPrevAlbum()
    {
      var repo = new Repository();
      var albumInfo = DoLoadFirstArtist(repo);

      var albumInfo1 = repo.BuildModel(albumInfo);
      var albumInfo2 = repo.BuildModel(albumInfo, Repository.Operation.PrevAlbum);
      var albumInfo3 = repo.BuildModel(albumInfo, Repository.Operation.PrevAlbum);

      Assert.IsNotNull(albumInfo);
      Assert.IsNotNull(albumInfo2);
      Assert.IsNotNull(albumInfo3);

      Assert.AreNotEqual(albumInfo2.CurrentAlbumIndex, albumInfo1.CurrentAlbumIndex);
      Assert.AreEqual(albumInfo1.CurrentAlbumIndex, albumInfo3.CurrentAlbumIndex);

    }

    [TestMethod]
    public void LoadFirstArtistRealDB()
    {
      var repo = new Repository();
      DoLoadFirstArtist(repo);
    }



    public ArtistModel DoLoadFirstArtist(IRepository repo)
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

    private void DoLoadNextArtist(IRepository repo)
    {
      var albumInfo = DoLoadFirstArtist(repo);

      var albumInfo1 = repo.BuildModel(null);
      var albumInfo2 = repo.BuildModel(albumInfo, Repository.Operation.NextArtist);
      var albumInfo3 = repo.BuildModel(albumInfo, Repository.Operation.NextArtist);

      Assert.IsNotNull(albumInfo1);
      Assert.IsNotNull(albumInfo2);
      Assert.IsNotNull(albumInfo3);

      Assert.AreNotEqual(albumInfo1.CurrentArtistIndex, albumInfo2.CurrentArtistIndex);
      Assert.AreNotEqual(albumInfo2.CurrentArtistIndex, albumInfo3.CurrentArtistIndex);
      Assert.AreNotEqual(albumInfo1.CurrentArtistIndex, albumInfo3.CurrentArtistIndex);
    }


  }

}
