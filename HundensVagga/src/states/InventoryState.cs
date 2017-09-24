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
            CheckIcons(inputManager, inventory);
            CheckOutsideOfInventory(inputManager, inventory);
            CheckItems(inputManager, inventory);
        }

        private void CheckIcons(InputManager inputManager, Inventory inventory) {
            if (gameManager.Inventory.IsCursorOnAnIcon(inputManager))
                gameManager.CursorManager.SetToClick();

            if (gameManager.Inventory.IsExitIconClicked(inputManager))
                gameManager.GameStateManager.CurrentState = new ExitMenuState(gameManager);

            if (gameManager.Inventory.IsSaveGameIconClicked(inputManager)) {
                gameManager.SaveGameManager.SaveGame(gameManager.CurrentRoom);
                string gameSavedText =
                        gameManager.SubtitleManager.SubtitlesOn.Value ? "Game saved!"
                                                                      : "Spel sparat!";
                gameManager.SubtitleManager.Print(gameSavedText, 5);
            }

            if (gameManager.Inventory.IsLoadGameIconClicked(inputManager)) {
                LoadGameStatus loadGameStatus = gameManager.SaveGameManager.LoadGame(gameManager);
                if (loadGameStatus == LoadGameStatus.SUCCESS) {
                    string gameLoadedText =
                        gameManager.SubtitleManager.SubtitlesOn.Value ? "Game loaded!"
                                                                      : "Spel laddat!";
                    gameManager.SubtitleManager.Print(gameLoadedText, 5);
                    gameManager.GameStateManager.PushState();
                    gameManager.GameStateManager.CurrentState = this;
                } else if (loadGameStatus == LoadGameStatus.NO_FILE) {
                    string noSaveGameText =
                        gameManager.SubtitleManager.SubtitlesOn.Value ? "No saved game available!"
                                                                      : "Ingen sparning tillgänglig!";
                    gameManager.SubtitleManager.Print(noSaveGameText, 5);
                } else {
                    string failureText =
                        gameManager.SubtitleManager.SubtitlesOn.Value ? "Loading saved game failed!"
                                                                      : "Laddandet misslyckades!";
                    gameManager.SubtitleManager.Print(failureText, 5);
                }
            }

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
