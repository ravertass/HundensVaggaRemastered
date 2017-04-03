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
        private Item currentItem;

        public UseItemState(MainGameState mainGameState, Item currentItem) {
            this.mainGameState = mainGameState;
            this.currentItem = currentItem;
        }

        public void Update(InputManager inputManager) {
            mainGameState.CursorManager.SetToItem();
            CheckInteractables(inputManager);
        }

        private void CheckInteractables(InputManager inputManager) {
            Interactable interactable = 
                mainGameState.CurrentRoom.GetInteractableAt(inputManager.GetMousePosition());
            if (inputManager.IsLeftButtonPressed()) {
                if (interactable != null && interactable.IsItemUsable(currentItem)) {
                    interactable.UseItem(currentItem);
                } else {
                    Console.WriteLine("nej");
                    // TODO: play fail sound
                }
                mainGameState.CurrentState = new ExploreState(mainGameState);
            }
        }
    }
}
