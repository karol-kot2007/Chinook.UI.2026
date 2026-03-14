using Chinook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealDbTests;

namespace Chinook.Tests
{
  [TestClass]
  public class MockedRepositoryTest : TestBase
  {
    [TestMethod]
    public void LoadFirstArtistMockedDB()
    {
      var repo = new MockedRepository();
      DoLoadFirstArtist(repo);
    }


    [TestMethod]

    public void MockedDbLoadNextAlbum()
    {
      var repo = new MockedRepository();
      DoLoadNextAlbum(repo);
    }


    [TestMethod]

    public void MockedDbLoadNextArtist()
    {
      var repo = new MockedRepository();
      DoLoadNextArtist(repo);
    }

    [TestMethod]

    public void MockedDbLoadPrevArtist()
    {
      var repo = new MockedRepository();
      DoLoadPrevArtist(repo);
    }
    [TestMethod]

    public void MockedDbLoadPrevAlbum()
    {
      var repo = new MockedRepository();
      DoLoadPrevAlbum(repo);
    }

  }
}
