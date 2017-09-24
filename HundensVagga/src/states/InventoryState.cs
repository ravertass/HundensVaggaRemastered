using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HundensVagga {
    /// <summary>
    /// The in-game state when the player can pick among the items in the inventory.
    /// </summary>
    internal class InventoryState : IGameState {
        private GameManager gameManager;

        public InventoryState(GameManager gameManager) {
            this.gameManager = gameManager;
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            gameManager.CursorManager.SetToDefault();

            Inventory inventory = gameManager.Inventory;

            inventory.SetItemCoords();
            CheckExitIcon(inputManager, inventory);
            CheckOutsideOfInventory(inputManager, inventory);
            CheckItems(inputManager, inventory);
        }

        private void CheckExitIcon(InputManager inputManager, Inventory inventory) {
            if (gameManager.Inventory.IsCursorOnAnIcon(inputManager))
                gameManager.CursorManager.SetToClick();
            if (gameManager.Inventory.IsExitIconClicked(inputManager))
                gameManager.GameStateManager.CurrentState = new ExitMenuState(gameManager);
            gameManager.Inventory.HandleSubtitlesIconClicks(inputManager);
        }

        private void CheckOutsideOfInventory(InputManager inputManager, Inventory inventory) {
            if (gameManager.Inventory.IsCursorOnBag(inputManager))
                gameManager.CursorManager.SetToClick();
            if (gameManager.Inventory.IsOutsideOfInventoryClicked(inputManager)) {
                inventory.GoUp();
                gameManager.GameStateManager.PopState();
            }
        }

        private void CheckItems(InputManager inputManager, Inventory inventory) {
            IItem clickedItem = inventory.GetItemAt(inputManager.GetMousePosition());
            if (clickedItem != null) {
                gameManager.CursorManager.SetToClick();

                if (inputManager.IsLeftButtonPressed()) {
                    inventory.GoUp();
                    HandleItem(clickedItem);
                }
            }
        }

        private void HandleItem(IItem clickedItem) {
            if (clickedItem.HasEffect()) {
                clickedItem.PerformEffect();

                IGameState itemState = clickedItem.GetItemState(gameManager);
                if (itemState != null)
                    gameManager.GameStateManager.CurrentState = itemState;
                else
                    gameManager.GameStateManager.PopState();
            } else
                gameManager.GameStateManager.CurrentState =
                    new UseItemState(gameManager, clickedItem);
        }

        public void Draw(SpriteBatch spriteBatch) {
            // nothing to draw
        }
    }
}
