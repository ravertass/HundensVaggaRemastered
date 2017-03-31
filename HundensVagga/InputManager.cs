using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    /// <summary>
    /// Manages all game input (mostly mouse input).
    /// </summary>
    public class InputManager {
        private MouseState lastMouseState;
        private Game game;

        public InputManager(Game game) {
            this.game = game;
        }

        public Vector2 GetMousePosition() {
            return Mouse.GetState().Position.ToVector2();
        }

        public bool IsLeftButtonPressed() {
            return Mouse.GetState().LeftButton == ButtonState.Pressed
                && lastMouseState.LeftButton == ButtonState.Released
                && game.IsActive;
        }

        public bool IsRightButtonPressed() {
            return Mouse.GetState().RightButton == ButtonState.Pressed
                && lastMouseState.RightButton == ButtonState.Released
                && game.IsActive;
        }

        public void Update() {
            lastMouseState = Mouse.GetState();
            CheckForExit();
        }

        private void CheckForExit() {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                game.Exit();
        }
    }
}
