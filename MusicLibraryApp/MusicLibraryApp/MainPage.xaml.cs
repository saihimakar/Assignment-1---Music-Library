using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Pickers; 
using Windows.Storage.Streams;
using Windows.Storage;
using System.Text;
using Windows.UI.ViewManagement;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.UI.Popups;


namespace App4
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
            this.DataContext = DataContext;
            ReadPlaylist();
        }

        public static IList<string> paths;
        public static StorageFile fileplaylist;

        public async void ReadPlaylist()
        {
            try
            {
                fileplaylist = await ApplicationData.Current.LocalFolder.GetFileAsync("playlist_index.txt");
                var lines = await FileIO.ReadLinesAsync(fileplaylist);
                
                foreach (var line in lines)
                {
                    playlist.Items.Add(line);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            string fileplaylist = "playlist_index.txt";
            StorageFile FilePath = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileplaylist,
                CreationCollisionOption.OpenIfExists);
            var openPicker = new FileOpenPicker();

            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".mp3");

            IReadOnlyList<StorageFile> files = await openPicker.PickMultipleFilesAsync();                       

            foreach (StorageFile file in files)
            {
                var musicPoperties = await file.Properties.GetMusicPropertiesAsync();
                var artist = musicPoperties.Artist;
                var album = musicPoperties.Album;
                var title = musicPoperties.Title;                
                playlist.Items.Add(artist + " - " + title + " - " + album);        
                await FileIO.AppendTextAsync(FilePath, file.Path + Environment.NewLine);
            }
                                 
        }

        private async void Playlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (playlist.SelectedItem != null)
            {
                fileplaylist = await ApplicationData.Current.LocalFolder.GetFileAsync("playlist_index.txt");
                paths = await FileIO.ReadLinesAsync(fileplaylist);
                StorageFile file = await StorageFile.GetFileFromPathAsync(paths[playlist.SelectedIndex]);
                var stream = await file.OpenAsync(FileAccessMode.Read);
                mediaPlayer.SetSource(stream, paths[playlist.SelectedIndex]);
            }
        }
    }
    }





    

