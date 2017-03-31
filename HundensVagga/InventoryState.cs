using System;
using Microsoft.Xna.Framework.Graphics;

namespace HundensVagga {
    /// <summary>
    /// The in-game state when the player can pick among the items in the inventory.
    /// </summary>
    public class InventoryState : IInGameState {
        private MainGameState mainGameState;

        public InventoryState(MainGameState mainGameState) {
            this.mainGameState = mainGameState;
        }

        public void Update(InputManager inputManager) {
            Inventory inventory = mainGameState.Inventory;

            inventory.SetItemCoords();
            CheckInventoryBag(inputManager, inventory);
            CheckItems(inputManager, inventory);
        }

        private void CheckInventoryBag(InputManager inputManager, Inventory inventory) {
            if (mainGameState.Inventory.IsBagClicked(inputManager)) {
                inventory.GoUp();
                mainGameState.CurrentState = new ExploreState(mainGameState);
            }
        }

        private void CheckItems(InputManager inputManager, Inventory inventory) {
            Item clickedItem = inventory.GetItemAt(inputManager.GetMousePosition());
            if (inputManager.IsLeftButtonPressed() && clickedItem != null) {
                inventory.GoUp();
                mainGameState.CurrentState = new UseItemState(mainGameState, clickedItem);
            }
        }
    }
}
