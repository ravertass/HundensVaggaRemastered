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

        public void PlayAndPrint(SoundAndSubtitle soundAndSubtitle) {
            // Add commented out code if you don't want it to be possible to replay the
            // currently playing sound effect (e.g. looking at the same object twice in a row).

            //if (soundEffectManager.CurrentPlayingSoundEffect != soundAndSubtitle.SoundEffect) {
            soundEffectManager.Play(soundAndSubtitle.SoundEffect);
            if (soundAndSubtitle.HasSubtitle())
                subtitleManager.Print(soundAndSubtitle.Subtitle, soundAndSubtitle.Duration);
            //}
        }

        public void Stop() {
            soundEffectManager.Stop();
            subtitleManager.Stop();
        }

        public bool Stopped() {
            return soundEffectManager.Stopped();
        }
    }
}
