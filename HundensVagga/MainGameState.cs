using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace HundensVagga {
    public class MainGameState : IGameState {
        private StateManager stateManager;
        private ContentManager content;
        private CursorManager cursorManager;
        public CursorManager CursorManager {
            get { return cursorManager; }
        }
        private readonly Rooms rooms;
        public Rooms Rooms {
            get { return rooms; }
        }
        public Room CurrentRoom { get; set; }
        private Inventory inventory;
        public Inventory Inventory {
            get { return inventory; }
        }

        public IInGameState CurrentState { get; set; }

        public MainGameState(StateManager stateManager, ContentManager content, 
                CursorManager cursorManager, Rooms rooms) {
            this.stateManager = stateManager;
            this.content = content;
            this.cursorManager = cursorManager;
            this.rooms = rooms;
            CurrentRoom = rooms.GetRoom("front");
            this.inventory = new Inventory(content);

            // TODO ta bort
            inventory.AddItem(new Item("cat", content.Load<Texture2D>("inventory/cat")));
            inventory.AddItem(new Item("flashlight", content.Load<Texture2D>("inventory/flashlight")));
            inventory.AddItem(new Item("saucepan_hot", content.Load<Texture2D>("inventory/saucepan_hot")));

            CurrentState = new ExploreState(this);
        }

        public void Update(InputManager inputManager) {
            cursorManager.SetToDefault();
            inventory.Update(inputManager);
            CurrentState.Update(inputManager);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(CurrentRoom.Background, new Vector2(0f, 0f), Color.White);
            inventory.Draw(spriteBatch);
        }
    }
}
