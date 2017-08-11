using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace HundensVagga {
    /// <summary>
    /// Manages the appearance of the cursor (not the input; for that, see the InputManager).
    /// </summary>
    internal class CursorManager {
        private const string DEFAULT = "0_0";
        private const string USE_ONLY = "0_1";
        private const string LOOK_ONLY = "1_0";
        private const string USE_LOOK = "1_1";
        private const string CLICK = "click";

        private const string UP = "up";
        private const string DOWN = "down";
        private const string LEFT = "left";
        private const string RIGHT = "right";

        private const string ITEM = "item";

        private ContentManager content;
        private InputManager inputManager;
        private Texture2D texture;

        public CursorManager(ContentManager content, InputManager inputManager) {
            this.content = content;
            this.inputManager = inputManager;
            SetTo(DEFAULT);
        }

        public void Draw(SpriteBatch spriteBatch) {
            Vector2 coords = new Vector2(
                inputManager.GetMousePosition().X - texture.Width/2,
                inputManager.GetMousePosition().Y);
            spriteBatch.Draw(texture, coords, Color.White);
        }

        private void SetTo(string cursorPath) {
            texture = content.Load<Texture2D>(Main.CURSORS_DIR 
                + Path.DirectorySeparatorChar + cursorPath);
        }

        public void SetToDefault() {
            SetTo(DEFAULT);
        }

        public void SetToClick() {
            SetTo(CLICK);
        }

        public void SetToUseOnly() {
            SetTo(USE_ONLY);
        }

        public void SetToLookOnly() {
            SetTo(LOOK_ONLY);
        }

        public void SetToUseLook() {
            SetTo(USE_LOOK);
        }

        public void SetToUp() {
            SetTo(UP);
        }

        public void SetToDown() {
            SetTo(DOWN);
        }

        public void SetToLeft() {
            SetTo(LEFT);
        }

        public void SetToRight() {
            SetTo(RIGHT);
        }

        public void SetToDirection(Direction dir) {
            if (dir == Direction.up)
                SetToUp();
            else if (dir == Direction.down)
                SetToDown();
            else if (dir == Direction.left)
                SetToLeft();
            else if (dir == Direction.right)
                SetToRight();
        }

        public void SetToItem() {
            SetTo(ITEM);
        }
    }
}