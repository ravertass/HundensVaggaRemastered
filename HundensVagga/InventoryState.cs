using System;
using Microsoft.Xna.Framework.Graphics;

namespace HundensVagga {
    internal class InventoryState : IInGameState {
        private MainGameState mainGameState;

        public InventoryState(MainGameState mainGameState) {
            this.mainGameState = mainGameState;
        }

        public void Update(InputManager inputManager) {
            Inventory inventory = mainGameState.Inventory;
            inventory.SetItemCoords();

            if (mainGameState.Inventory.IsBagClicked(inputManager)) {
                inventory.GoUp();
                mainGameState.CurrentState = new ExploreState(mainGameState);
            }

            Item clickedItem = inventory.GetItemAt(inputManager.GetMousePosition());
            if (inputManager.IsLeftButtonPressed() && clickedItem != null) {
                inventory.GoUp();
                mainGameState.CurrentState = new UseItemState(mainGameState, clickedItem);
            }
        }
    }
}
