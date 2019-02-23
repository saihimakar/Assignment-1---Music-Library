using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp1
{
    public class Recording
    {
            
        public string Artist { get; set; }
        public string Track { get; set; }
        public string Album { get; set; }
        public uint Year { get; set; }
        //public Recording()
        //{
        //    this.ArtistName = "Wolfgang Amadeus Mozart";
        //    this.CompositionName = "Andante in C for Piano";
        //    this.ReleaseDateTime = new DateTime(1761, 1, 1);
        //}
        //public string OneLineSummary
        //{
        //    get
        //    {
        //        return $"{this.CompositionName} by {this.ArtistName}, released: "
        //            + this.ReleaseDateTime.ToString("d");
        //    }
        //}
    }

    public class RecordingViewModel
    {
        //  private Recording defaultRecording = new Recording();
        //  public Recording DefaultRecording { get { return this.defaultRecording; } }
        public ObservableCollection<Recording> recordings = new ObservableCollection<Recording>();

        public ObservableCollection<Recording> Recordings { get { return this.recordings; } }

        //public RecordingViewModel()
        //{
        //    this.recordings.Add(new Recording()
        //    {
        //        Artist = "Johann Sebastian Bach",
        //        Track = "Mass in B minor",
        //        Album = "Unknown Album"
        //    });
        //}
    }
}
