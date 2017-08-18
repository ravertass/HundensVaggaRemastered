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

        public double Duration {
            get { return soundEffect.Duration.TotalSeconds; }
        }

        public SoundAndSubtitle(SoundEffect soundEffect, String subtitle) {
            this.soundEffect = soundEffect;
            this.subtitle = subtitle;
        }

        public bool HasSubtitle() {
            return subtitle != null;
        }
    }
}
