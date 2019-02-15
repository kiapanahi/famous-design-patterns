using System;

namespace DoFactory.HeadFirst.Combined.MVC
{
    /// <summary>
    /// BeatEventArgs carry info about BPM (Beats Per Minute).
    /// </summary>
    public class BeatEventArgs : EventArgs
    {
        private int _beatsPerMinute;

        public BeatEventArgs(int beatsPerMinute) 
        {
            _beatsPerMinute = beatsPerMinute;
        }

        public int BeatsPerMinute
        {
            get{ return _beatsPerMinute; }
        }
    }
}
