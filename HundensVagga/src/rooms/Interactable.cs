﻿using Microsoft.Xna.Framework;
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

        private readonly SoundEffectInstance lookSound;
        public SoundEffectInstance LookSound {
            get { return lookSound; }
        }

        private readonly IEffect useEffect;
        private readonly IEffect clickEffect;
        private IDictionary<string, IEffect> itemEffects;

        private IList<VarVal> prereqs;
        private readonly Texture2D texture;

        public Interactable(Rectangle rectangle, SoundEffectInstance lookSound, 
                IEffect useEffect, IEffect clickEffect, IDictionary<string, IEffect> itemEffects,
                IList<VarVal> prereqs, Texture2D texture = null) {
            this.rectangle = rectangle;
            this.lookSound = lookSound;
            this.useEffect = useEffect;
            this.clickEffect = clickEffect;
            this.itemEffects = itemEffects;
            this.prereqs = prereqs;
            this.texture = texture;
        }

        public bool IsActive() {
            foreach (VarVal prereq in prereqs)
                if (!prereq.IsMet())
                    return false;
            return true;
        }

        public bool IsInteractive() {
            return IsClickable() || IsLookable() || IsUsable() || itemEffects.Count > 0;
        }

        public bool IsClickable() {
            return clickEffect != null;
        }

        public bool IsLookable() {
            return lookSound != null;
        }

        public void Look() {
            lookSound.Play();
        }

        public virtual bool IsUsable() {
            return useEffect != null;
        }

        public void ClickAt(GameManager mainGameState) {
            clickEffect.Perform(mainGameState);
        }

        public void Use(GameManager mainGameState) {
            useEffect.Perform(mainGameState);
        }

        public bool IsItemUsable(IItem item) {
            return itemEffects.ContainsKey(item.Name);
        }

        public void UseItem(IItem item, GameManager mainGameState) {
            itemEffects[item.Name].Perform(mainGameState);
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (texture != null)
                spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}