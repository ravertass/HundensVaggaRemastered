using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    /// <summary>
    /// The in-game state when the player can use an item picked from the inventory.
    /// </summary>
    internal class UseItemState : IInGameState {
        private MainGameState mainGameState;
        private IItem currentItem;
        private SoundEffectInstance failSound;

        public UseItemState(MainGameState mainGameState, IItem currentItem) {
            this.mainGameState = mainGameState;
            this.currentItem = currentItem;
            this.failSound = mainGameState.MiscContent.ItemFailSound;
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            mainGameState.CursorManager.SetToItem();
            CheckInteractables(inputManager);
        }

        private void CheckInteractables(InputManager inputManager) {
            Interactable interactable = 
                mainGameState.CurrentRoom.GetInteractableAt(inputManager.GetMousePosition());
            if (inputManager.IsLeftButtonPressed()) {
                if (interactable != null && interactable.IsItemUsable(currentItem)) {
                    interactable.UseItem(currentItem, mainGameState);
                } else {
                    failSound.Play();
                }
                mainGameState.InGameStateManager.PopState();
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            // nothing to draw
        }
    }
}
