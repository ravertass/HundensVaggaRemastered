using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class SoundAndSubtitleManager {
        private LookSoundManager lookSoundManager;
        private SubtitleManager subtitleManager;
        private bool playing;

        public SoundAndSubtitleManager(SubtitleManager subtitleManager) {
            playing = false;
            lookSoundManager = new LookSoundManager();
            this.subtitleManager = subtitleManager;
        }

        public void Update(GameTime gameTime) {
            subtitleManager.Update(gameTime);
            lookSoundManager.Update();
            if (playing && subtitleManager.ShouldPrint() && lookSoundManager.StoppedPlaying()) {
                playing = false;
                subtitleManager.SetTimer(1);
            }
        }

        public void PlayAndPrint(SoundEffectInstance sound, string text) {
            if (lookSoundManager.CurrentPlayingLookSound != sound) {
                playing = true;
                lookSoundManager.Play(sound);
                subtitleManager.Print(text);
            }
        }
    }
}
