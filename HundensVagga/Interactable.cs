using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HundensVagga {
    /// <summary>
    /// Models everything in a room that is not the background image or an exit.
    /// Typically interactable (e.g. you can look at the interactable or use it, 
    /// or use an item on it).
    /// </summary>
    internal class Interactable {
        private readonly Rectangle rectangle;
        public Rectangle Rectangle {
            get { return rectangle; }
        }
        private readonly Texture2D texture;
        private readonly SoundEffectInstance lookSound;
        private IList<Prereq> prereqs;

        public Interactable(Rectangle rectangle, SoundEffectInstance lookSound, 
                IList<Prereq> prereqs, Texture2D texture = null) {
            this.rectangle = rectangle;
            this.lookSound = lookSound;
            this.prereqs = prereqs;
            this.texture = texture;
        }

        public bool IsActive() {
            foreach (Prereq prereq in prereqs)
                if (!prereq.IsMet())
                    return false;
            return true;
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

        public void Draw(SpriteBatch spriteBatch) {
            if (texture != null)
                spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}