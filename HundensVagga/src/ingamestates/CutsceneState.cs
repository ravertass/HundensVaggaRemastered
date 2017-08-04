using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HundensVagga {
    internal class CutsceneState : IInGameState {

        private MainGameState mainGameState;

        public CutsceneState(MainGameState mainGameState) {
            this.mainGameState = mainGameState;
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            mainGameState.CursorManager.SetToDefault();

            if (mainGameState.CurrentRoom is ICutsceneRoom room) {
                room.Update(gameTime);
                if (room.ShouldGoToExit() || inputManager.IsLeftButtonPressed())
                    mainGameState.GoToRoom(room.ExitRoomName);
            } else {
                throw new Exception("At cutscene state without cutscene room.");
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            // nothing to draw
        }
    }
}
