using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HundensVagga {
    internal class CutsceneState : IGameState {

        private GameManager gameManager;

        public CutsceneState(GameManager gameManager) {
            this.gameManager = gameManager;
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            gameManager.CursorManager.SetToDefault();

            if (gameManager.CurrentRoom is ICutsceneRoom room) {
                room.Update(gameTime);
                if (room.ShouldGoToExit() || inputManager.IsLeftButtonPressed()) {
                    room.Stop();
                    gameManager.GoToRoom(room.ExitRoomName);
                }
            } else {
                throw new Exception("At cutscene state without cutscene room.");
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            // nothing to draw
        }
    }
}
