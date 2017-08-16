using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class SoundAndSubtitleManager {
        private SoundEffectManager soundEffectManager;
        private SubtitleManager subtitleManager;

        public SoundAndSubtitleManager(SubtitleManager subtitleManager) {
            soundEffectManager = new SoundEffectManager();
            this.subtitleManager = subtitleManager;
        }

        public void Update(GameTime gameTime) {
            subtitleManager.Update(gameTime);
            soundEffectManager.Update();
        }

        public void PlayAndPrint(SoundEffect sound, string text) {
            if (soundEffectManager.CurrentPlayingSoundEffect != sound) {
                soundEffectManager.Play(sound);
                subtitleManager.Print(text, sound.Duration.TotalSeconds);
            }
        }

        public void Stop() {
            soundEffectManager.Stop();
            subtitleManager.Stop();
        }

        public bool Stopped() {
            return soundEffectManager.Stopped() || subtitleManager.Stopped();
        }
    }
}
