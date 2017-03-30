using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    class Inventory {
        private InventoryUI ui;
        private IList<Item> items;

        public Inventory(ContentManager content) {
            ui = new InventoryUI(content);
            items = new List<Item>();
        }

        public void AddItem(Item item) {
            items.Add(item);
        }

        public bool IsActive() {
            return ui.IsDown();
        }

        public void Update(InputManager inputManager) {
            ui.Update(inputManager);
        }

        public void Draw(SpriteBatch spriteBatch) {
            ui.Draw(spriteBatch, items);
        }
    }
}
