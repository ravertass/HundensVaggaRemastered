using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace HundensVagga {
    /// <summary>
    /// The in-game state when the player can interact with interactables in the room, 
    /// or move between rooms.
    /// </summary>
    internal class ExploreState : IGameState {
        protected GameManager gameManager;
        private Interactable interactableHoveredOver;

        public ExploreState(GameManager gameManager) {
            this.gameManager = gameManager;
        }

        public virtual void Update(InputManager inputManager, GameTime gameTime) {
            // inventory has priority over exits and interactables
            if (gameManager.CurrentRoom.WithInventory && CheckInventoryBag(inputManager))
                return;

            // exits have priority over interactables
            if (CheckExits(inputManager))
                return;

            CheckInteractables(inputManager);
        }

        private bool CheckExits(InputManager inputManager) {
            Exit exit = gameManager.CurrentRoom.GetExitAt(inputManager.GetMousePosition());
            if (exit != null) {
                ChangeCursorExit(exit);
                HandleClicksExit(inputManager, exit);
                return true;
            }

            return false;
        }

        private void ChangeCursorExit(Exit exit) {
            gameManager.CursorManager.SetToDirection(exit.Direction);
        }

        private void HandleClicksExit(InputManager inputManager, Exit exit) {
            if (inputManager.IsLeftButtonPressed())
                UseExit(exit);
        }

        protected virtual void UseExit(Exit exit) {
            exit.DoEffects();
            gameManager.GoToRoom(exit.RoomName);
        }

        private void CheckInteractables(InputManager inputManager) {
            Interactable interactable =
                gameManager.CurrentRoom.GetInteractableAt(inputManager.GetMousePosition());
            if (interactable != null) {
                ChangeCursorInteractable(interactable);
                Hover(interactable);
                HandleClicksInteractable(inputManager, interactable);
            }
        }

        private void ChangeCursorInteractable(Interactable interactable) {
            if (interactable.IsClickable())
                gameManager.CursorManager.SetToClick();
            else if (interactable.IsLookable() && interactable.IsUsable())
                gameManager.CursorManager.SetToUseLook();
            else if (interactable.IsLookable())
                gameManager.CursorManager.SetToLookOnly();
            else if (interactable.IsUsable())
                gameManager.CursorManager.SetToUseOnly();
        }

        private void HandleClicksInteractable(InputManager inputManager,
                Interactable interactable) {
            if ((inputManager.IsLeftButtonPressed() || inputManager.IsRightButtonPressed())
                && interactable.IsClickable())
                ClickAt(interactable);
            else if (inputManager.IsLeftButtonPressed() && interactable.IsLookable())
                LookAt(interactable);
            else if (inputManager.IsRightButtonPressed() && interactable.IsUsable())
                UseInteractable(interactable);
        }

        protected virtual void ClickAt(Interactable interactable) {
            interactable.ClickAt(gameManager);
        }

        protected virtual void Hover(Interactable interactable) {
            if (interactableHoveredOver != interactable) {
                interactableHoveredOver = interactable;
                interactable.Hover(gameManager);
            }
        }

        private void LookAt(Interactable interactable) {
            gameManager.SoundAndSubtitleManager.PlayAndPrint(interactable.LookSoundAndSubtitle);
        }

        protected virtual void UseInteractable(Interactable interactable) {
            interactable.Use(gameManager);
        }

        private bool CheckInventoryBag(InputManager inputManager) {
            if (gameManager.Inventory.IsBagClicked(inputManager)) {
                gameManager.GameStateManager.PushState();
                gameManager.GameStateManager.CurrentState = new InventoryState(gameManager);
                gameManager.Inventory.GoDown();
                return true;
            } else if (gameManager.Inventory.IsCursorOnBag(inputManager)) {
                gameManager.CursorManager.SetToClick();
                return true;
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch) {
            // nothing to draw
        }
    }
}
