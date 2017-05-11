using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HundensVagga {
    class MainMenuState : IGameState {
        private StateManager stateManager;
        private ExitGameManager exitGameManager;
        private CursorManager cursorManager;
        private SongManager songManager;

        private Texture2D background;

        private Rectangle startButton;
        private Rectangle exitButton;

        public MainMenuState(StateManager stateManager, Main main) {
            this.stateManager = stateManager;
            exitGameManager = main.ExitGameManager;
            cursorManager = main.CursorManager;
            songManager = main.SongManager;

            MiscContent miscContent = main.MiscContent;
            background = miscContent.MainMenuBackgroundImage;
            startButton = miscContent.MainMenuStartRect;
            exitButton = miscContent.MainMenuExitRect;

            songManager.FadeIntoSong(miscContent.MainMenuSong);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(background, new Vector2(0f, 0f), Color.White);
            cursorManager.Draw(spriteBatch);
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            songManager.Update();

            if (inputManager.IsLeftButtonPressed()) {
                if (startButton.Contains(inputManager.GetMousePosition()))
                    FinishState();
                else if (exitButton.Contains(inputManager.GetMousePosition()))
                    ExitGame();
            }
        }

        private void FinishState() {
            stateManager.GoToIntroState();
        }

        private void ExitGame() {
            exitGameManager.Exit();
        }
    }
}
