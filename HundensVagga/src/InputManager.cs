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
        private Main main;
        private bool justToggled = false;

        public InputManager(Main main) {
            this.main = main;
        }

        public Vector2 GetMousePosition() {
            return Mouse.GetState().Position.ToVector2();
        }

        public bool IsLeftButtonPressed() {
            return Mouse.GetState().LeftButton == ButtonState.Pressed
                && lastMouseState.LeftButton == ButtonState.Released
                && main.IsActive;
        }

        public bool IsRightButtonPressed() {
            return Mouse.GetState().RightButton == ButtonState.Pressed
                && lastMouseState.RightButton == ButtonState.Released
                && main.IsActive;
        }

        public void Update() {
            lastMouseState = Mouse.GetState();
            CheckForExit();
            CheckForFullScreen();
        }

        private void CheckForExit() {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                main.Exit();
        }

        private void CheckForFullScreen() {
            if (!justToggled && IsAltEnterPressed()) {
                main.ToggleFullScreen();
                justToggled = true;
            }

            if (!IsAltEnterPressed()) {
                justToggled = false;
            }
        }

        private bool IsAltEnterPressed() {
            return (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) ||
                    Keyboard.GetState().IsKeyDown(Keys.RightAlt)) &&
                   Keyboard.GetState().IsKeyDown(Keys.Enter);
        }
    }
}
