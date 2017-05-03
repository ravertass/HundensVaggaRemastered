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
    public class Inventory {
        private const int ITEM_X_OFFSET = 25;
        private const int ITEM_X_DIFF = 75;
        public const int ITEM_Y_OFFSET = 25;

        private InventoryUI ui;
        private IList<Item> items;

        public Inventory(ContentManager content) {
            ui = new InventoryUI(content);
            items = new List<Item>();
        }

        public void AddItem(Item item) {
            items.Add(item);
        }

        public void RemoveItem(Item item) {
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

        public Item GetItemAt(Vector2 coords) {
            foreach (Item item in items)
                if (item.Rectangle.Contains(coords))
                    return item;

            return null;
        }

        public bool IsBagClicked(InputManager inputManager) {
            return inputManager.IsLeftButtonPressed() 
                && ui.BagRectangle().Contains(inputManager.GetMousePosition());
        }

        public void GoUp() {
            ui.GoUp();
        }

        public void GoDown() {
            ui.GoDown();
        }
    }
}
