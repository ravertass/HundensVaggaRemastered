using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class SoundAndSubtitle {
        private SoundEffect soundEffect;
        public SoundEffect SoundEffect {
            get { return soundEffect; }
        }

        private String subtitle;
        public String Subtitle {
            get { return subtitle; }
        }

        private static readonly double DEFAULT_DURATION_PER_WORD = 0.35;

        public double Duration {
            get {
                return (HasSoundEffect())
                       ? soundEffect.Duration.TotalSeconds
                       : subtitle.Split(' ').Length * DEFAULT_DURATION_PER_WORD;
            }
        }

        public SoundAndSubtitle(SoundEffect soundEffect, String subtitle) {
            this.soundEffect = soundEffect;
            this.subtitle = subtitle;
        }

        public bool HasSoundEffect() {
            return soundEffect != null;
        }

        public bool HasSubtitle() {
            return subtitle != null;
        }
    }
}
