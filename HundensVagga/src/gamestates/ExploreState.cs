﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace HundensVagga {
    /// <summary>
    /// The in-game state when the player can interact with interactables in the room, 
    /// or move between rooms.
    /// </summary>
    internal class ExploreState : IInGameState {
        protected MainGameState mainGameState;

        public ExploreState(MainGameState mainGameState) {
            this.mainGameState = mainGameState;
        }

        public virtual void Update(InputManager inputManager) {
            // inventory has priority over exits and interactables
            if (CheckInventoryBag(inputManager))
                return;

            // exits have priority over interactables
            if (CheckExits(inputManager))
                return;

            CheckInteractables(inputManager);
        }


        private bool CheckExits(InputManager inputManager) {
            Exit exit = mainGameState.CurrentRoom.GetExitAt(inputManager.GetMousePosition());
            if (exit != null) {
                ChangeCursorExit(exit);
                HandleClicksExit(inputManager, exit);
                return true;
            }

            return false;
        }

        private void ChangeCursorExit(Exit exit) {
            mainGameState.CursorManager.SetToDirection(exit.Direction);
        }

        private void HandleClicksExit(InputManager inputManager, Exit exit) {
            if (inputManager.IsLeftButtonPressed())
                UseExit(exit);
        }

        protected virtual void UseExit(Exit exit) {
            exit.DoEffects();
            mainGameState.GoToRoom(exit.RoomName);
        }


        private void CheckInteractables(InputManager inputManager) {
            Interactable interactable =
                mainGameState.CurrentRoom.GetInteractableAt(inputManager.GetMousePosition());
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

        private void HandleClicksInteractable(InputManager inputManager,
                Interactable interactable) {
            if (inputManager.IsLeftButtonPressed() && interactable.IsLookable())
                interactable.Look();
            if (inputManager.IsRightButtonPressed() && interactable.IsUsable())
                UseInteractable(interactable);
        }

        protected virtual void UseInteractable(Interactable interactable) {
            interactable.Use(mainGameState);
        }


        private bool CheckInventoryBag(InputManager inputManager) {
            if (mainGameState.Inventory.IsBagClicked(inputManager)) {
                mainGameState.InGameStateManager.PushState();
                mainGameState.InGameStateManager.CurrentState = new InventoryState(mainGameState);
                mainGameState.Inventory.GoDown();
                return true;
            }

            return false;
        }
    }
}
