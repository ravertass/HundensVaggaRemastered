using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace HundensVagga {
    internal class SoundItem : Item, IItem {
        private SoundEffectInstance sound;

        public SoundItem(string name, Texture2D icon, SoundEffectInstance sound) 
            : base(name, icon) {
            this.sound = sound;
        }

        public override void PerformEffect() {
            if (!sound.IsDisposed)
                sound.Play();
        }

        public override bool HasEffect() {
            return true;
        }
    }
}
