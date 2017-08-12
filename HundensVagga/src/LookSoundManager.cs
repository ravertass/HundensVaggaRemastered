using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class LookSoundManager {
        private SoundEffectInstance currentPlayingLookSound;
        public SoundEffectInstance CurrentPlayingLookSound {
            get { return currentPlayingLookSound; }
        }

        public void Update() {
            if (currentPlayingLookSound != null
                && currentPlayingLookSound.State == SoundState.Stopped)
                currentPlayingLookSound = null;
        }

        public void Play(SoundEffectInstance sound) {
            if (currentPlayingLookSound != null)
                currentPlayingLookSound.Stop();

            currentPlayingLookSound = sound;
            currentPlayingLookSound.Play();
        }

        public bool StoppedPlaying() {
            return currentPlayingLookSound == null;
        }
    }
}
