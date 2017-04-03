using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace HundensVagga {
    /// <summary>
    /// The main class for handling everything in-game (e.g. not in the start menu, credits etc.).
    /// This class also delegates to a current in-game state, which controls if we're using the 
    /// inventory, exploring rooms, etc.
    /// </summary>
    internal class MainGameState : IGameState {
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
                CursorManager cursorManager, Rooms rooms, Inventory inventory) {
            this.stateManager = stateManager;
            this.content = content;
            this.cursorManager = cursorManager;
            this.rooms = rooms;
            CurrentRoom = rooms.GetRoom("front");
            this.inventory = inventory;

            // TODO remove
            inventory.AddItem(new Item("cat", content.Load<Texture2D>("inventory/cat")));
            inventory.AddItem(new Item("flashlight", content.Load<Texture2D>("inventory/flashlight")));
            inventory.AddItem(new Item("saucepan_hot", content.Load<Texture2D>("inventory/saucepan_hot")));
            inventory.AddItem(new Item("mainkey", content.Load<Texture2D>("inventory/mainkey")));

            CurrentState = new ExploreState(this);
        }

        public void Update(InputManager inputManager) {
            cursorManager.SetToDefault();
            inventory.Update(inputManager);
            CurrentState.Update(inputManager);
        }

        public void Draw(SpriteBatch spriteBatch) {
            CurrentRoom.Draw(spriteBatch);
            inventory.Draw(spriteBatch);
        }
    }
}
