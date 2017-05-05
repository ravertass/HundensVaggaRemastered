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
                CursorManager cursorManager, Rooms rooms, Inventory inventory,
                SongManager songManager) {
            this.stateManager = stateManager;
            this.content = content;
            this.cursorManager = cursorManager;
            this.rooms = rooms;
            this.inventory = inventory;
            this.songManager = songManager;
            inGameStateManager = new InGameStateManager();

            GoToRoom(START_ROOM_NAME);

            // TODO remove, add letter
            inventory.AddItem(new Item("cat", content.Load<Texture2D>("inventory/cat")));
            // TODO remove, add letter
            inventory.AddItem(new Item("notebook_drawn", content.Load<Texture2D>("inventory/notebook_drawn")));
            // TODO remove, add letter
            inventory.AddItem(new Item("shovel", content.Load<Texture2D>("inventory/shovel")));
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            cursorManager.SetToDefault();
            inventory.Update(inputManager);
            inGameStateManager.CurrentState.Update(inputManager, gameTime);
            CurrentRoom.Update(gameTime);
            songManager.Update();
        }

        public void Draw(SpriteBatch spriteBatch) {
            CurrentRoom.Draw(spriteBatch);
            inventory.Draw(spriteBatch);
        }

        public void GoToRoom(string roomName) {
            ChangeRoom(roomName);
            songManager.NewRoom(CurrentRoom);
        }

        private void ChangeRoom(string roomName) {
            CurrentRoom = rooms.GetRoom(roomName);
            CurrentRoom.GoTo();
            if (CurrentRoom.HasSpecialState())
                inGameStateManager.CurrentState = (IInGameState)Activator.CreateInstance(
                    CurrentRoom.SpecialStateType, this);
            else
                inGameStateManager.CurrentState = new ExploreState(this);
            inGameStateManager.PushState();
        }

        public void Win() {
            // TODO go to win state
            Console.WriteLine("Tired of winning");
        }
    }
}
