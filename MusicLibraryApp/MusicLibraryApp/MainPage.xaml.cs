using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.UI.Popups;


namespace MusicApp1
{

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {

            this.InitializeComponent();
            mediaPlayer.TransportControls.IsFullWindowButtonVisible = false;
            mediaPlayer.TransportControls.IsZoomButtonVisible = false;
            mediaPlayer.TransportControls.IsStopButtonVisible = true;
            mediaPlayer.TransportControls.IsStopEnabled = true;
            this.ViewModel = new RecordingViewModel();
            LoadPlaylist();

        }
        public RecordingViewModel ViewModel { get; set; }
        public static IList<string> paths;
        public static StorageFile fileplaylist;

        public async void LoadPlaylist()
        {
            try
            {
                var fileplaylist = await ApplicationData.Current.LocalFolder.GetFileAsync("playlist_index.txt");
                var lines = await FileIO.ReadLinesAsync(fileplaylist);

                foreach (var line in lines)
                {
                    StorageFile file = await StorageFile.GetFileFromPathAsync(line);
                    var musicProperties = await file.Properties.GetMusicPropertiesAsync();
                    var artist = musicProperties.Artist;
                    var track = musicProperties.Title;
                    var album = musicProperties.Album;
                    var year = musicProperties.Year;

                    ViewModel.recordings.Add(new Recording
                    {
                        Artist = artist,
                        Track = track,
                        Album = album,
                        Year = year
                    });
                }
            }
            catch (Exception ex)
            {
            }
        }


        private async void BrowseFiles_Click(object sender, RoutedEventArgs e)
        {
            RecordingViewModel rvm = new RecordingViewModel();

            string fileplaylist = "playlist_index.txt";
            StorageFile FilePath = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileplaylist,
                CreationCollisionOption.OpenIfExists);

            var openPicker = new FileOpenPicker();

            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            openPicker.FileTypeFilter.Add(".mp3");
            openPicker.FileTypeFilter.Add(".wav");
            openPicker.FileTypeFilter.Add(".m4a");
            openPicker.FileTypeFilter.Add(".ac3");

            IReadOnlyList<StorageFile> files = await openPicker.PickMultipleFilesAsync();

           

            foreach (StorageFile file in files)
            {
                var musicProperties = await file.Properties.GetMusicPropertiesAsync();
                var artist = musicProperties.Artist;
                var album = musicProperties.Album;
                var track = musicProperties.Title;
                var year = musicProperties.Year;

                ViewModel.recordings.Add(new Recording
                {
                    Artist = artist,
                    Track = track,
                    Album = album,
                    Year = year
                });

                await FileIO.AppendTextAsync(FilePath, file.Path + Environment.NewLine);
            }
       }

        private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {
                if (PlaylistView.SelectedItem != null)
                {
                    fileplaylist = await ApplicationData.Current.LocalFolder.GetFileAsync("playlist_index.txt");
                    paths = await FileIO.ReadLinesAsync(fileplaylist);

                    StorageFile file = await StorageFile.GetFileFromPathAsync(paths[PlaylistView.SelectedIndex]);

                    var stream = await file.OpenAsync(FileAccessMode.Read);

                    mediaPlayer.SetSource(stream, paths[PlaylistView.SelectedIndex]);


                    // ==============================================================================

                    // Play the song by the MediaPlayer that can set the source. If the StorageFile 
                    // which the user clicks on is 'file', we can set the source like this:

                    // StorageFolder loc = Applicaion.Current.LocalFolder;
                   

                    //if (songplaying)
                    //{
                    //    MediaElement.AutoPlay = false;
                    //    Playing = false;
                    //}

                    //else
                    //{
                    //    playing = true;
                    //    MediaElement.AutoPlay = false;
                    //    // give cover image. 
                    //}

                    // MediaPlayerElement.Source = MediaSource.CreateFromStorsge(file);
                    // MediaPlayerElement.Media.Play();

                    // ===================================================================================
                }
            }
        }

        private void ExitApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
