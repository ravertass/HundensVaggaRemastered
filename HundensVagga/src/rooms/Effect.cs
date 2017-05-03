using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace HundensVagga {
    // TODO: Would perhaps be nicer if a container class held different effects, such as a
    // SoundEffect, a VarValEffect, an InventoryEffect etc.

    /// <summary>
    /// An effect from using/using an item on an interactable.
    /// </summary>
    internal class Effect : IEffect {
        private readonly IList<VarVal> varVals;
        private readonly SoundEffectInstance sound;
        private readonly Item item;
        private readonly Item removeItem;
        private readonly Inventory inventory;
        private readonly string exit;
        private readonly SongManager songManager;
        private readonly Song song;

        public Effect(IList<VarVal> varVals, SoundEffectInstance sound, Item item, Item removeItem,
                Inventory inventory, Song song, SongManager songManager, String exit) {
            this.varVals = varVals;
            this.sound = sound;
            this.item = item;
            this.removeItem = removeItem;
            this.inventory = inventory;
            this.song = song;
            this.songManager = songManager;
            this.exit = exit;
        }

        public void Perform(MainGameState mainGameState) {
            SetVarVals();
            PlaySound();
            AddItemToInventory();
            RemoveItemFromInventory();
            ChangeSong();
            GoToExit(mainGameState);
        }

        private void SetVarVals() {
            foreach (VarVal varVal in varVals)
                varVal.Set();
        }

        private void PlaySound() {
            if (sound != null)
                sound.Play();
        }

        private void AddItemToInventory() {
            if (item != null)
                inventory.AddItem(item);
        }

        private void RemoveItemFromInventory() {
            if (removeItem != null)
                inventory.RemoveItem(removeItem);
        }

        private void ChangeSong() {
            if (song != null)
                songManager.FadeIntoSong(song);
        }

        private void GoToExit(MainGameState mainGameState) {
            if (exit != null)
                mainGameState.GoToRoom(exit);
        }
    }
}