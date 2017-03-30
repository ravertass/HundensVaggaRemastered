using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace HundensVagga {
    class ExploreState : IInGameState {
        private MainGameState mainGameState;

        public ExploreState(MainGameState mainGameState) {
            this.mainGameState = mainGameState;
        }

        public void Update(InputManager inputManager) {
            CheckInteractables(inputManager);
            CheckExits(inputManager);

            if (mainGameState.Inventory.IsBagClicked(inputManager)) {
                mainGameState.CurrentState = new InventoryState(mainGameState);
                mainGameState.Inventory.GoDown();
            }
        }

        private void CheckInteractables(InputManager inputManager) {
            Interactable interactable = mainGameState.CurrentRoom.GetInteractableAt(inputManager.GetMousePosition());
            if (interactable != null) {
                if (interactable.IsLookable() && interactable.IsUsable())
                    mainGameState.CursorManager.SetToUseLook();
                else if (interactable.IsLookable())
                    mainGameState.CursorManager.SetToLookOnly();
                else if (interactable.IsUsable())
                    mainGameState.CursorManager.SetToUseOnly();

                if (inputManager.IsLeftButtonPressed() && interactable.IsLookable())
                    interactable.PlayLookSound();
            }
        }

        private void CheckExits(InputManager inputManager) {
            Exit exit = mainGameState.CurrentRoom.GetExitAt(inputManager.GetMousePosition());
            if (exit != null) {
                mainGameState.CursorManager.SetToDirection(exit.Direction);
                if (inputManager.IsLeftButtonPressed())
                    mainGameState.CurrentRoom = mainGameState.Rooms.GetRoom(exit.RoomName);
            }
        }
    }
}
