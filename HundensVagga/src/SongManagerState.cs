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
        protected const float MAX_VOLUME = 1f;

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
            MediaPlayer.Volume = MAX_VOLUME;
            this.nextSong = nextSong;
        }

        public override void Update(SongManager songManager) {
            MediaPlayer.Volume -= VOLUME_SPEED;

            if (MediaPlayer.Volume == MIN_VOLUME) {
                songManager.State = new SongManagerFadeIn(nextSong);
                songManager.CurrentSong = nextSong;
            }
        }
    }

    internal class SongManagerFadeIn : ISongManagerState {
        public SongManagerFadeIn(Song song) {
            MediaPlayer.Volume = MIN_VOLUME;
            MediaPlayer.Play(song);
        }

        public override void Update(SongManager songManager) {
            MediaPlayer.Volume += VOLUME_SPEED;

            if (MediaPlayer.Volume == MIN_VOLUME)
                songManager.State = new SongManagerIdle();
        }
    }
}
