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
        private Texture2D subtitlesOnIconTexture;
        private Texture2D subtitlesOffIconTexture;

        private WorldStateVariable subtitlesOn;

        public InventoryUI(MiscContent miscContent, WorldStateVariable subtitlesOn) {
            this.subtitlesOn = subtitlesOn;

            Y = Y_MIN;
            State = new InventoryUIStill();

            bagTexture = miscContent.InventoryBagImage;
            backgroundTexture = miscContent.InventoryBackgroundImage;
            exitIconTexture = miscContent.ExitIconImage;
            subtitlesOnIconTexture = miscContent.SubtitlesOnIconImage;
            subtitlesOffIconTexture = miscContent.SubtitlesOffIconImage;
        }

        public Rectangle BagRectangle() {
            return new Rectangle(X, Y + BAG_Y_OFFSET, bagTexture.Width, bagTexture.Height);
        }

        public Rectangle InventoryRectangle() {
            return new Rectangle(X, Y, backgroundTexture.Width, backgroundTexture.Height);
        }

        public Rectangle ExitIconRectangle() {
            return new Rectangle(Inventory.ICON_X,
                Y + ((int)(BAG_Y_OFFSET * 3/4) - exitIconTexture.Height)/2,
                exitIconTexture.Width, exitIconTexture.Height);
        }

        public Rectangle SubtitlesIconRectangle() {
            return new Rectangle(Inventory.ICON_X,
                Y + ((int)(BAG_Y_OFFSET * 5/4) - subtitlesOnIconTexture.Height) / 2,
                subtitlesOnIconTexture.Width, subtitlesOnIconTexture.Height);
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
                DrawSubtitlesIcon(spriteBatch);
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

        private void DrawSubtitlesIcon(SpriteBatch spriteBatch) {
            Texture2D subtitlesIconTexture = (subtitlesOn.Value)
                                             ? subtitlesOnIconTexture
                                             : subtitlesOffIconTexture;
            spriteBatch.Draw(subtitlesIconTexture, SubtitlesIconRectangle(), Color.White);
        }

        private void DrawUpperBorder(SpriteBatch spriteBatch) {
            Texture2D blackTexture =
                new Texture2D(spriteBatch.GraphicsDevice, Main.WINDOW_WIDTH, 32);
            blackTexture.Dispose();
            spriteBatch.Draw(blackTexture, new Vector2(0f, 0f), Color.Black);
        }

        private void DrawBag(SpriteBatch spriteBatch) {
            spriteBatch.Draw(bagTexture, BagRectangle(), Color.White);
        }
    }
}
