using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HundensVagga {
    /// <summary>
    /// The in-game state when the player can pick among the items in the inventory.
    /// </summary>
    internal class InventoryState : IGameState {
        private GameManager mainGameState;

        public InventoryState(GameManager mainGameState) {
            this.mainGameState = mainGameState;
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            Inventory inventory = mainGameState.Inventory;

            inventory.SetItemCoords();
            CheckExitIcon(inputManager, inventory);
            CheckOutsideOfInventory(inputManager, inventory);
            CheckItems(inputManager, inventory);
        }

        private void CheckExitIcon(InputManager inputManager, Inventory inventory) {
            if (mainGameState.Inventory.IsExitIconClicked(inputManager)) {
                mainGameState.GameStateManager.CurrentState = new ExitMenuState(mainGameState);
            }
        }

        private void CheckOutsideOfInventory(InputManager inputManager, Inventory inventory) {
            // TODO: Make it possible to simply click outside inventory
            if (mainGameState.Inventory.IsOutsideOfInventoryClicked(inputManager)) {
                inventory.GoUp();
                mainGameState.GameStateManager.PopState();
            }
        }

        private void CheckItems(InputManager inputManager, Inventory inventory) {
            IItem clickedItem = inventory.GetItemAt(inputManager.GetMousePosition());
            if (inputManager.IsLeftButtonPressed() && clickedItem != null) {
                inventory.GoUp();
                HandleItem(clickedItem);
            }
        }

        private void HandleItem(IItem clickedItem) {
            if (clickedItem.HasEffect()) {
                clickedItem.PerformEffect();

                IGameState itemState = clickedItem.GetItemState(mainGameState);
                if (itemState != null)
                    mainGameState.GameStateManager.CurrentState = itemState;
                else
                    mainGameState.GameStateManager.PopState();
            } else
                mainGameState.GameStateManager.CurrentState =
                    new UseItemState(mainGameState, clickedItem);
        }

        public void Draw(SpriteBatch spriteBatch) {
            // nothing to draw
        }
    }
}
