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
    internal class UseItemState : IGameState {
        private GameManager gameManager;
        private IItem currentItem;
        private SoundEffectInstance failSound;

        public UseItemState(GameManager gameManager, IItem currentItem) {
            this.gameManager = gameManager;
            this.currentItem = currentItem;
            this.failSound = gameManager.MiscContent.ItemFailSound;
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            gameManager.CursorManager.SetToItem();
            CheckInteractables(inputManager);
        }

        private void CheckInteractables(InputManager inputManager) {
            Interactable interactable = 
                gameManager.CurrentRoom.GetInteractableAt(inputManager.GetMousePosition());
            if (inputManager.IsLeftButtonPressed()) {
                if (interactable != null && interactable.IsItemUsable(currentItem)) {
                    interactable.UseItem(currentItem, gameManager);
                } else {
                    failSound.Play();
                }
                gameManager.GameStateManager.PopState();
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            // nothing to draw
        }
    }
}
