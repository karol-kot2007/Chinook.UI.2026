using Chinook.DAL;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using Chinook.DAL.Models;
using System.Windows.Media;
using Chinook.Models;
namespace Chinook.UI
{
  public partial class AlbumInfoControl : UserControl
  {
    public MediaPlayer Player { get; set; }
    public Mode DisplayMode { get; set; }
    public ArtistContext ArtistId { get; set; }
    public Track LocalPath { get; set; }
    public Button CancelBtn { get; set; }
    public static object Album { get; private set; }
    private Button LastPlayedButton = new();
    public AlbumInfoControl()
    {
      InitializeComponent();
      Player = new MediaPlayer();
    }
    internal void Bind(ArtistModel model, Mode mode)
    {
      GridAlbum.ItemsSource = model.MusicModel.AlbumInfo.Tracks;
      DisplayMode = mode;
      ArtistSwapper.Bind(model.GetArtistInfo());
      AlbumSwapper.Bind(model.GetAlbumInfo()); 
    }

    private void dgUsers_AddingNewItem(object sender, AddingNewItemEventArgs e)
    {

    }

    private void Close_Button(object sender, RoutedEventArgs e)
    {
      System.Environment.Exit(0);
    }
    private void Play_Stop_Btn_Click(object sender, RoutedEventArgs e)
    {
      Track LocalPath = new Track();
      var obj = ((FrameworkElement)sender).DataContext as Track;
      var btn = sender as Button;
      if (btn.Content.ToString() == "Play")
      {
        PlaySound();
        btn.Content = "Stop";
        LastPlayedButton.Content = "Play";
      }
      else
      {
        Player.Stop();
        btn.Content = "Play";
      }
      LastPlayedButton = btn;
    }


    private void PlaySound()
    {
      Uri uri = new Uri(@"mp3\Michael Jackson - Smooth Criminal (Official Video).mp3", UriKind.Relative);
      Player.Open(uri);
      Player.Play();

    }
    private void AlbumName_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void AlbumSwapper_Loaded(object sender, RoutedEventArgs e)
    {

        }
    }

}