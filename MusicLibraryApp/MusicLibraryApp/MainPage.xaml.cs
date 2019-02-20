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
//using TagLib;

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
            ReadPlaylist();
        }
        public string[] paths;

        private async void ReadPlaylist()
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync("playlist_index.txt");
                var lines = await FileIO.ReadLinesAsync(file);
                foreach (var line in lines)
                {
                    playlist.Items.Add(line);
                }
            }
            catch (Exception ex)
            {

            }
            //foreach (var line in lines)
            //{
            //    playlist.Items.Add(line);
            //}

        }


        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            string filename = "playlist_index.txt";
            StorageFile FilePath = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename,
                CreationCollisionOption.OpenIfExists);
            var openPicker = new FileOpenPicker();

            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".mp3");

            IReadOnlyList<StorageFile> files = await openPicker.PickMultipleFilesAsync();                       

            foreach (StorageFile file in files)
            {
                playlist.Items.Add(file.Path);
                await FileIO.AppendTextAsync(FilePath, file.Path + Environment.NewLine);
            }





            //FileOpenPicker FileBrowser = new FileOpenPicker();
            //FileBrowser.SuggestedStartLocation = PickerLocationId.Desktop;
            //FileBrowser.ViewMode = PickerViewMode.List;
            //FileBrowser.FileTypeFilter.Add(".mp3");
            //StorageFile File = await FileBrowser.PickSingleFileAsync();
            //if (File != null)
            //{
            //    IRandomAccessStream stream = await File.OpenAsync(FileAccessMode.Read);
            //    mediaPlayer.SetSource(stream, File.ContentType);
            //    mediaPlayer.Play();
            //}


        }
    }





        //FileOpenPicker FileBrowser = new FileOpenPicker();
        //FileBrowser.SuggestedStartLocation = PickerLocationId.Desktop;
        //FileBrowser.ViewMode = PickerViewMode.List;
        //FileBrowser.FileTypeFilter.Add(".mp3");
        //StorageFile File = await FileBrowser.PickSingleFileAsync();
        //if (File != null)
        //{
        //    IRandomAccessStream stream = await File.OpenAsync(FileAccessMode.Read);
        //    media.SetSource(stream, File.ContentType);
        //    media.Play();
        //}
    }





    

