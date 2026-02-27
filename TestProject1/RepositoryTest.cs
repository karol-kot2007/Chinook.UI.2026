using Chinook.DAL;
using Chinook.Models;

namespace TestProject1
{
  [TestClass]
  public sealed class RepositoryTest
  {
    [TestMethod]
    public void LoadNextArtist()
    {
      SQLitePCL.Batteries.Init();
      var repo = new Repository();
      var albumInfo = DoLoadFirstArtist();

      var albumInfo1 = repo.BuildModel(null);
      var Id1 = albumInfo1.ArtistInfo.Id;

      var albumInfo2 = repo.BuildModel(albumInfo, Repository.Operation.NextArtist);
      var Id2 = albumInfo2.ArtistInfo.Id;


      var albumInfo3 = repo.BuildModel(albumInfo, Repository.Operation.NextArtist);
      var Id3 = albumInfo3.ArtistInfo.Id;

      Assert.IsNotNull(albumInfo1);
      Assert.IsNotNull(albumInfo2);


      Assert.AreNotEqual(Id1, Id2);
      Assert.AreNotEqual(Id2, Id3);
      Assert.AreNotEqual(Id1, Id3);

    }

    [TestMethod]
    public void LoadPrevArtist()
    {
      SQLitePCL.Batteries.Init();
      var repo = new Repository();
      var albumInfo = new AlbumInfoModel();

      var albumInfo1 = repo.BuildModel(albumInfo, Repository.Operation.PrevArtist);
      var Id1 = albumInfo1.ArtistInfo.Id;

      var albumInfo2 = repo.BuildModel(albumInfo, Repository.Operation.NextArtist);
      var Id2 = albumInfo2.ArtistInfo.Id;


      var albumInfo3 = repo.BuildModel(albumInfo, Repository.Operation.NextArtist);


      albumInfo3 = repo.BuildModel(albumInfo, Repository.Operation.PrevArtist);
      var Id3 = albumInfo3.ArtistInfo.Id;
      Assert.IsNotNull(albumInfo1);
      Assert.IsNotNull(albumInfo2);

      Assert.AreEqual(Id2, Id3);
      Assert.AreNotEqual(Id1, Id3);

    }

    [TestMethod]
    public void LoadNextAlbum()
    {
      SQLitePCL.Batteries.Init();
      var repo = new Repository();
      var albumInfo = new AlbumInfoModel();

      var albumInfo1 = repo.BuildModel(albumInfo);
      var Id1 = albumInfo1.AlbumInfo.Id;

      var albumInfo2 = repo.BuildModel(albumInfo, Repository.Operation.NextAlbum);
      var Id2 = albumInfo2.AlbumInfo.Id;


      var albumInfo3 = repo.BuildModel(albumInfo, Repository.Operation.NextAlbum);
      var Id3 = albumInfo3.AlbumInfo.Id;

      Assert.IsNotNull(albumInfo1);
      Assert.IsNotNull(albumInfo2);
      Assert.IsNotNull(albumInfo3);

      Assert.AreNotEqual(Id1, Id2);
      //ac dc ma 2 albumy jesli wyjdzie poza index to zeruje sie 
      Assert.AreEqual(Id1, Id3);


    }

    [TestMethod]
    public void LoadPrevAlbum()
    {
      SQLitePCL.Batteries.Init();
      var repo = new Repository();
      var artistInfo = new AlbumInfoModel();

      var artistInfo1 = repo.BuildModel(artistInfo);
      var Id1 = artistInfo1.ArtistInfo.Id;

      var artistInfo2 = repo.BuildModel(artistInfo1, Repository.Operation.PrevAlbum);
      var Id2 = artistInfo2.ArtistInfo.Id;


      var artistInfo3 = repo.BuildModel(artistInfo1, Repository.Operation.PrevAlbum);
      var Id3 = artistInfo3.ArtistInfo.Id;

      Assert.IsNotNull(artistInfo1);
      Assert.IsNotNull(artistInfo2);

      Assert.AreEqual(Id2, Id3);
      Assert.AreEqual(Id1, Id3);

    }

    [TestMethod]
    public void LoadFirstArtist()
    {
      SQLitePCL.Batteries.Init();
      DoLoadFirstArtist();
    }

    public AlbumInfoModel DoLoadFirstArtist()
    {
      var repo = new Repository();
      Assert.IsNotNull(repo);
      var albumInfo = repo.BuildModel();
      Assert.IsNotNull(albumInfo);
      return albumInfo;
    }
  }
}
