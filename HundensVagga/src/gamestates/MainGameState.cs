using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace HundensVagga {
    /// <summary>
    /// The main class for handling everything in-game (e.g. not in the start menu, credits etc.).
    /// This class also delegates to a current in-game state, which controls if we're using the 
    /// inventory, exploring rooms, etc.
    /// </summary>
    internal class MainGameState : IGameState {
        private const string START_ROOM_NAME = "front";

        private StateManager stateManager;
        private ContentManager content;

        private CursorManager cursorManager;
        public CursorManager CursorManager {
            get { return cursorManager; }
        }

        private readonly Rooms rooms;
        public Room CurrentRoom { get; set; }
        private SongManager songManager;

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
            this.inventory = inventory;

            songManager = new SongManager();

            GoToRoom(START_ROOM_NAME);

            // TODO remove
            inventory.AddItem(new Item("cat", content.Load<Texture2D>("inventory/cat")));

            CurrentState = new ExploreState(this);
        }

        public void Update(InputManager inputManager) {
            cursorManager.SetToDefault();
            inventory.Update(inputManager);
            CurrentState.Update(inputManager);
            songManager.Update();
        }

        public void Draw(SpriteBatch spriteBatch) {
            CurrentRoom.Draw(spriteBatch);
            inventory.Draw(spriteBatch);
        }

        public void GoToRoom(string roomName) {
            ChangeRoom(roomName);
            songManager.NewRoomSong(CurrentRoom.Song);
        }

        public void StartAtRoom(string roomName) {
            ChangeRoom(roomName);
            songManager.FadeIntoSong(CurrentRoom.Song);
        }

        private void ChangeRoom(string roomName) {
            CurrentRoom = rooms.GetRoom(roomName);
        }
    }
}
