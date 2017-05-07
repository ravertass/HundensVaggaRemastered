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
        private const int ITEM_X_OFFSET = 20;
        private const int ITEM_X_DIFF = 75;
        public const int ITEM_Y_OFFSET = 57;

        private InventoryUI ui;
        private IList<IItem> items;

        public Inventory(ContentManager content) {
            ui = new InventoryUI(content);
            items = new List<IItem>();
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

        public bool IsCursorOnBag(InputManager inputManager) {
            return ui.BagRectangle().Contains(inputManager.GetMousePosition());
        }

        public bool IsBagClicked(InputManager inputManager) {
            return inputManager.IsLeftButtonPressed()
                && IsCursorOnBag(inputManager);
        }

        public void GoUp() {
            ui.GoUp();
        }

        public void GoDown() {
            ui.GoDown();
        }
    }
}
