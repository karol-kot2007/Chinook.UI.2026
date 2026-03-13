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
  public class MockedRepositoryTest
  {
    [TestInitialize]
    public void TestIntialize()
    {
      SQLitePCL.Batteries.Init();
    }

    RepositoryTest test = new();


    [TestMethod]
    public void LoadFirstArtistMockedDB()
    {
      var repo = new MockedRepository();
      test.DoLoadFirstArtist(repo);
    }


    [TestMethod]

    public void MockedDbLoadNextAlbum()
    {
      var repo = new MockedRepository();
      var albumInfo = test.DoLoadFirstArtist(repo);
      Assert.IsNotNull(repo);
      var firstModel = repo.BuildModel(null);
      var nextModel = repo.BuildModel(firstModel, Repository.Operation.NextAlbum);
      Assert.IsNotNull(firstModel);
      Assert.IsNotNull(nextModel);
      Assert.AreNotEqual(firstModel.CurrentAlbumIndex, nextModel.CurrentAlbumIndex);

    }


    [TestMethod]

    public void MockedDbLoadNextArtist()
    {

      var repo = new MockedRepository();
      var albumInfo = test.DoLoadFirstArtist(repo);
      Assert.IsNotNull(repo);
      var firstModel = repo.BuildModel(null);
      var nextModel = repo.BuildModel(firstModel, Repository.Operation.NextArtist);
      Assert.IsNotNull(firstModel);
      Assert.IsNotNull(nextModel);

      Assert.AreEqual(firstModel.CurrentArtistIndex, 1);
      Assert.AreNotEqual(firstModel.CurrentArtistIndex, nextModel.CurrentArtistIndex);
    }

  }
}
