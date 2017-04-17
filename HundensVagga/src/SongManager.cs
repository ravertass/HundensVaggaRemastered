using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class SongManager {
        public ISongManagerState State { get; set; }
        public Song CurrentSong { get; set; }

        public SongManager() {
            MediaPlayer.IsRepeating = true;
            State = new SongManagerIdle();
            CurrentSong = null;
        }

        public void Update() {
            State.Update(this);
        }

        public void NewRoom(Room room) {
            if (room.Song != CurrentSong)
                FadeOutThenIntoSong(room.Song);
            MediaPlayer.Volume = room.Volume;
        }

        private void FadeOutThenIntoSong(Song song) {
            State = new SongManagerFadeOut(song);
        }

        public void FadeIntoSong(Song song) {
            CurrentSong = song;
            State = new SongManagerFadeIn(song);
        }
    }
}
