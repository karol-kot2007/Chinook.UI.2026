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
      var albumInfo2 = repo.BuildModel(albumInfo, Repository.Operation.NextArtist);
      var albumInfo3 = repo.BuildModel(albumInfo, Repository.Operation.NextArtist);
      
      Assert.IsNotNull(albumInfo1);
      Assert.IsNotNull(albumInfo2);
      Assert.IsNotNull(albumInfo3);

      Assert.AreNotEqual(albumInfo1.CurrentArtistIndex, albumInfo2.CurrentArtistIndex);
      Assert.AreNotEqual(albumInfo2.CurrentArtistIndex, albumInfo3.CurrentArtistIndex);
      Assert.AreNotEqual(albumInfo1.CurrentArtistIndex, albumInfo3.CurrentArtistIndex);

    }

    [TestMethod]
    public void LoadPrevArtist()
    {
      SQLitePCL.Batteries.Init();
      var repo = new Repository();
      var albumInfo = DoLoadFirstArtist();

      var albumInfo1 = repo.BuildModel(albumInfo, Repository.Operation.PrevArtist);
      var Id1 = albumInfo1.CurrentArtistIndex;

      var albumInfo2 = repo.BuildModel(albumInfo, Repository.Operation.NextArtist);
      var Id2 = albumInfo2.CurrentArtistIndex;


      var albumInfo3 = repo.BuildModel(albumInfo, Repository.Operation.NextArtist);



      albumInfo3 = repo.BuildModel(albumInfo, Repository.Operation.PrevArtist);
      var Id3 = albumInfo3.CurrentArtistIndex;
      Assert.IsNotNull(albumInfo1);
      Assert.IsNotNull(albumInfo2);
      Assert.IsNotNull(albumInfo3);

      Assert.AreEqual(Id2, Id3);
      Assert.AreNotEqual(Id1, Id3);

    }

    [TestMethod]
    public void LoadNextAlbum()
    {
      SQLitePCL.Batteries.Init();
      var repo = new Repository();
      var albumInfo = DoLoadFirstArtist();

      var albumInfo1 = repo.BuildModel(albumInfo);
      var Id1 = albumInfo1.CurrentAlbumIndex;

      var albumInfo2 = repo.BuildModel(albumInfo, Repository.Operation.NextAlbum);
      var Id2 = albumInfo2.CurrentAlbumIndex;


      var albumInfo3 = repo.BuildModel(albumInfo, Repository.Operation.NextAlbum);
      var Id3 = albumInfo3.CurrentAlbumIndex;

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
      var albumInfo = DoLoadFirstArtist();
      var Id1 = albumInfo.CurrentAlbumIndex;

      var artistInfo2 = repo.BuildModel(albumInfo, Repository.Operation.PrevAlbum);
      var Id2 = artistInfo2.CurrentAlbumIndex;


      var artistInfo3 = repo.BuildModel(albumInfo, Repository.Operation.PrevAlbum);
      var Id3 = artistInfo3.CurrentAlbumIndex;

      Assert.IsNotNull(albumInfo);
      Assert.IsNotNull(artistInfo2);
      Assert.IsNotNull(artistInfo3);

      Assert.AreNotEqual(Id2, Id3);
      Assert.AreEqual(Id1, Id3);

    }

    [TestMethod]
    public void LoadFirstArtist()
    {
      SQLitePCL.Batteries.Init();

      DoLoadFirstArtist();
    }

    public ArtistModel DoLoadFirstArtist()
    {
      var repo = new Repository();
      Assert.IsNotNull(repo);
      var albumInfo = repo.BuildModel();
      Assert.IsNotNull(albumInfo);
      return albumInfo;
    }
  }
}
