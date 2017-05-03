using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HundensVagga {
    internal class CutsceneState : IInGameState {

        private MainGameState mainGameState;

        public CutsceneState(MainGameState mainGameState) {
            this.mainGameState = mainGameState;
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            mainGameState.CursorManager.SetToDirection(Direction.up);

            if (mainGameState.CurrentRoom is ICutsceneRoom room) {
                room.Update(gameTime);
                if (room.ShouldGoToExit() || inputManager.IsLeftButtonPressed())
                    mainGameState.GoToRoom(room.ExitRoomName);
            } else {
                throw new Exception("At cutscene state without cutscene room.");
            }
        }
    }
}
