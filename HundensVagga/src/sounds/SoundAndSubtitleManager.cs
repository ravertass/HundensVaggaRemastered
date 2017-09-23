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

        private bool currentHasSoundEffect;

        public SoundAndSubtitleManager(SubtitleManager subtitleManager) {
            soundEffectManager = new SoundEffectManager();
            this.subtitleManager = subtitleManager;
        }

        public void Update(GameTime gameTime) {
            subtitleManager.Update(gameTime);
            soundEffectManager.Update();
        }

        public void PlayAndPrint(SoundAndSubtitle soundAndSubtitle) {
            currentHasSoundEffect = soundAndSubtitle.HasSoundEffect();

            if (soundAndSubtitle.HasSoundEffect())
                soundEffectManager.Play(soundAndSubtitle.SoundEffect);
            if (soundAndSubtitle.HasSubtitle())
                subtitleManager.Print(soundAndSubtitle.Subtitle, soundAndSubtitle.Duration);
        }

        public void Stop() {
            soundEffectManager.Stop();
            subtitleManager.Stop();
        }

        public bool Stopped() {
            return (currentHasSoundEffect)
                   ? soundEffectManager.Stopped()
                   : subtitleManager.Stopped();
        }
    }
}
