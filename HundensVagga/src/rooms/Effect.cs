﻿using System;
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
        private readonly SoundAndSubtitle soundAndSubtitle;
        private readonly IItem item;
        private readonly IItem removeItem;
        private readonly Inventory inventory;
        private readonly string exit;
        private readonly SongManager songManager;
        private readonly Song song;

        public Effect(IList<VarVal> varVals, SoundAndSubtitle soundAndSubtitle, IItem item,
                IItem removeItem, Inventory inventory, Song song, SongManager songManager,
                String exit) {
            this.varVals = varVals;
            this.soundAndSubtitle = soundAndSubtitle;
            this.item = item;
            this.removeItem = removeItem;
            this.inventory = inventory;
            this.song = song;
            this.songManager = songManager;
            this.exit = exit;
        }

        public void Perform(GameManager gameManager) {
            SetVarVals();
            PlaySound(gameManager.SoundAndSubtitleManager);
            AddItemToInventory();
            RemoveItemFromInventory();
            ChangeSong();
            GoToExit(gameManager);
        }

        private void SetVarVals() {
            foreach (VarVal varVal in varVals)
                varVal.Set();
        }

        private void PlaySound(SoundAndSubtitleManager soundAndSubtitleManager) {
            if (soundAndSubtitle != null)
                soundAndSubtitleManager.PlayAndPrint(soundAndSubtitle);
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

        private void GoToExit(GameManager mainGameState) {
            if (exit != null)
                mainGameState.GoToRoom(exit);
        }
    }
}