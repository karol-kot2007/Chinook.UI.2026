using Chinook.Models;

namespace Chinook.Tests
{
  [TestClass]
  public sealed class Test1
  {
    [TestMethod]
    public void RepositoryTest()
    {
      var repo = new Repository();
      var model = repo.BuildModel();
      Assert.IsNull(model.Tracks);
      Assert.IsNotNull(model.Tracks);

      if (model.Tracks == null)
        Assert.Fail("Tracks jest null");

      Assert.IsNotNull(model.ArtistInfo);
    }
  }

}
