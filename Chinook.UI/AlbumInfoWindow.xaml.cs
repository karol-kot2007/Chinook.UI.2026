using Chinook.DAL;
using Chinook.DAL.Models;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Chinook.UI
{//prev i warunkibrzegowe
  public partial class AlbumInfoWindow : Window
  {
    public ArtistModel ArtistModel { get; private set; }
    public AlbumInfoModel AlbumInfoModel { get; set; }
    public ArtistInfo ArtistInfo { get; set; }
    public AlbumInfo AlbumInfo { get; set; }
    public Repository Repository { get; private set; } = new();
    public Mode DisplayMode { get; set; }

    public AlbumInfoWindow()
    {

      InitializeComponent();
      this.ArtistModel = new ArtistModel();
      AlbumInfoControl.AlbumSwapper.OnNext += AlbumInfoControl_OnNext;
      AlbumInfoControl.AlbumSwapper.OnPrev += AlbumInfoControl_onPrev;
      AlbumInfoControl.ArtistSwapper.OnNext += ArtistInfoControl_OnNext;
      AlbumInfoControl.ArtistSwapper.OnPrev += ArtistInfoControl_onPrev;

    }

    public void ArtistInfoControl_onPrev(object? sender, EventArgs e)
    {
      Repository.BuildModel(ArtistModel, operation: Repository.Operation.PrevArtist);
      SetModel();
    }
    public void ArtistInfoControl_OnNext(object? sender, EventArgs e)
    {
      Repository.BuildModel(ArtistModel, operation: Repository.Operation.NextArtist);
      SetModel();
    }

    public void AlbumInfoControl_onPrev(object? sender, EventArgs e)
    {
      Repository.BuildModel(ArtistModel, operation: Repository.Operation.PrevAlbum);
      SetModel();
    }

    public void AlbumInfoControl_OnNext(object? sender, EventArgs e)
    {
      Repository.BuildModel(ArtistModel, operation: Repository.Operation.NextAlbum);
      SetModel();
    }

    protected override void OnInitialized(EventArgs e)
    {
      base.OnInitialized(e);
    }


    private void SetModel(ArtistModel model)
    {
      DataContext = model;
      AlbumInfoControl.Bind(model, DisplayMode);
      if (DisplayMode == Mode.View)
      {
        CancelBtn.Visibility = Visibility.Collapsed;
        OkBtn.Visibility = Visibility.Collapsed;
      }
      if (DisplayMode == Mode.Edit)
      {
        CloseBtn.Visibility = Visibility.Collapsed;
      }
      AlbumInfoModel = model.AlbumInfo;

    }
    protected void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder
          .Entity<AlbumTracks>(
              eb =>
              {
                eb.HasNoKey();
                eb.ToView("AlbumTracks");
                eb.Property(v => v.artistName).HasColumnName("artistName");
              });
    }


    private void CloseBtn_Click(object sender, RoutedEventArgs e)
    {

      Close();
    }
    private void OkBtn_Click(object sender, RoutedEventArgs e)
    {
      var artistContext = new ArtistContext();
      var albumContext = new AlbumInfoWindow();
      var artist = artistContext.Artists.Where(i => i.ArtistId == AlbumInfoModel.ArtistInfo.Id).Single();
      var album = artistContext.Albums.Where(a => a.AlbumId == AlbumInfoModel.AlbumInfo.Id).Single();
      artist.Name = AlbumInfoControl.ArtistName.Text;
      album.Title = AlbumInfoControl.AlbumName.Text;
      artistContext.SaveChanges();
      Close();
    }
    private void CancelBtn_Click(object sender, RoutedEventArgs e)
    {
      Close();

    }
    private void PlayBtn_Click(object sender, RoutedEventArgs e)
    {

    }

    internal void Show(Mode mode)
    {
      DisplayMode = mode;

      SetModel();

      ShowDialog();
    }

    private void SetModel()
    {

      var model = Repository.BuildModel(ArtistModel);
      if (model.AlbumInfo.Tracks == null)
      {
        AlbumInfoControl.AlbumName.Text = "no match";
        AlbumInfoControl.GridAlbum.Visibility = Visibility.Hidden;
      }
      else
      {
        AlbumInfoControl.AlbumName.Text = model.AlbumInfo.AlbumInfo.Name;
        AlbumInfoControl.GridAlbum.Visibility = Visibility.Visible;
       
      }
      AlbumInfoControl.ArtistName.Text = model.AlbumInfo.ArtistInfo.Name;
      SetModel(model);
    }

    private void AlbumInfoControl_Loaded(object sender, RoutedEventArgs e)
    {

    }
  }
}