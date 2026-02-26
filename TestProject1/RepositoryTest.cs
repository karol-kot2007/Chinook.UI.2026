using Chinook.DAL;
using Chinook.Models;

namespace TestProject1
{
  [TestClass]
  public sealed class RepositoryTest
  {
    [TestMethod]
    public void TestModelBuild()
    {
      SQLitePCL.Batteries.Init();

      var repo = new Repository();
      Assert.IsNotNull(repo);

      var build = repo.BuildModel();
      Assert.IsNotNull(build);
    }

    [TestMethod]
    public void TestModelBuildNull()
    {
      SQLitePCL.Batteries.Init();

      var repo = new Repository();
      Assert.IsNotNull(repo);
      var build = repo.BuildModel();
      Assert.IsNotNull(build);
    }
  }
}
