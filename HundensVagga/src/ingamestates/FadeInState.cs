using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HundensVagga {
    internal class FadeInState : IInGameState {
        private MainGameState mainGameState;

        private double elapsedTime;
        private static readonly double TIME_STEP = 0.025;

        private int rectTransparency;
        private static readonly int TRANSPARENCY_STEP = -5;
        private static readonly int MAX_TRANSPARENCY = 255;

        public FadeInState(MainGameState mainGameState) {
            this.mainGameState = mainGameState;
            rectTransparency = MAX_TRANSPARENCY;
        }

        public void Draw(SpriteBatch spriteBatch) {
            DrawTransparentBlackBackground(spriteBatch);
        }

        private void DrawTransparentBlackBackground(SpriteBatch spriteBatch) {
            Texture2D blackTexture =
                new Texture2D(spriteBatch.GraphicsDevice, Main.WINDOW_WIDTH, Main.WINDOW_HEIGHT);
            blackTexture.Dispose();
            spriteBatch.Draw(blackTexture, new Vector2(0f, 0f), 
                new Color(0, 0, 0, rectTransparency));
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;
            elapsedTime += delta;

            while (elapsedTime >= TIME_STEP) {
                elapsedTime -= TIME_STEP;
                AdvanceTransparency();
            }

            if (rectTransparency <= 0)
                FinishState();
        }

        private void AdvanceTransparency() {
            rectTransparency += TRANSPARENCY_STEP;
        }

        private void FinishState() {
            mainGameState.InGameStateManager.PopState();
        }
    }
}