using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    public class InputManager {
        public Vector2 GetMousePosition() {
            return Mouse.GetState().Position.ToVector2();
        }

        public bool IsLeftButtonPressed() {
            return Mouse.GetState().LeftButton == ButtonState.Pressed;
        }

        public bool IsRightButtonPressed() {
            return Mouse.GetState().RightButton == ButtonState.Pressed;
        }
    }
}
