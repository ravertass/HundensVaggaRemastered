using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class ExitMenuState : IInGameState {
        private MainGameState mainGameState;
        private Texture2D menuImage;

        private Rectangle yesRectangle;
        private Rectangle noRectangle;

        public ExitMenuState(MainGameState mainGameState) {
            this.mainGameState = mainGameState;
            menuImage = mainGameState.MiscContent.ExitMenuImage;

            yesRectangle = mainGameState.MiscContent.ExitMenuYesRect;
            yesRectangle.Offset(MenuX(), MenuY());

            noRectangle = mainGameState.MiscContent.ExitMenuNoRect;
            noRectangle.Offset(MenuX(), MenuY());
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            mainGameState.CursorManager.SetToDefault();

            if (inputManager.IsLeftButtonPressed()
                    && noRectangle.Contains(inputManager.GetMousePosition())) {
                mainGameState.Inventory.GoUp();
                mainGameState.InGameStateManager.PopState();
            }

            if (inputManager.IsLeftButtonPressed()
                    && yesRectangle.Contains(inputManager.GetMousePosition()))
                mainGameState.ExitGame();
        }

        public void Draw(SpriteBatch spriteBatch) {
            DrawTransparentBlackBackground(spriteBatch);
            DrawLetter(spriteBatch);
        }

        private int MenuX() {
            return (Main.SCREEN_WIDTH - menuImage.Width) / 2;
        }

        private int MenuY() {
            return (Main.SCREEN_HEIGHT - menuImage.Height) / 2;
        }

        private static void DrawTransparentBlackBackground(SpriteBatch spriteBatch) {
            Texture2D blackTexture =
                new Texture2D(spriteBatch.GraphicsDevice, Main.SCREEN_WIDTH, Main.SCREEN_HEIGHT);
            blackTexture.Dispose();
            spriteBatch.Draw(blackTexture, new Vector2(0f, 0f), new Color(0, 0, 0, 150));
        }

        private void DrawLetter(SpriteBatch spriteBatch) {
            spriteBatch.Draw(menuImage, new Vector2(MenuX(), MenuY()), Color.White);
        }
    }
}

