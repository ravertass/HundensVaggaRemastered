using Microsoft.Xna.Framework;

namespace HundensVagga {
    internal class ExitGameManager {
        private Game game;

        public ExitGameManager(Game game) {
            this.game = game;
        }

        public void Exit() {
            game.Exit();
        }
    }
}