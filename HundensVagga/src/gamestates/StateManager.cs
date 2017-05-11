using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    /// <summary>
    /// Used so game states can change state without a reference to the Main class instance.
    /// </summary>
    internal class StateManager {
        private IGameState currentState;

        private Main main;

        public StateManager(Main main) {
            this.main = main;
        }

        public void GoToStartState() {
            GoToLogoState();
        }

        public void UpdateCurrentState(InputManager inputManager, GameTime gameTime) {
            currentState.Update(inputManager, gameTime);
        }

        public void DrawCurrentState(SpriteBatch spriteBatch) {
            currentState.Draw(spriteBatch);
        }

        public void GoToLogoState() {
            currentState = new LogoState(this, main.MiscContent);
        }

        public void GoToMainMenuFadeInState() {
            currentState = new MainMenuFadeInState(this, main.MiscContent);
        }

        public void GoToMainMenuState() {
            currentState = new MainMenuState(this, main);
        }

        public void GoToIntroState() {

        }

        public void GoToMainGameState() {
            currentState = new MainGameState(this, main);
        }

        public void GoToCreditsFadeInState() {

        }

        public void GoToCreditsState() {

        }

        public void GoToCreditsFadeOutState() {

        }

        public void GoToLastMelodyState() {

        }
    }
}
