using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HundensVagga {
    /// <summary>
    /// The in-game state when the player can pick among the items in the inventory.
    /// </summary>
    internal class InventoryState : IInGameState {
        private MainGameState mainGameState;

        public InventoryState(MainGameState mainGameState) {
            this.mainGameState = mainGameState;
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            Inventory inventory = mainGameState.Inventory;

            inventory.SetItemCoords();
            CheckInventoryBag(inputManager, inventory);
            CheckItems(inputManager, inventory);
        }

        private void CheckInventoryBag(InputManager inputManager, Inventory inventory) {
            if (mainGameState.Inventory.IsBagClicked(inputManager)) {
                inventory.GoUp();
                mainGameState.InGameStateManager.PopState();
            }
        }

        private void CheckItems(InputManager inputManager, Inventory inventory) {
            IItem clickedItem = inventory.GetItemAt(inputManager.GetMousePosition());
            if (inputManager.IsLeftButtonPressed() && clickedItem != null) {
                inventory.GoUp();

                if (clickedItem.HasEffect()) {
                    clickedItem.PerformEffect();
                    mainGameState.InGameStateManager.PopState();
                } else
                    mainGameState.InGameStateManager.CurrentState =
                        new UseItemState(mainGameState, clickedItem);
            }
        }
    }
}
