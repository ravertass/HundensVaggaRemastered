using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class SoundEffectManager {
        private SoundEffect currentPlayingSoundEffect;
        public SoundEffect CurrentPlayingSoundEffect {
            get { return currentPlayingSoundEffect; }
        }

        private SoundEffectInstance currentPlayingSoundEffectInstance;

        public void Update() {
            if (currentPlayingSoundEffectInstance != null
                && currentPlayingSoundEffectInstance.State == SoundState.Stopped)
                currentPlayingSoundEffectInstance = null;
        }

        public void Play(SoundEffect sound) {
            Stop();

            currentPlayingSoundEffect = sound;
            currentPlayingSoundEffectInstance = currentPlayingSoundEffect.CreateInstance();
            currentPlayingSoundEffectInstance.Play();
        }

        public void Stop() {
            if (currentPlayingSoundEffectInstance != null)
                currentPlayingSoundEffectInstance.Stop();
        }

        public bool Stopped() {
            return currentPlayingSoundEffectInstance == null;
        }
    }
}
