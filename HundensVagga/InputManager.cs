using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    public class InputManager {
        private MouseState lastMouseState;

        public Vector2 GetMousePosition() {
            return Mouse.GetState().Position.ToVector2();
        }

        public bool IsLeftButtonPressed() {
            return Mouse.GetState().LeftButton == ButtonState.Pressed
                && lastMouseState.LeftButton == ButtonState.Released;
        }

        public bool IsRightButtonPressed() {
            return Mouse.GetState().RightButton == ButtonState.Pressed
                && lastMouseState.RightButton == ButtonState.Released;
        }

        public void Update() {
            lastMouseState = Mouse.GetState();
        }
    }
}
