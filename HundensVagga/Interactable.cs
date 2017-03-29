using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace HundensVagga {
    public class Interactable {
        private readonly Rectangle rectangle;
        public Rectangle Rectangle {
            get { return rectangle; }
        }
        private readonly Texture2D image;
        private readonly SoundEffectInstance lookSound;

        public Interactable(Rectangle rectangle, SoundEffectInstance lookSound, Texture2D image = null) {
            this.rectangle = rectangle;
            this.lookSound = lookSound;
            this.image = image;
        }

        public bool IsLookable() {
            return lookSound != null;
        }

        public void PlayLookSound() {
            lookSound.Play();
        }

        public bool IsUsable() {
            return false;
        }
    }
}