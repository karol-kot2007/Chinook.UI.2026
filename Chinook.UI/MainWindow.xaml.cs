using System.Windows;
using System.Windows.Controls;
using Chinook.DAL;
namespace Chinook.UI
{
  public enum Mode
  {
    View, Edit
  }
  public partial class MainWindow : Window
  {
    public Button CancelBtn { get; set; }
    public MainWindow()
    {
      InitializeComponent();

    }

    class Artist
    {
      public Artist()
      {

      }
      public string? Name { get; set; }
      public int ArtistId { get; set; }
    }

    private void Button_View_Click(object sender, RoutedEventArgs e)
    {
      var wnd = new AlbumInfoWindow();
      wnd.Show(Mode.View);
    }

    private void Button_Edit_Click(object sender, RoutedEventArgs e)
    {
      var wnd = new AlbumInfoWindow();
      wnd.Show(Mode.Edit);
    }

  }

}
