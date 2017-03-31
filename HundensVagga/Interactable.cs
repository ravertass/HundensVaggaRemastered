using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace HundensVagga {
    /// <summary>
    /// Models everything in a room that is not the background image or an exit.
    /// Typically interactable (e.g. you can look at the interactable or use it, 
    /// or use an item on it).
    /// </summary>
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

        public bool IsItemUsable(Item item) {
            return false; // TODO
        }
    }
}