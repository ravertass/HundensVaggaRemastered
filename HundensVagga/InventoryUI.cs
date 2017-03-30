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
    public class InventoryUI {
        private const int X = 0;
        private const int BAG_Y_OFFSET = 100;

        private const int ITEM_X_OFFSET = 25;
        private const int ITEM_X_DIFF = 100;
        private const int ITEM_Y_OFFSET = 25;

        private const int Y_MIN = -BAG_Y_OFFSET;
        private const int Y_MAX = 0;
        public const int Y_SPEED = 5;
        public int Y { get; set; }

        public IInventoryState State { get; set; }

        private const string BAG_PATH = "bag";
        private Texture2D bagTexture;

        private const string BACKGROUND_PATH = "inventory";
        private Texture2D backgroundTexture;

        public InventoryUI(ContentManager content) {
            Y = Y_MIN;
            State = new InventoryStateUp();

            bagTexture = content.Load<Texture2D>(Main.MISC_DIR 
                + Path.DirectorySeparatorChar + BAG_PATH);
            backgroundTexture = content.Load<Texture2D>(Main.MISC_DIR 
                + Path.DirectorySeparatorChar + BACKGROUND_PATH);
        }

        public Rectangle BagRectangle() {
            return new Rectangle(X, Y + BAG_Y_OFFSET, bagTexture.Width, bagTexture.Height);
        }

        public bool IsUp() {
            return Y <= Y_MIN;
        }

        public bool IsDown() {
            return Y >= Y_MAX;
        }

        public void Update(InputManager inputManager) {
            State.Update(this, inputManager);
        }

        public void Draw(SpriteBatch spriteBatch, IList<Item> items) {
            if (!IsUp()) {
                spriteBatch.Draw(backgroundTexture, new Vector2(X, Y), Color.White);
                DrawItems(spriteBatch, items);
            }
            spriteBatch.Draw(bagTexture, new Vector2(X, Y + BAG_Y_OFFSET), Color.White);
        }

        private void DrawItems(SpriteBatch spriteBatch, IList<Item> items) {
            int x = X + ITEM_X_OFFSET;
            foreach (Item item in items) {
                spriteBatch.Draw(item.Texture, new Vector2(x, Y + ITEM_Y_OFFSET), Color.White);
                x += ITEM_X_DIFF;
            }
        }
    }
}
