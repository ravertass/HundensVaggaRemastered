using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HundensVagga {
    internal class LetterItemState : IGameState {
        private GameManager gameManager;
        private Texture2D letterImage;

        public LetterItemState(GameManager gameManager, Texture2D letterImage) {
            this.gameManager = gameManager;
            this.letterImage = letterImage;
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            gameManager.CursorManager.SetToClick();

            if (inputManager.IsLeftButtonPressed())
                gameManager.GameStateManager.PopState();
        }

        public void Draw(SpriteBatch spriteBatch) {
            DrawTransparentBlackBackground(spriteBatch);
            DrawLetter(spriteBatch);
        }

        private static void DrawTransparentBlackBackground(SpriteBatch spriteBatch) {
            Texture2D blackTexture =
                new Texture2D(spriteBatch.GraphicsDevice, Main.WINDOW_WIDTH, Main.WINDOW_HEIGHT);
            blackTexture.Dispose();
            spriteBatch.Draw(blackTexture, new Vector2(0f, 0f), new Color(0, 0, 0, 150));
        }

        private void DrawLetter(SpriteBatch spriteBatch) {
            float letterX = (Main.WINDOW_WIDTH - letterImage.Width) / 2;
            float letterY = (Main.WINDOW_HEIGHT - letterImage.Height) / 2;
            spriteBatch.Draw(letterImage, new Vector2(letterX, letterY), Color.White);
        }
    }
}
