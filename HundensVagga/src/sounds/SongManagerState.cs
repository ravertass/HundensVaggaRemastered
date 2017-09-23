using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    /// <summary>
    /// Used to manage when songs fade out and in.
    /// </summary>
    internal abstract class ISongManagerState {
        protected const float VOLUME_SPEED = 0.025f;
        protected const float MIN_VOLUME = 0f;

        abstract public void Update(SongManager songManager);
    }

    internal class SongManagerIdle : ISongManagerState {
        public override void Update(SongManager songManager) {
            // do nothing
        }
    }

    internal class SongManagerFadeOut : ISongManagerState {
        private Song nextSong; 

        public SongManagerFadeOut(Song nextSong) {
            this.nextSong = nextSong;
        }

        public override void Update(SongManager songManager) {
            MediaPlayer.Volume -= VOLUME_SPEED;

            if (MediaPlayer.Volume <= MIN_VOLUME) {
                MediaPlayer.Volume = MIN_VOLUME;
                if (nextSong != null)
                    NextSong(songManager);
                else
                    Stop(songManager);
            }
        }

        private void NextSong(SongManager songManager) {
            songManager.State = new SongManagerFadeIn(nextSong);
            songManager.CurrentSong = nextSong;
        }

        private static void Stop(SongManager songManager) {
            MediaPlayer.Stop();
            songManager.State = new SongManagerIdle();
        }
    }

    internal class SongManagerFadeIn : ISongManagerState {
        public SongManagerFadeIn(Song song) {
            MediaPlayer.Volume = MIN_VOLUME;
            if (song != null)
                MediaPlayer.Play(song);
        }

        public override void Update(SongManager songManager) {
            MediaPlayer.Volume += VOLUME_SPEED;

            if (MediaPlayer.Volume >= songManager.MaxVolume)
                songManager.State = new SongManagerIdle();
        }
    }
}
