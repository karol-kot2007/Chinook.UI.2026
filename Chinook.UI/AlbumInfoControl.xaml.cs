using Chinook.DAL;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using Chinook.DAL.Models;
using System.Windows.Media;
namespace Chinook.UI
{
  public partial class AlbumInfoControl : UserControl
  {
    public MediaPlayer Player { get; set; }
    public Mode DisplayMode { get; set; }
    public ArtistContext ArtistId { get; set; }
    public Track LocalPath { get; set; }
    public Button CancelBtn { get; set; }
    // public AlbumInfo AlbumName { get; set; }
    public static object Album { get; private set; }

    public AlbumInfoControl()
    {
      InitializeComponent();
      Player = new MediaPlayer();
      Button btn = new Button();


    }
    internal void Bind(AlbumInfoModel model, Mode mode)
    {

      GridAlbum.ItemsSource = model.Tracks;
      DisplayMode = mode;
      ArtistSwapper.Bind("artist", model.ArtistInfo);
      AlbumSwapper.Bind("album", model.AlbumInfo);

    }

    private void dgUsers_AddingNewItem(object sender, AddingNewItemEventArgs e)
    {

    }

    private void Close_Button(object sender, RoutedEventArgs e)
    {
      System.Environment.Exit(0);
    }

    private void ok_Button(object sender, RoutedEventArgs e, ArtistContext ArtistId)
    {

    }

    private void PlayBtn_Click(object sender, RoutedEventArgs e)
    {
      //locql path zakodowac

      Track LocalPath = new Track();
      var obj = ((FrameworkElement)sender).DataContext as Track;
      int k = 0;


      var btn = sender as Button;
      if (btn.Content == "Play")
      {
        btn.Content = "Stop";
        Player.Stop();
      }
      else
      {
        btn.Content = "Play";
        PlaySound();
      }
      k++;

    }


    private void PlaySound()
    {
      Uri uri = new Uri(@"mp3\Michael Jackson - Smooth Criminal (Official Video).mp3", UriKind.Relative);
      Player.Open(uri);
      Player.Play();

    }
    private void GridAlbum_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void AlbumName_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
  }

}