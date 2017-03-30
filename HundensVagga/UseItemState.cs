using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    public class UseItemState : IInGameState {
        private MainGameState mainGameState;
        private Item currentItem;

        public UseItemState(MainGameState mainGameState, Item currentItem) {
            this.mainGameState = mainGameState;
            this.currentItem = currentItem;
        }

        public void Update(InputManager inputManager) {
            mainGameState.CursorManager.SetToItem();

            Interactable interactable = mainGameState.CurrentRoom.GetInteractableAt(inputManager.GetMousePosition());
            if (inputManager.IsLeftButtonPressed()) {
                if (interactable != null && interactable.IsItemUsable(currentItem)) {
                    Console.WriteLine("hej");
                } else {
                    Console.WriteLine("nej");
                    // play fail sound
                }
                mainGameState.CurrentState = new ExploreState(mainGameState);
            }
        }
    }
}
