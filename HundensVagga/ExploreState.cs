using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace HundensVagga {
    /// <summary>
    /// The in-game state when the player can interact with interactables in the room, or move between rooms.
    /// </summary>
    internal class ExploreState : IInGameState {
        private MainGameState mainGameState;

        public ExploreState(MainGameState mainGameState) {
            this.mainGameState = mainGameState;
        }

        public void Update(InputManager inputManager) {
            CheckInteractables(inputManager);
            CheckExits(inputManager);
            CheckInventoryBag(inputManager);
        }



        private void CheckInteractables(InputManager inputManager) {
            Interactable interactable = mainGameState.CurrentRoom.GetInteractableAt(inputManager.GetMousePosition());
            if (interactable != null) {
                ChangeCursorInteractable(interactable);
                HandleClicksInteractable(inputManager, interactable);
            }
        }

        private void ChangeCursorInteractable(Interactable interactable) {
            if (interactable.IsLookable() && interactable.IsUsable())
                mainGameState.CursorManager.SetToUseLook();
            else if (interactable.IsLookable())
                mainGameState.CursorManager.SetToLookOnly();
            else if (interactable.IsUsable())
                mainGameState.CursorManager.SetToUseOnly();
        }

        private static void HandleClicksInteractable(InputManager inputManager, Interactable interactable) {
            if (inputManager.IsLeftButtonPressed() && interactable.IsLookable())
                interactable.PlayLookSound();
        }



        private void CheckExits(InputManager inputManager) {
            Exit exit = mainGameState.CurrentRoom.GetExitAt(inputManager.GetMousePosition());
            if (exit != null) {
                ChangeCursorExit(exit);
                HandleClicksExit(inputManager, exit);
            }
        }

        private void ChangeCursorExit(Exit exit) {
            mainGameState.CursorManager.SetToDirection(exit.Direction);
        }

        private void HandleClicksExit(InputManager inputManager, Exit exit) {
            if (inputManager.IsLeftButtonPressed())
                mainGameState.CurrentRoom = mainGameState.Rooms.GetRoom(exit.RoomName);
        }



        private void CheckInventoryBag(InputManager inputManager) {
            if (mainGameState.Inventory.IsBagClicked(inputManager)) {
                mainGameState.CurrentState = new InventoryState(mainGameState);
                mainGameState.Inventory.GoDown();
            }
        }
    }
}
