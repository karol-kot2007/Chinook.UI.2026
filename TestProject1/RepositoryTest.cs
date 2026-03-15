using Chinook.DAL;
using Chinook.Models;
using Chinook.Tests;

namespace RealDbTests
{
  [TestClass]
  public class RepositoryTest : TestBase
  {
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
      DoLoadPrevArtist(repo);
    }

    [TestMethod]
    public void LoadNextAlbum()
    {
      var repo = new Repository();
      DoLoadNextAlbum(repo);
    }

    [TestMethod]
    public void LoadPrevAlbum()
    {
      var repo = new Repository();
      DoLoadPrevAlbum(repo);
    }

    [TestMethod]
    public void LoadFirstArtistRealDB()
    {
      var repo = new Repository();
      DoLoadFirstArtist(repo);
    }

  }

}
