using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;

namespace HundensVagga {
    internal class SoundEffects {
        private IDictionary<string, SoundEffect> loadedSoundEffects;
        public SoundEffects() {
            loadedSoundEffects = new Dictionary<string, SoundEffect>();
        }

        public SoundEffect GetSoundEffect(ContentManager content, string soundEffectName) {
            if (!loadedSoundEffects.ContainsKey(soundEffectName))
                loadedSoundEffects.Add(
                    soundEffectName,
                    LoadSoundEffect(content, soundEffectName));

            return loadedSoundEffects[soundEffectName];
        }

        private SoundEffect LoadSoundEffect(ContentManager content, string soundEffectName) {
            SoundEffect soundEffect;

            try {
                soundEffect = content.Load<SoundEffect>(Main.SOUND_EFFECTS_DIR
                        + Path.DirectorySeparatorChar + soundEffectName);
            } catch (ContentLoadException) {
                soundEffect = null;
            }

            return soundEffect;
        }
    }
}
