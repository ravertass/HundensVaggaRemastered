using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace HundensVagga {
    // TODO: Would perhaps be nicer if a container class held different effects, such as a
    // SoundEffect, a VarValEffect, an InventoryEffect etc.

    /// <summary>
    /// An effect from using/using an item on an interactable.
    /// </summary>
    internal class Effect : IEffect {
        private IList<VarVal> varVals;
        private SoundEffectInstance sound;

        public Effect(IList<VarVal> varVals, SoundEffectInstance sound) {
            this.varVals = varVals;
            this.sound = sound;
        }

        public void Perform() {
            SetVarVals();
            PlaySound();
        }

        private void SetVarVals() {
            foreach (VarVal varVal in varVals)
                varVal.Set();
        }

        private void PlaySound() {
            if (sound != null)
                sound.Play();
        }
    }
}