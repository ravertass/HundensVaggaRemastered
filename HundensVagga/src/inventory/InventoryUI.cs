using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    /// <summary>
    /// Manages the inventory UI. Delegates Update() to an IInventoryUIState, which 
    /// manages when the inventory UI moves up and down.
    /// </summary>
    internal class InventoryUI {
        private const int X = 0;
        private const int BAG_Y_OFFSET = 100;

        private const int Y_MAX = 32;
        private const int Y_MIN = -BAG_Y_OFFSET + Y_MAX;
        public const int Y_SPEED = 5;
        public int Y { get; set; }

        public IInventoryUIState State { get; set; }

        private Texture2D bagTexture;
        private Texture2D backgroundTexture;
        private Texture2D exitIconTexture;

        public InventoryUI(MiscContent miscContent) {
            Y = Y_MIN;
            State = new InventoryUIStill();

            bagTexture = miscContent.InventoryBagImage;
            backgroundTexture = miscContent.InventoryBackgroundImage;
            exitIconTexture = miscContent.ExitIconImage;
        }

        public Rectangle BagRectangle() {
            return new Rectangle(X, Y + BAG_Y_OFFSET, bagTexture.Width, bagTexture.Height);
        }

        public Rectangle ExitIconRectangle() {
            return new Rectangle(Inventory.EXIT_ICON_X, 
                Y + (BAG_Y_OFFSET - exitIconTexture.Height)/2,
                exitIconTexture.Width, exitIconTexture.Height);
        }

        public bool IsUp() {
            return Y <= Y_MIN;
        }

        public bool IsDown() {
            return Y >= Y_MAX;
        }

        public void GoUp() {
            State = new InventoryGoingUp();
        }

        public void GoDown() {
            State = new InventoryGoingDown();
        }

        public void Update(InputManager inputManager) {
            State.Update(this);
        }

        public void Draw(SpriteBatch spriteBatch, IList<IItem> items) {
            if (!IsUp()) {
                spriteBatch.Draw(backgroundTexture, new Vector2(X, Y), Color.White);
                DrawItems(spriteBatch, items);
                DrawExitIcon(spriteBatch);
                DrawUpperBorder(spriteBatch);
            }
            DrawBag(spriteBatch);
        }

        private void DrawItems(SpriteBatch spriteBatch, IList<IItem> items) {
            foreach (Item item in items) {
                spriteBatch.Draw(item.Texture, 
                    new Vector2(item.Coords.X, Y + item.Coords.Y - Y_MAX), Color.White);
            }
        }

        private void DrawExitIcon(SpriteBatch spriteBatch) {
            spriteBatch.Draw(exitIconTexture, ExitIconRectangle(), Color.White);
        }

        private void DrawUpperBorder(SpriteBatch spriteBatch) {
            Texture2D blackTexture =
                new Texture2D(spriteBatch.GraphicsDevice, Main.SCREEN_WIDTH, 32);
            blackTexture.Dispose();
            spriteBatch.Draw(blackTexture, new Vector2(0f, 0f), Color.Black);
        }

        private void DrawBag(SpriteBatch spriteBatch) {
            spriteBatch.Draw(bagTexture, BagRectangle(), Color.White);
        }
    }
}
