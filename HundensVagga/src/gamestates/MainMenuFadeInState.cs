using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HundensVagga {
    internal class MainMenuFadeInState : IGameState {

        private Texture2D background;

        private double elapsedTime;
        private static readonly double TIME_STEP = 0.01;

        private float backgroundTransparency;
        private static readonly float BACKGROUND_TRANSPARENCY_STEP = 0.005f;
        private static readonly float MAX_BACKGROUND_TRANSPARENCY = 1;

        private StateManager stateManager;

        public MainMenuFadeInState(StateManager stateManager, MiscContent miscContent) {
            this.stateManager = stateManager;
            background = miscContent.MainMenuBackgroundImage;
            elapsedTime = 0;
            backgroundTransparency = 0;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(background, new Vector2(0f, 0f), 
                Color.White * backgroundTransparency);
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;
            elapsedTime += delta;

            while (elapsedTime >= TIME_STEP) {
                elapsedTime -= TIME_STEP;
                AdvanceBackground();
            }
        }

        private void AdvanceBackground() {
            backgroundTransparency += BACKGROUND_TRANSPARENCY_STEP;

            if (backgroundTransparency >= MAX_BACKGROUND_TRANSPARENCY)
                FinishState();
        }

        private void FinishState() {
            stateManager.GoToMainMenuState();
        }
    }
}
