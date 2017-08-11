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

        public float MaxVolume;
        private const float MAX_VOLUME = 1f;

        public SongManager() {
            MediaPlayer.IsRepeating = true;
            SetVolume(MAX_VOLUME);
            State = new SongManagerIdle();
            CurrentSong = null;
        }

        public void Update() {
            State.Update(this);
        }

        public void NewRoom(Room room) {
            if (room.Song != CurrentSong)
                if (CurrentSong == null)
                    FadeIntoSong(room.Song);
                else
                    FadeOutThenIntoSong(room.Song);
            SetVolume(room.Volume);
        }

        private void SetVolume(float volume) {
            MediaPlayer.Volume = volume;
            MaxVolume = volume;
        }

        public void FadeOutThenIntoSong(Song song) {
            State = new SongManagerFadeOut(song);
        }

        public void FadeOut() {
            State = new SongManagerFadeOut(null);
            CurrentSong = null;
        }

        public void FadeIntoSong(Song song) {
            CurrentSong = song;
            State = new SongManagerFadeIn(song);
        }

        public void Stop() {
            MediaPlayer.Stop();
            State = new SongManagerIdle();
            CurrentSong = null;
        }
    }
}
