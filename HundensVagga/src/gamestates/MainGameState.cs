using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace HundensVagga {
    /// <summary>
    /// The main class for handling everything in-game (e.g. not in the start menu, credits etc.).
    /// This class also delegates to a current in-game state, which controls if we're using the 
    /// inventory, exploring rooms, etc.
    /// </summary>
    internal class MainGameState : IGameState {
        private const string START_ROOM_NAME = "front";
        public SoundEffectInstance CurrentPlayingLookSound { get; set; }


        private Game game;

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

        private MiscContent miscContent;

        public MiscContent MiscContent {
            get { return miscContent; }
        }

        public MainGameState(Game game, StateManager stateManager, ContentManager content, 
                CursorManager cursorManager, Rooms rooms, Inventory inventory,
                SongManager songManager, Items items, MiscContent miscContent) {
            this.game = game;
            this.stateManager = stateManager;
            this.content = content;
            this.cursorManager = cursorManager;
            this.rooms = rooms;
            this.inventory = inventory;
            this.songManager = songManager;
            inGameStateManager = new InGameStateManager();
            this.miscContent = miscContent;

            GoToRoom(START_ROOM_NAME);

            inventory.AddItem(items.GetItem("letter"));
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            UpdateCurrentPlayingLookSound();
            cursorManager.SetToDefault();
            inventory.Update(inputManager);
            inGameStateManager.CurrentState.Update(inputManager, gameTime);
            CurrentRoom.Update(gameTime);
            songManager.Update();
        }

        private void UpdateCurrentPlayingLookSound() {
            if (CurrentPlayingLookSound != null
                && CurrentPlayingLookSound.State == SoundState.Stopped)
                CurrentPlayingLookSound = null;
        }

        public void Draw(SpriteBatch spriteBatch) {
            CurrentRoom.Draw(spriteBatch);
            inventory.Draw(spriteBatch);
            inGameStateManager.CurrentState.Draw(spriteBatch);
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

        public void ExitGame() {
            game.Exit();
        }

        public void Win() {
            // TODO go to win state
            Console.WriteLine("Tired of winning");
        }
    }
}
