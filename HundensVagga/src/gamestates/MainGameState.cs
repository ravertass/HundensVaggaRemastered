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
        private const string START_ROOM_NAME = "telephone_open";

        private StateManager stateManager;

        private ContentManager content;
        public ContentManager Content {
            get { return content; }
        }

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

        private InGameStateManager inGameStateManager;
        public InGameStateManager InGameStateManager {
            get { return inGameStateManager; }
        }

        public MainGameState(StateManager stateManager, ContentManager content, 
                CursorManager cursorManager, Rooms rooms, Inventory inventory) {
            this.stateManager = stateManager;
            this.content = content;
            this.cursorManager = cursorManager;
            this.rooms = rooms;
            this.inventory = inventory;
            inGameStateManager = new InGameStateManager();

            songManager = new SongManager();

            GoToRoom(START_ROOM_NAME);

            // TODO remove
            inventory.AddItem(new Item("cat", content.Load<Texture2D>("inventory/cat")));
        }

        public void Update(InputManager inputManager) {
            cursorManager.SetToDefault();
            inventory.Update(inputManager);
            inGameStateManager.CurrentState.Update(inputManager);
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

        private void ChangeRoom(string roomName) {
            CurrentRoom = rooms.GetRoom(roomName);
            if (CurrentRoom.HasSpecialState())
                inGameStateManager.CurrentState = (IInGameState)Activator.CreateInstance(
                    CurrentRoom.SpecialStateType, this);
            else
                inGameStateManager.CurrentState = new ExploreState(this);
        }

        public void Win() {
            // TODO go to win state
            Console.WriteLine("Tired of winning");
        }
    }
}
