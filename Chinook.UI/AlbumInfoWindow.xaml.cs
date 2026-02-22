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
    public Track trackId { get; set; }
    public AlbumTrack trackName { get; set; }
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

    //public HandleError()
    //{

    //}
    public void AlbumInfoControl_onPrev(object? sender, EventArgs e)
    {
      Debug.WriteLine("btn clicked Prev !!!!!!!!!!" + ArtistModel.MaxAlbumIndex + " " + ArtistModel.CurrentAlbumIndex);
      ArtistModel.CurrentAlbumIndex--;
      if (ArtistModel.CurrentAlbumIndex < 0)
      {
        ArtistModel.CurrentAlbumIndex = ArtistModel.MaxAlbumIndex-1;
      }
      SetModel();
    }

    public void ArtistInfoControl_onPrev(object? sender, EventArgs e)
    {
      ArtistModel.CurrentArtistIndex--;
      ArtistModel.CurrentAlbumIndex = 0;
      if (ArtistModel.CurrentArtistIndex < 0)
      {
       ArtistModel.CurrentArtistIndex = ArtistModel.MaxArtistIndex - 1;
      }
      SetModel();
    }
    public void ArtistInfoControl_OnNext(object? sender, EventArgs e)
    {
       ArtistModel.CurrentArtistIndex++;
      ArtistModel.CurrentAlbumIndex = 0;
      if (ArtistModel.CurrentArtistIndex > ArtistModel.MaxArtistIndex)
      {
        ArtistModel.CurrentArtistIndex--;
      }
      
      SetModel();
    }
    public void AlbumInfoControl_OnNext(object? sender, EventArgs e)
    {
      ArtistModel.CurrentAlbumIndex++;
      if (ArtistModel.CurrentAlbumIndex == ArtistModel.MaxAlbumIndex)
      {
        ArtistModel.CurrentAlbumIndex = 0;
   
      }
  
      SetModel();
    }

    protected override void OnInitialized(EventArgs e)
    {
      base.OnInitialized(e);
    }


    private void SetModel(AlbumInfoModel model)
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
      AlbumInfoModel = model;
      
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
    private AlbumInfoModel BuildModelFromView(ArtistContext context)
    {
      ArtistModel.MaxArtistIndex = context.AlbumTracks.Count();
      var model = new AlbumInfoModel();


      var artist = context.AlbumTracks.ElementAt(ArtistModel.CurrentArtistIndex);
      model.ArtistInfo.Name = artist.artistName;//
      model.ArtistInfo.Id = artist.artistId;
      model.ArtistInfo.Max = ArtistModel.MaxArtistIndex;
      model.ArtistInfo.Current = ArtistModel.CurrentArtistIndex;
      model.ArtistInfo.Max = context.Artists.Count();
    
      var tracks = context.Tracks.First();
      model.TrackInfo.Id = tracks.TrackId;
      model.TrackInfo.Name = tracks.Name;
      var albums = context.AlbumTracks.Where(a => a.artistId == model.ArtistInfo.Id).ToList();
      //ArtistModel.MaxAlbumIndex = albums.Count;
      ArtistModel.MaxAlbumIndex = albums.Count;
      var album = albums[ArtistModel.CurrentAlbumIndex];
      model.AlbumInfo.Id = album.albumId;
      model.AlbumInfo.Name = album.albumName;
      model.AlbumInfo.Max = ArtistModel.MaxAlbumIndex;
      model.AlbumInfo.Current = ArtistModel.CurrentAlbumIndex;
      model.Tracks = context.Tracks.Where(i => i.AlbumId == album.albumId).ToList();
      return model;
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
      ArtistContext context = new ArtistContext();
    //  this.ArtistModel = new ArtistModel();
     // var model = BuildModelFromView(context);
         
     var model = ArtistModel.BuildModel(context);
      AlbumInfoControl.ArtistName.Text = model.ArtistInfo.Name;
      AlbumInfoControl.AlbumName.Text = model.AlbumInfo.Name;
      if (model.ArtistInfo.Name == null )
        return;
      SetModel(model);
    }

    //dodac  private void
  }
}