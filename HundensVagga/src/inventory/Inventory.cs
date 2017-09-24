using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    /// <summary>
    /// The in-game inventory. Keeps track of its items, and delegates
    /// UI-related stuff to an InventoryUI instance.
    /// </summary>
    internal class Inventory {
        public const int ICON_X = 8;

        private const int ITEM_X_OFFSET = ICON_X + 56;
        private const int ITEM_X_DIFF = 64;
        public const int ITEM_Y_OFFSET = 57;

        private InventoryUI ui;
        private IList<IItem> items;

        private WorldStateVariable subtitlesOn;

        public Inventory(MiscContent miscContent, WorldStateVariable subtitlesOn) {
            ui = new InventoryUI(miscContent, subtitlesOn);
            items = new List<IItem>();
            this.subtitlesOn = subtitlesOn;
        }

        public void AddItem(IItem item) {
            items.Add(item);
        }

        public void RemoveItem(IItem item) {
            items.Remove(item);
        }

        public void Update(InputManager inputManager) {
            ui.Update(inputManager);
        }

        public void Draw(SpriteBatch spriteBatch) {
            ui.Draw(spriteBatch, items);
        }

        public void SetItemCoords() {
            int x = ITEM_X_OFFSET;
            foreach (Item item in items) {
                item.Coords = new Vector2(x, ITEM_Y_OFFSET);
                x += ITEM_X_DIFF;
            }
        }

        public IItem GetItemAt(Vector2 coords) {
            foreach (IItem item in items)
                if (item.Rectangle.Contains(coords))
                    return item;

            return null;
        }

        public bool IsBagClicked(InputManager inputManager) {
            return inputManager.IsLeftButtonPressed()
                && IsCursorOnBag(inputManager);
        }

        public bool IsOutsideOfInventoryClicked(InputManager inputManager) {
            return inputManager.IsLeftButtonPressed()
                && IsCursorOutsideOfInventory(inputManager);
        }

        private bool IsCursorOutsideOfInventory(InputManager inputManager) {
            return !ui.InventoryRectangle().Contains(inputManager.GetMousePosition());
        }

        public bool IsCursorOnBag(InputManager inputManager) {
            return ui.BagRectangle().Contains(inputManager.GetMousePosition());
        }

        internal bool IsExitIconClicked(InputManager inputManager) {
            return ui.IsDown() && inputManager.IsLeftButtonPressed()
                && IsCursorOnExitIcon(inputManager);
        }

        internal void HandleSubtitlesIconClicks(InputManager inputManager) {
            if (IsSubtitlesIconClicked(inputManager))
                subtitlesOn.Value = !subtitlesOn.Value;
        }

        private bool IsSubtitlesIconClicked(InputManager inputManager) {
            return ui.IsDown() && inputManager.IsLeftButtonPressed()
                && IsCursorOnSubtitlesIcon(inputManager);
        }

        public bool IsCursorOnAnIcon(InputManager inputManager) {
            return IsCursorOnExitIcon(inputManager) || IsCursorOnSubtitlesIcon(inputManager);
        }

        public bool IsCursorOnExitIcon(InputManager inputManager) {
            return ui.ExitIconRectangle().Contains(inputManager.GetMousePosition());
        }

        public bool IsCursorOnSubtitlesIcon(InputManager inputManager) {
            return ui.SubtitlesIconRectangle().Contains(inputManager.GetMousePosition());
        }

        public void GoUp() {
            ui.GoUp();
        }

        public void GoDown() {
            ui.GoDown();
        }
    }
}
