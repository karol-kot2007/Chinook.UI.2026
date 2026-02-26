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
       var albumInfo = new AlbumInfoModel();

     var build1 = repo.BuildModel(albumInfo); 
     var Id1 = build1.ArtistInfo.Id;

     var build2 = repo.BuildModel(albumInfo, Repository.Operation.NextArtist);
     var Id2 = build2.ArtistInfo.Id;


      var build3 = repo.BuildModel(albumInfo, Repository.Operation.NextArtist);
      var Id3 = build3.ArtistInfo.Id;

      Assert.IsNotNull(build1);
      Assert.IsNotNull(build2);


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

      var build1 = repo.BuildModel(albumInfo, Repository.Operation.PrevArtist);
      var Id1 = build1.ArtistInfo.Id;

      var build2 = repo.BuildModel(albumInfo, Repository.Operation.NextArtist);
      var Id2 = build2.ArtistInfo.Id;


      var build3 = repo.BuildModel(albumInfo, Repository.Operation.NextArtist);
      

      build3 = repo.BuildModel(albumInfo, Repository.Operation.PrevArtist);
      var Id3 = build3.ArtistInfo.Id;
      Assert.IsNotNull(build1);
      Assert.IsNotNull(build2);

     Assert.AreEqual(Id2, Id3);
      Assert.AreNotEqual(Id1, Id3);

    }

    [TestMethod]
    public void LoadNextAlbum()
    {
      SQLitePCL.Batteries.Init();
      var repo = new Repository();
      var albumInfo = new AlbumInfoModel();

      var build1 = repo.BuildModel(albumInfo);
      var Id1 = build1.AlbumInfo.Id;

      var build2 = repo.BuildModel(albumInfo, Repository.Operation.NextAlbum);
      var Id2 = build2.AlbumInfo.Id;


      var build3 = repo.BuildModel(albumInfo, Repository.Operation.NextAlbum);
      var Id3 = build3.AlbumInfo.Id;

      Assert.IsNotNull(build1);
      Assert.IsNotNull(build2);
      Assert.IsNotNull(build3);

      Assert.AreNotEqual(Id1, Id2);
      //ac dc ma 2 albumy jesli wyjdzie poza index to zeruje sie 
      Assert.AreEqual(Id1, Id3);


    }

    [TestMethod]
    public void LoadPrevAlbum()
    {
      SQLitePCL.Batteries.Init();
      var repo = new Repository();
      var albumInfo = new AlbumInfoModel();

      var build1 = repo.BuildModel(albumInfo);
      var Id1 = build1.ArtistInfo.Id;

      var build2 = repo.BuildModel(albumInfo, Repository.Operation.PrevAlbum);
      var Id2 = build2.ArtistInfo.Id;


      var build3 = repo.BuildModel(albumInfo, Repository.Operation.PrevAlbum);
      var Id3 = build3.ArtistInfo.Id;

      Assert.IsNotNull(build1);
      Assert.IsNotNull(build2);

      Assert.AreEqual(Id2, Id3);
      Assert.AreEqual(Id1, Id3);

    }

    [TestMethod]
    public void LoadFirstArtist()
    {
      SQLitePCL.Batteries.Init();

      var repo = new Repository();
      Assert.IsNotNull(repo);

      var build = repo.BuildModel();
      Assert.IsNotNull(build);
    }
  }
}
