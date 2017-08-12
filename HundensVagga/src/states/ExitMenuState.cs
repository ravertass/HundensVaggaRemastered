using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class ExitMenuState : IGameState {
        private GameManager gameManager;
        private Texture2D menuImage;

        private Rectangle yesRectangle;
        private Rectangle noRectangle;

        public ExitMenuState(GameManager gameManager) {
            this.gameManager = gameManager;
            menuImage = gameManager.MiscContent.ExitMenuImage;

            yesRectangle = gameManager.MiscContent.ExitMenuYesRect;
            yesRectangle.Offset(MenuX(), MenuY());

            noRectangle = gameManager.MiscContent.ExitMenuNoRect;
            noRectangle.Offset(MenuX(), MenuY());
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            gameManager.CursorManager.SetToDefault();

            if (noRectangle.Contains(inputManager.GetMousePosition())
                || yesRectangle.Contains(inputManager.GetMousePosition()))
                gameManager.CursorManager.SetToClick();

            if (inputManager.IsLeftButtonPressed()
                    && noRectangle.Contains(inputManager.GetMousePosition())) {
                gameManager.Inventory.GoUp();
                gameManager.GameStateManager.PopState();
            }

            if (inputManager.IsLeftButtonPressed()
                    && yesRectangle.Contains(inputManager.GetMousePosition()))
                gameManager.ExitGame();
        }

        public void Draw(SpriteBatch spriteBatch) {
            DrawTransparentBlackBackground(spriteBatch);
            DrawLetter(spriteBatch);
        }

        private int MenuX() {
            return (Main.WINDOW_WIDTH - menuImage.Width) / 2;
        }

        private int MenuY() {
            return (Main.WINDOW_HEIGHT - menuImage.Height) / 2;
        }

        private static void DrawTransparentBlackBackground(SpriteBatch spriteBatch) {
            Texture2D blackTexture =
                new Texture2D(spriteBatch.GraphicsDevice, Main.WINDOW_WIDTH, Main.WINDOW_HEIGHT);
            blackTexture.Dispose();
            spriteBatch.Draw(blackTexture, new Vector2(0f, 0f), new Color(0, 0, 0, 150));
        }

        private void DrawLetter(SpriteBatch spriteBatch) {
            spriteBatch.Draw(menuImage, new Vector2(MenuX(), MenuY()), Color.White);
        }
    }
}

