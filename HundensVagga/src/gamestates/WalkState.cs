using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HundensVagga {
    internal class WalkState : IInGameState {

        private MainGameState mainGameState;

        public WalkState(MainGameState mainGameState) {
            this.mainGameState = mainGameState;
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            if (mainGameState.CurrentRoom is WalkRoom room) {
                room.Update(gameTime);
                if (room.ShouldGoToExit()) {
                    mainGameState.GoToRoom(room.ExitRoomName);
                }
            }
        }
    }
}
