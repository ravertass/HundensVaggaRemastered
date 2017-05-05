﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    /// <summary>
    /// The in-game state when the player can use an item picked from the inventory.
    /// </summary>
    internal class UseItemState : IInGameState {
        private MainGameState mainGameState;
        private IItem currentItem;

        public UseItemState(MainGameState mainGameState, IItem currentItem) {
            this.mainGameState = mainGameState;
            this.currentItem = currentItem;
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            mainGameState.CursorManager.SetToItem();
            CheckInteractables(inputManager);
        }

        private void CheckInteractables(InputManager inputManager) {
            Interactable interactable = 
                mainGameState.CurrentRoom.GetInteractableAt(inputManager.GetMousePosition());
            if (inputManager.IsLeftButtonPressed()) {
                if (interactable != null && interactable.IsItemUsable(currentItem)) {
                    interactable.UseItem(currentItem, mainGameState);
                } else {
                    Console.WriteLine("nej: " + currentItem.Name);
                    // TODO: play fail sound
                }
                mainGameState.InGameStateManager.PopState();
            }
        }
    }
}
